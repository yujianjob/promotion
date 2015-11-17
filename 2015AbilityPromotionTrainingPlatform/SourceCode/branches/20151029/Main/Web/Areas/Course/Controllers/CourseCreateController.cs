﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Dianda.AP.BLL;
using Dianda.AP.Model;
using Dianda.AP.Model.Course.CourseCreate;
using Web.Attributes;
using Dianda.Common;
using System.IO;

namespace Web.Areas.Course.Controllers
{
    public class CourseCreateController : Controller
    {
        //
        // GET: /CourseCreate/

        [ValidateInput(false)]
        public ActionResult CourseList()
        {
            //获取用户Id
            int AccountId = Code.SiteCache.Instance.LoginInfo.UserId;
            //ViewBag.Title = "课程制作列表";
            Course_DetailBLL bll = new Course_DetailBLL();
            
            //获取所有课程单元信息
            int subjectId;
            int.TryParse(Request["SubjectId"], out subjectId);
            ViewBag.Subject = bll.GetSubjectInfoList();
            ViewBag.SubjectId = subjectId;

            string keyWords = "";
            if (!string.IsNullOrEmpty(Request["KeyWords"]))
                keyWords = Request["KeyWords"].Trim();
            ViewBag.KeyWords = keyWords;

            Code.SiteCache cache = Code.SiteCache.Instance;
            StringBuilder where = new StringBuilder();
            where.Append("A.Delflag=0 and A.Status<>1 and A.PartitionId=" + cache.LoginInfo.PartitionId);
            if (keyWords != "")
                where.Append(" and Title like '%" + keyWords + "%'");
            if (subjectId != 0)
                where.Append(" and " + subjectId + " in (select value from [dbo].[split](Subject,','))");
            where.Append(" and A.Status > 2 AND A.Status != 4 and A.OutSideType = -1 and C.AccountId=" + AccountId);

            //获取课程总记录数
            int recordCount = bll.GetTrainingInfoCount(where.ToString());

            int pageSize = 8, pageIndex;
            int pageCount = (int)Math.Ceiling((double)recordCount / pageSize);
            int.TryParse(Request["PageIndex"], out pageIndex);
            if (pageIndex < 1)
                pageIndex = 1;
            else if (pageIndex > pageCount)
                pageIndex = pageCount;
            ViewBag.RecordCount = recordCount;
            ViewBag.PageCount = pageCount;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;

            //获取课程单元信息
            ViewBag.SubjectData = bll.GetSubject();
            //获取课程列表信息
            ViewBag.TrainingInfoList = bll.GetTrainingInfoList(pageSize, pageIndex, where.ToString(), "A.CreateDate");
            
            return View();
        }

        [UrlDecrypt]
        public ActionResult TrainingDetail(int id)
        {
            PrepareTrainingApplyBLL bll = new PrepareTrainingApplyBLL();
            Traning_Detail model = bll.GetTrainingDetail(id, "Delflag=0");
            if (model == null)
            {
                return Content("<script>setTimeout(function () { window.location.href = '/Course/CourseCreate/CourseList' }, 3000);</script>操作失败,3秒后自动返回到列表！");
            }

            ViewBag.TrainingObjectData = bll.GetTraningObject();
            ViewBag.SubjectData = bll.GetSubject();
            ViewBag.StudyLevelData = bll.GetStudyLevel();
            ViewBag.TrainingFormData = bll.GetTrainingForm();
            ViewBag.TeacherTitleData = bll.GetTeacherTitle();
            ViewBag.TrainingFeildData = bll.GetTrainingField("Delflag=0 and IsSpec=0", "");
            ViewBag.TrainingCategoryData = bll.GetTrainingCategory("Delflag=0 and Field=" + model.TraingField, "");
            ViewBag.TrainingTopicData = bll.GetTrainingTopic("Delflag=0 and CategoryId=" + model.TraingCategory, "");
            ViewBag.AttachData = bll.GetAttachTable("Delflag=0 and TraningId=" + id, "");
            ViewBag.OutCourseTypeData = bll.GetOutCourseType("Delflag=0", "");
            ViewBag.NationalCoursData = new NationalAbility_CourseBLL().GetList2("Delflag=0 and TCategoryId=" + model.TraingCategory, "");
            return View(model);
        }

        [UrlDecrypt]
        public ActionResult CourseDetail(int TrainingId)
        {
            //ViewBag.Title = "课程制作详细";
            Course_DetailBLL bll = new Course_DetailBLL();
            Code.SiteCache cache = Code.SiteCache.Instance;

            if (!bll.IsExistsCourseInfo(TrainingId))
            {
                AddCourseDetail(TrainingId);
            }

            //指定课程的课程ID
            ViewBag.TrainingId = TrainingId;
            //获取指定课程ID对应的课程单元信息
            ViewBag.TrainingInfo = bll.GetTrainingInfoById(TrainingId);

            cache.TrainingId = TrainingId;
            cache.UnitId = TrainingId;
            ViewBag.ExamQuesCnt = cache.ExamQuesInfo.Count;
            ViewBag.ExamInfo = cache.ActivityInfo;

            return View();
        }

        [UrlDecrypt]
        public ActionResult CourseUnitAdd(int TrainingId)
        {
            //ViewBag.Title = "课程单元添加";
            Course_DetailBLL bll = new Course_DetailBLL();

            //指定课程的课程ID
            ViewBag.TrainingId = TrainingId;
            //获取指定课程ID对应的课程单元信息
            ViewBag.TrainingInfo = bll.GetTrainingInfoById(TrainingId);
            //获取所有章节信息
            ViewBag.AllChapterSectionInfo = GetAllChapterSectionInfo(TrainingId,1);

            return View();
        }

        [UrlDecrypt]
        public ActionResult CourseUnitEdit(int TrainingId,int CourseId)
        {
            //ViewBag.Title = "课程单元编辑";
            Course_DetailBLL bll_CourseDetail = new Course_DetailBLL();
            Course_UnitDetailBLL bll = new Course_UnitDetailBLL();

            //指定课程的课程ID
            ViewBag.TrainingId = TrainingId;
            //获取指定课程ID对应的课程单元信息
            ViewBag.TrainingInfo = bll_CourseDetail.GetTrainingInfoById(TrainingId);
            //获取课程单元Model
            ViewBag.UnitDetailModel = bll.GetModel(CourseId,"Delflag = 0");

            return View();
        }

        [UrlDecrypt]
        public ActionResult CourseActivityAdd(int TrainingId)
        {
            //ViewBag.Title = "学习活动添加";
            Course_DetailBLL bll = new Course_DetailBLL();

            //指定课程的课程ID
            ViewBag.TrainingId = TrainingId;
            //获取指定课程ID对应的课程单元信息
            ViewBag.TrainingInfo = bll.GetTrainingInfoById(TrainingId);

            return View();
        }

        [UrlDecrypt]
        public ActionResult CourseSetUpRatio(int TrainingId)
        {
            //ViewBag.Title = "考核比例设定";
            Course_DetailBLL bll = new Course_DetailBLL();

            //指定课程的课程ID
            ViewBag.TrainingId = TrainingId;
            //获取指定课程ID对应的课程单元信息
            ViewBag.TrainingInfo = bll.GetTrainingInfoById(TrainingId);
            //获取指定课程ID对应的课程考核比例信息
            ViewBag.TestRateInfo = bll.GetTestRateModel(TrainingId, "delflag = 0");

            return View();
        }

        [UrlDecrypt]
        public ActionResult CourseLearn(int TrainingId,int ClassId)
        {
            //ViewBag.Title = "课程预览";

            Course_DetailBLL bll = new Course_DetailBLL();
            Code.SiteCache cache = Code.SiteCache.Instance;

            //指定课程的课程ID
            ViewBag.TrainingId = TrainingId;
            //获取指定课程ID对应的课程单元信息
            ViewBag.TrainingInfo = bll.GetTrainingInfoById(TrainingId);
            //获取班级ID
            ViewBag.ClassId = ClassId;

            cache.TrainingId = TrainingId;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              
            cache.UnitId = TrainingId;
            cache.ClassId = ClassId;
            ViewBag.ExamQuesCnt = cache.ExamQuesInfo.Count;
            ViewBag.ExamInfo = cache.ActivityLearn;

            this.MyProgress(TrainingId);

            return View();
        }

        [UrlDecrypt]
        public ActionResult CoursePreviewLearn(int TrainingId)
        {
            //ViewBag.Title = "课程预览";

            Course_DetailBLL bll = new Course_DetailBLL();
            Code.SiteCache cache = Code.SiteCache.Instance;

            //指定课程的课程ID
            ViewBag.TrainingId = TrainingId;
            //获取指定课程ID对应的课程单元信息
            ViewBag.TrainingInfo = bll.GetTrainingInfoById(TrainingId);

            cache.TrainingId = TrainingId;
            cache.UnitId = TrainingId;
            ViewBag.ExamQuesCnt = cache.ExamQuesInfo.Count;
            ViewBag.ExamInfo = cache.ActivityInfo;

            return View();
        }

        [UrlDecrypt]
        public ActionResult CourseActivityReading(int TrainingId)
        {
            //ViewBag.Title = "阅读文本增加";
            Course_DetailBLL bll = new Course_DetailBLL();

            //指定课程的课程ID
            ViewBag.TrainingId = TrainingId;
            //获取指定课程ID对应的课程单元信息
            ViewBag.TrainingInfo = bll.GetTrainingInfoById(Convert.ToInt32(TrainingId));
            //获取所有章节信息
            ViewBag.AllChapterSectionInfo = GetAllChapterSectionInfo(Convert.ToInt32(TrainingId),2);

            return View();
        }

        [UrlDecrypt]
        public ActionResult CourseActivityReadingEdit(int TrainingId,int UnitId)
        {
            //ViewBag.Title = "阅读文本编辑";
            Course_DetailBLL bll_CourseDetail = new Course_DetailBLL();
            Course_UnitContentBLL bll = new Course_UnitContentBLL();

            //指定课程的课程ID
            ViewBag.TrainingId = TrainingId;
            //获取指定课程ID对应的课程单元信息
            ViewBag.TrainingInfo = bll_CourseDetail.GetTrainingInfoById(Convert.ToInt32(TrainingId));
            //获取阅读文本Model
            ViewBag.ActivityReadingModel = bll.GetModel(Convert.ToInt32(UnitId), "Delflag = 0");

            return View();
        }

        [UrlDecrypt]
        public ActionResult CourseActivityVideo(int TrainingId)
        {
            //ViewBag.Title = "影音教材增加";
            Course_DetailBLL bll = new Course_DetailBLL();

            //指定课程的课程ID
            ViewBag.TrainingId = TrainingId;
            //获取指定课程ID对应的课程单元信息
            ViewBag.TrainingInfo = bll.GetTrainingInfoById(Convert.ToInt32(TrainingId));
            //获取所有章节信息
            ViewBag.AllChapterSectionInfo = GetAllChapterSectionInfo(Convert.ToInt32(TrainingId),2);

            return View();
        }

        [UrlDecrypt]
        public ActionResult CourseActivityVideoEdit(int TrainingId, int UnitId)
        {
            //ViewBag.Title = "影音教材编辑";
            Course_DetailBLL bll_CourseDetail = new Course_DetailBLL();
            Course_UnitContentBLL bll = new Course_UnitContentBLL();

            //指定课程的课程ID
            ViewBag.TrainingId = TrainingId;
            //获取指定课程ID对应的课程单元信息
            ViewBag.TrainingInfo = bll_CourseDetail.GetTrainingInfoById(Convert.ToInt32(TrainingId));
            //获取影音教材Model
            ViewBag.ActivityVideoModel = bll.GetModel(Convert.ToInt32(UnitId), "Delflag = 0");

            return View();
        }

        [UrlDecrypt]
        public ActionResult CourseActivityDiscuss(int TrainingId)
        {
            //ViewBag.Title = "讨论增加";
            Course_DetailBLL bll = new Course_DetailBLL();

            //指定课程的课程ID
            ViewBag.TrainingId = TrainingId;
            //获取指定课程ID对应的课程单元信息
            ViewBag.TrainingInfo = bll.GetTrainingInfoById(Convert.ToInt32(TrainingId));
            //获取所有章节信息
            ViewBag.AllChapterSectionInfo = GetAllChapterSectionInfo(Convert.ToInt32(TrainingId),2);

            return View();
        }

        [UrlDecrypt]
        public ActionResult CourseActivityDiscussEdit(int TrainingId, int UnitId)
        {
            //ViewBag.Title = "讨论编辑";
            Course_DetailBLL bll_CourseDetail = new Course_DetailBLL();
            Course_UnitContentBLL bll = new Course_UnitContentBLL();

            //指定课程的课程ID
            ViewBag.TrainingId = TrainingId;
            //获取指定课程ID对应的课程单元信息
            ViewBag.TrainingInfo = bll_CourseDetail.GetTrainingInfoById(Convert.ToInt32(TrainingId));
            //获取讨论Model
            ViewBag.ActivityDiscussModel = bll.GetModel(Convert.ToInt32(UnitId), "Delflag = 0");

            return View();
        }

        [UrlDecrypt]
        public ActionResult CourseActivityTask(int TrainingId)
        {
            //ViewBag.Title = "作业增加";
            Course_DetailBLL bll = new Course_DetailBLL();

            //指定课程的课程ID
            ViewBag.TrainingId = TrainingId;
            //获取指定课程ID对应的课程单元信息
            ViewBag.TrainingInfo = bll.GetTrainingInfoById(Convert.ToInt32(TrainingId));
            //获取所有章节信息
            ViewBag.AllChapterSectionInfo = GetAllChapterSectionInfo(Convert.ToInt32(TrainingId),2);

            return View();
        }

        [UrlDecrypt]
        public ActionResult CourseActivityTaskEdit(int TrainingId, int UnitId)
        {
            //ViewBag.Title = "作业编辑";
            Course_DetailBLL bll_CourseDetail = new Course_DetailBLL();
            Course_UnitContentBLL bll = new Course_UnitContentBLL();

            //指定课程的课程ID
            ViewBag.TrainingId = TrainingId;
            //获取指定课程ID对应的课程单元信息
            ViewBag.TrainingInfo = bll_CourseDetail.GetTrainingInfoById(Convert.ToInt32(TrainingId));
            //获取作业Model
            ViewBag.ActivityTaskModel = bll.GetModel(Convert.ToInt32(UnitId), "Delflag = 0");

            return View();
        }

        [UrlDecrypt]
        public ActionResult CourseActivityQuiz(int TrainingId)
        {
            //ViewBag.Title = "测试增加";
            Course_DetailBLL bll = new Course_DetailBLL();

            //指定课程的课程ID
            ViewBag.TrainingId = TrainingId;
            //获取指定课程ID对应的课程单元信息
            ViewBag.TrainingInfo = bll.GetTrainingInfoById(Convert.ToInt32(TrainingId));
            //获取所有章节信息
            ViewBag.AllChapterSectionInfo = GetAllChapterSectionInfo(Convert.ToInt32(TrainingId),2);

            return View();
        }

        [UrlDecrypt]
        public ActionResult CourseActivityQuizEdit(int TrainingId, int UnitId)
        {
            //ViewBag.Title = "测试编辑";
            Course_DetailBLL bll_CourseDetail = new Course_DetailBLL();
            Course_UnitContentBLL bll = new Course_UnitContentBLL();
            Course_UnitQuestionBLL bll_UnitQuestion = new Course_UnitQuestionBLL();

            //指定课程的课程ID
            ViewBag.TrainingId = TrainingId;
            //获取指定课程ID对应的课程单元信息
            ViewBag.TrainingInfo = bll_CourseDetail.GetTrainingInfoById(Convert.ToInt32(TrainingId));
            //获取测试Model
            ViewBag.ActivityQuizModel = bll.GetModel(Convert.ToInt32(UnitId), "Delflag = 0");
            //指定单元活动是否已经添加试题
            ViewBag.IsExistsQuizQues = bll_UnitQuestion.IsExistsQuizQues(Convert.ToInt32(UnitId));

            return View();
        }

        [UrlDecrypt]
        public ActionResult CourseActivityExam(int TrainingId)
        {
            //ViewBag.Title = "结业考试增加";
            Course_DetailBLL bll = new Course_DetailBLL();

            //指定课程的课程ID
            ViewBag.TrainingId = TrainingId;
            //获取指定课程ID对应的课程单元信息
            ViewBag.TrainingInfo = bll.GetTrainingInfoById(Convert.ToInt32(TrainingId));
            //获取所有章节信息
            ViewBag.AllChapterSectionInfo = GetAllChapterSectionInfo(Convert.ToInt32(TrainingId),2);

            return View();
        }

        [UrlDecrypt]
        public ActionResult CourseActivityExamEdit(int TrainingId, int UnitId)
        {
            //ViewBag.Title = "结业考试编辑";
            Course_DetailBLL bll_CourseDetail = new Course_DetailBLL();
            Course_UnitContentBLL bll = new Course_UnitContentBLL();
            Course_UnitTestBLL bll_UnitTest = new Course_UnitTestBLL();

            //指定课程的课程ID
            ViewBag.TrainingId = TrainingId;
            //获取指定课程ID对应的课程单元信息
            ViewBag.TrainingInfo = bll_CourseDetail.GetTrainingInfoById(Convert.ToInt32(TrainingId));
            //获取结业考试Model
            ViewBag.ActivityExamModel = bll.GetModel(Convert.ToInt32(UnitId), "Delflag = 0");
            //指定课程是否已经添加试题
            ViewBag.IsExistsExamQues = bll_UnitTest.IsExistsExamQues(Convert.ToInt32(TrainingId));

            return View();
        }

        [UrlDecrypt]
        public ActionResult CourseActivityQuizQues(int TrainingId,int UnitContent)
        {
            //ViewBag.Title = "添加活动试题";
            Course_DetailBLL bll = new Course_DetailBLL();

            //指定课程的课程ID
            ViewBag.TrainingId = TrainingId;
            //指定活动单元的ID
            ViewBag.UnitContent = UnitContent;
            //获取指定课程ID对应的课程单元信息
            ViewBag.TrainingInfo = bll.GetTrainingInfoById(Convert.ToInt32(TrainingId));

            return View();
        }

        [UrlDecrypt]
        public ActionResult CourseActivityExamQues(int TrainingId, int UnitContent)
        {
            //ViewBag.Title = "添加结业考试试题";
            Course_DetailBLL bll = new Course_DetailBLL();
            Course_UnitTestBLL bll_UnitTest = new Course_UnitTestBLL();

            //指定课程的课程ID
            ViewBag.TrainingId = TrainingId;
            //指定活动单元的ID
            ViewBag.UnitContent = UnitContent;
            //获取指定课程ID对应的课程单元信息
            ViewBag.TrainingInfo = bll.GetTrainingInfoById(Convert.ToInt32(TrainingId));
            //获取指定课程ID对应的课程试题信息
            ViewBag.ExamQuesInfo = bll_UnitTest.GetExamQuesInfo(Convert.ToInt32(TrainingId));

            return View();
        }

        /// <summary>
        /// 当前[我的进度]
        /// </summary>
        /// <param name="TrainingId"></param>
        public void MyProgress(int TrainingId)
        {
            int iAccountId = 0, iClassId = 0,iPlanId=0;

            iAccountId = Code.SiteCache.Instance.LoginInfo.UserId;
            int.TryParse(Convert.ToString(Request["ClassId"]) == "" ? "0" : QueryString.Decrypt(Request["ClassId"]), out iClassId);//获取 ClassId
            iPlanId = Code.SiteCache.Instance.PlanId;

            Member_ClassRegister Model_Member_ClassRegister = new Member_ClassRegister();

            var bll = new Member_ClassRegisterBLL();
            string strWhere = string.Format(@"AccountId = {0} AND ClassId = {1} AND PlanId = {2} and TrainingId = {3} and Delflag = 0 AND Status = 4 ",
                iAccountId, iClassId, iPlanId, TrainingId);
            var List_Member_ClassRegister = bll.GetList(strWhere, "CreateDate");

            if (List_Member_ClassRegister != null && List_Member_ClassRegister.Count == 1)
            {
                Model_Member_ClassRegister = List_Member_ClassRegister[0];
            }

            ViewBag.Model_Member_ClassRegister = Model_Member_ClassRegister;
        }

        #region  新增模块
        #region 课程列表新增
        public bool AddCourseDetail(int TrainingId)
        {
            Code.SiteCache cache = Code.SiteCache.Instance;
            Course_Detail model = new Course_Detail();
            Course_DetailBLL bll = new Course_DetailBLL();

            #region 设置课程Model的值
            model.TrainingId = TrainingId;
            model.AccountId = cache.LoginInfo.UserId;
            model.OrganId = cache.OrganId;
            model.Status = 2;
            model.Display = true;
            model.Sort = 50;
            model.Delflag = false;
            model.CreateDate = DateTime.Now;
            #endregion

            return bll.Add(model);
        }
        #endregion

        #region 学习单元新增
        public ActionResult AddCourseUnit(Course_UnitDetail model)
        {
            Course_UnitDetailBLL bll = new Course_UnitDetailBLL();

            if (bll.Add(model))
            {
                return Json(new { Result = true, Msg = "新增成功!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false, Msg = "新增失败!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 学习活动新增
        public ActionResult AddCourseAtivity(Course_UnitContent model)
        {
            Course_UnitContentBLL bll = new Course_UnitContentBLL();

            int id = bll.Add(model);
            if (id > 0)
            {
                return Json(new { Result = true, UnitContent = id, Msg = "新增成功!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false, UnitContent = id, Msg = "新增失败!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 单元活动测试试题新增
        public ActionResult AddQuizQues(Course_UnitQuestion model)
        {
            Course_UnitQuestionBLL bll = new Course_UnitQuestionBLL();

            if (bll.Add(model))
            {
                return Json(new { Result = true, Msg = "新增成功!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false, Msg = "新增失败!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 结业考试试题新增
        public ActionResult AddEaxmQues(Course_UnitTest model)
        {
            Course_UnitTestBLL bll = new Course_UnitTestBLL();

            if (bll.Add(model))
            {
                return Json(new { Result = true, Msg = "新增成功!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false, Msg = "新增失败!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #endregion

        #region 编辑模块
        #region 课程列表编辑
        public ActionResult EditCourseDetail(Course_TestRate model)
        {
            Course_DetailBLL bll = new Course_DetailBLL();

            if (bll.UpdateTestRate(model))
            {
                return Json(new { Result = true, Msg = "设定成功!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false, Msg = "设定失败!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 学习单元编辑
        public ActionResult EditCourseUnit(Course_UnitDetail model)
        {
            Course_UnitDetailBLL bll = new Course_UnitDetailBLL();

            if (bll.Update(model))
            {
                return Json(new { Result = true, Msg = "修改成功!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false, Msg = "修改失败!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 学习活动编辑
        public ActionResult EditCourseAtivity(Course_UnitContent model)
        {
            Course_UnitContentBLL bll = new Course_UnitContentBLL();

            model.Display = true;

            if (bll.Update(model))
            {
                return Json(new { Result = true, UnitContent = model.Id, Msg = "修改成功!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false, UnitContent = model.Id, Msg = "修改失败!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 单元活动测试试题编辑
        public ActionResult EditUnitQuestion(Course_Question model)
        {
            Course_UnitQuestionBLL bll = new Course_UnitQuestionBLL();

            if (bll.UpdateCourseQuestion(model))
            {
                return Json(new { Result = true, Msg = "修改成功!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false, Msg = "修改失败!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 结业考试试题编辑
        public ActionResult EditUnitTest(Course_Test model)
        {
            Course_UnitTestBLL bll = new Course_UnitTestBLL();

            if (bll.UpdateCourseTest(model))
            {
                return Json(new { Result = true, Msg = "修改成功!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false, Msg = "修改失败!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #endregion

        #region 删除模块
        #region 学习单元删除
        public ActionResult DeleteCourseUnit(int id)
        {
            Course_UnitDetailBLL bll = new Course_UnitDetailBLL();

            if (bll.Delete(id))
            {
                return Json(new { Result = true, Msg = "删除成功!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false, Msg = "删除失败!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 学习活动删除
        public ActionResult DeleteCourseAtivity(int id)
        {
            Course_UnitContentBLL bll = new Course_UnitContentBLL();
            Member_ClassUnitContentScheduleBLL bll_Sch = new Member_ClassUnitContentScheduleBLL();

            if (bll.Delete(id) && bll_Sch.Delete(id))
            {
                return Json(new { Result = true, Msg = "删除成功!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false, Msg = "删除失败!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 单元活动测试试题删除
        public ActionResult DeleteUnitQuestion(int id)
        {
            Course_UnitQuestionBLL bll = new Course_UnitQuestionBLL();

            if (bll.Delete(id))
            {
                return Json(new { Result = true, Msg = "删除成功!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false, Msg = "删除失败!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 结业考试试题删除
        public ActionResult DeleteUnitTest(int id)
        {
            Course_UnitTestBLL bll = new Course_UnitTestBLL();

            if (bll.Delete(id))
            {
                return Json(new { Result = true, Msg = "删除成功!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false, Msg = "删除失败!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #endregion

        #region 设置显示模块
        #region 学习单元显示设置
        public ActionResult DisplayCourseUnit(int id)
        {
            Course_UnitDetailBLL bll = new Course_UnitDetailBLL();

            Course_UnitDetail model = bll.GetModel(id,"Delflag = 0");
            
            //设置是否显示
            if (model.Display)
            {
                model.Display = false;
            }
            else
            {
                model.Display = true;
            }

            if (bll.Update(model))
            {
                return Json(new { Result = true,  Display= model.Display}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false, Display = model.Display}, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 学习活动显示设置
        public ActionResult DisplayCourseAtivity(int id)
        {
            Course_UnitContentBLL bll = new Course_UnitContentBLL();

            Course_UnitContent model = bll.GetModel(id, "Delflag = 0");

            //设置是否显示
            if (model.Display)
            {
                model.Display = false;
            }
            else
            {
                model.Display = true;
            }

            if (bll.Update(model))
            {
                return Json(new { Result = true, Display = model.Display }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false, Display = model.Display }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #endregion 

        #region 公共函数
        #region  获取所有章节信息
        //Type=1表示只获取章,Type=2表示获取所有章节
        public List<Course_ChapterSectionInfo> GetAllChapterSectionInfo(int TrainingId, int Type)
        {
            List<Course_ChapterSectionInfo> lstAllChapterSectionInfo = new List<Course_ChapterSectionInfo>();
            
            //增加【请选择章节】节点
            Course_ChapterSectionInfo head = new Course_ChapterSectionInfo();
            head.Id = -1;
            if (Type == 1)
            {
                head.Title = "--请选择章--";
                lstAllChapterSectionInfo.Add(head);
            }
            else
            {
                head.Title = "--请选择章节--";
                lstAllChapterSectionInfo.Add(head);
            }
            
            //获取所有章的章节信息
            List<Course_ChapterSectionInfo> list = GetChapterSectionInfo(TrainingId,0);
            foreach (var model in list)
            {
                model.Title = "　" + model.Title;
                lstAllChapterSectionInfo.Add(model);

                if (Type == 2)
                {
                    //若章有节,则将节添加到列表
                    if (GetChapterSectionInfo(TrainingId, model.Id) != null)
                    {
                        foreach (var model1 in GetChapterSectionInfo(TrainingId, model.Id))
                        {
                            model1.Title = "　　|--" + model1.Title;
                            lstAllChapterSectionInfo.Add(model1);
                        }
                    }
                }
            }

            return lstAllChapterSectionInfo;
        }
        #endregion

        #region 获取指定课程的章节信息(课程详细)
        public ActionResult ChapterSectionInfo(int TrainingId, int ParentId)
        {
            return Json(new { Data = GetChapterSectionInfo(TrainingId, ParentId) }, JsonRequestBehavior.AllowGet);
        }

        public List<Course_ChapterSectionInfo> GetChapterSectionInfo(int TrainingId, int ParentId)
        {
            List<Course_ChapterSectionInfo> list = new Course_UnitDetailBLL().GetChapterSectionInfo(TrainingId, ParentId); ;
            return list;
        }
        #endregion

        #region 获取指定课程的章节信息(课程预览和学习)
        public ActionResult LearnChapterSectionInfo(int TrainingId, int ParentId)
        {
            return Json(new { Data = GetLearnChapterSectionInfo(TrainingId, ParentId) }, JsonRequestBehavior.AllowGet);
        }

        public List<Course_ChapterSectionInfo> GetLearnChapterSectionInfo(int TrainingId, int ParentId)
        {
            Code.SiteCache cache = Code.SiteCache.Instance;

            cache.TrainingId = TrainingId;
            cache.ParentId = ParentId;
            return cache.LearnChapterSectionInfo;
        }
        #endregion-

        #region 获取指定章节的活动信息(课程详细)
        public ActionResult ActivityInfo(int UnitId)
        {
            Code.SiteCache cache = Code.SiteCache.Instance;

            cache.UnitId = UnitId;
            return Json(new { Data = cache.ActivityInfo }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取指定章节的活动信息(课程预览和学习)
        public ActionResult LearnActivityInfo(int UnitId)
        {
            Code.SiteCache cache = Code.SiteCache.Instance;

            cache.UnitId = UnitId;
            return Json(new { Data = cache.LearnActivityInfo }, JsonRequestBehavior.AllowGet);
        }
        #endregion
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
        #region 获取指定章节的学习活动信息
        public ActionResult ActivityLearn(int UnitId,int ClassId)
        {
            Code.SiteCache cache = Code.SiteCache.Instance;

            cache.UnitId = UnitId;
            cache.ClassId = ClassId;
            return Json(new { Data = cache.ActivityLearn }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 是否学完所有课程
        public ActionResult IsAllLearn(int TrainingId, int ClassId)
        {
            Code.SiteCache cache = Code.SiteCache.Instance;

            cache.TrainingId = TrainingId;
            cache.ClassId = ClassId;
            return Json(new { Data = cache.IsAllLearn }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取指定课程的结业考试信息(课程详细)
        public ActionResult ExamInfo(int TrainingId)
        {
            Code.SiteCache cache = Code.SiteCache.Instance;

            cache.TrainingId = TrainingId;
            return Json(new { Data = cache.ExamInfo }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取指定课程的结业考试信息(课程预览和学习)
        public ActionResult ExamLearnInfo(int TrainingId)
        {
            Code.SiteCache cache = Code.SiteCache.Instance;

            cache.TrainingId = TrainingId;
            return Json(new { Data = cache.ExamLearnInfo }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取指定课程的学习结业考试信息
        public ActionResult LearnExamInfo(int TrainingId,int ClassId)
        {
            Code.SiteCache cache = Code.SiteCache.Instance;

            cache.TrainingId = TrainingId;
            cache.ClassId = ClassId;
            return Json(new { Data = cache.LearnExamInfo }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取指定单元活动测试试题信息
        public ActionResult QuizQuesInfo(int UnitContent)
        {
            Code.SiteCache cache = Code.SiteCache.Instance;

            cache.UnitContent = UnitContent;
            return Json(new { Data = cache.QuizQuesInfo }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取指定单元活动测试试题Model
        public ActionResult QuizQuesModel(int Id)
        {
            Course_UnitQuestion model = new Course_UnitQuestionBLL().GetModel(Id, "Display = 1 and Delflag = 0");
            return Json(new { Data = model }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取指定课程的结业考试试题信息
        public ActionResult ExamQuesInfo(int TrainingId)
        {
            Code.SiteCache cache = Code.SiteCache.Instance;

            cache.TrainingId = TrainingId;
            return Json(new { Data = cache.ExamQuesInfo }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取指定结业考试试题Model
        public ActionResult ExamQuesModel(int Id)
        {
            Course_UnitTest model = new Course_UnitTestBLL().GetModel(Id,"Display = 1 and Delflag = 0");
            return Json(new { Data = model }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取指定章节总时长(课程详细)
        public ActionResult TotalTimeLength(int TimeLengthId)
        {
            Course_UnitDetailBLL bll = new Course_UnitDetailBLL();
            int TotalTimeLength = bll.GetTotalTimeLength(TimeLengthId);
            return Json(new { Data = TotalTimeLength }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取指定章节总时长(课程预览和学习)
        public ActionResult LearnTotalTimeLength(int TimeLengthId)
        {
            Course_UnitDetailBLL bll = new Course_UnitDetailBLL();
            int TotalTimeLength = bll.GetLearnTotalTimeLength(TimeLengthId);
            return Json(new { Data = TotalTimeLength }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取指定章节最大顺序号
        public ActionResult GetChapterSectionMaxOrder(int ParentId,int TrainingId)
        {
            Course_UnitDetailBLL bll = new Course_UnitDetailBLL();
            int Order = bll.GetChapterSectionMaxOrder(ParentId, TrainingId);
            return Json(new { MaxOrder = Order }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取指定章节指定内容类型学习活动最大顺序号
        public ActionResult GetUnitContentMaxOrder(int UnitId, int UnitType)
        {
            Course_UnitContentBLL bll = new Course_UnitContentBLL();
            int Order = bll.GetUnitContentMaxOrder(UnitId, UnitType);
            return Json(new { MaxOrder = Order }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 更新指定单元活动测试试题版本号
        public ActionResult SetQuizVerson(int UnitContent)
        {
            Course_UnitQuestionBLL bll = new Course_UnitQuestionBLL();

            if (bll.SetVerson(UnitContent))
            {
                return Json(new { Result = true, Msg = "您已经完成单元活动测试试题制作!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false, Msg = "单元活动测试试题制作失败!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 更新指定课程结业考试试题版本号
        public ActionResult SetExamVerson(int TrainingId)
        {
            Course_UnitTestBLL bll = new Course_UnitTestBLL();

            if (bll.SetVerson(TrainingId))
            {
                return Json(new { Result = true, Msg = "您已经完成结业试题制作!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false, Msg = "结业试题制作失败!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 判断同一课程下新增章名称是否重复
        public ActionResult IsChapterAddRename(int TrainingId, string tilte)
        {
            bool IsChapterAddRename = new Course_UnitContentBLL().IsChapterAddRename(TrainingId, tilte);
            return Json(new { Data = IsChapterAddRename }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 判断同一课程下编辑章名称是否重复
        public ActionResult IsChapterEditRename(int TrainingId, string tilte, int CourseId)
        {
            bool IsChapterEditRename = new Course_UnitContentBLL().IsChapterEditRename(TrainingId, tilte, CourseId);
            return Json(new { Data = IsChapterEditRename }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 判断同一章下新增节名称是否重复
        public ActionResult IsSectionAddRename(int ParentId, string tilte)
        {
            bool IsSectionAddRename = new Course_UnitContentBLL().IsSectionAddRename(ParentId, tilte);
            return Json(new { Data = IsSectionAddRename }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 判断同一章下编辑节名称是否重复
        public ActionResult IsSectionEditRename(int ParentId, string tilte, int CourseId)
        {
            bool IsSectionEditRename = new Course_UnitContentBLL().IsSectionEditRename(ParentId, tilte, CourseId);
            return Json(new { Data = IsSectionEditRename }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 判断同一章节下同一类型新增活动名称是否重复
        public ActionResult IsActivityAddRename(int UnitId, int UnitType, string tilte)
        {
            bool IsActivityAddRename = new Course_UnitContentBLL().IsActivityAddRename(UnitId, UnitType, tilte);
            return Json(new { Data = IsActivityAddRename }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 判断同一章节下同一类型编辑活动名称是否重复
        public ActionResult IsActivityEditRename(int UnitId, int UnitType, string tilte, int UnitContent)
        {
            bool IsActivityEditRename = new Course_UnitContentBLL().IsActivityEditRename(UnitId, UnitType, tilte, UnitContent);
            return Json(new { Data = IsActivityEditRename }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取Url参数加密字符串
        public ActionResult EnCode(string Code)
        {
            string EnCode = Dianda.Common.QueryString.UrlEncrypt(Code);
            return Json(new { Data = EnCode }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
    }
}
