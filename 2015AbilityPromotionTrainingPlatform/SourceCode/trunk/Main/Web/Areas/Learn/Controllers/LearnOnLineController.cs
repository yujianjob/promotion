﻿using Dianda.AP.BLL;
using Dianda.AP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using XXW.SiteUtils;
using System.IO;
using Dianda.AP.Model.Course.CourseCreate;
using Dianda.AP.Model.Learn.LearnNote;
using Dianda.Common;
using Web.Attributes;
using Web.Code;

namespace Web.Areas.Learn.Controllers
{
    public class LearnOnLineController : Controller
    {
        #region 在线学习 - 阅读
        /// <summary>
        /// 在线学习-阅读
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [UrlDecrypt]
        public ActionResult LearnOnLineReadingView(int TrainingId, int UnitContent, int ClassId)
        {
            var Model_Course_UnitContent = this.GetCourseUnitContentActivity(TrainingId, UnitContent, ClassId, false);

            ViewBag.TimeLength = Model_Course_UnitContent.TimeLength == null ? 0 : Model_Course_UnitContent.TimeLength.Value;
            ViewBag.List_Member_ClassContentNote = this.GetReadingNoteList(TrainingId, ClassId, Model_Course_UnitContent.Id);
            ViewBag.RecordId = this.InsertClassContentTimeRecord(ClassId, TrainingId, UnitContent);

            return View();
        }

        /// <summary>
        /// 获取我的笔记信息
        /// </summary>
        /// <returns></returns>
        private List<Member_ClassContentNote> GetReadingNoteList(int TrainingId, int ClassId, int iUnitContentId, bool IsNeedPaging = true)
        {
            var memberBll = new Member_ClassContentNoteBLL();
            var List_Member_ClassContentNote = new List<Member_ClassContentNote>();
            var stbSqlWhere = new StringBuilder();

            //与课程TrainingId，班级ClassId，活动UnitContent，用户AccountId进行绑定
            int iAccountId = 0;

            iAccountId = Code.SiteCache.Instance.LoginInfo.UserId;

            stbSqlWhere.Append("Delflag = 0");
            stbSqlWhere.Append("AND ClassId = " + ClassId);
            stbSqlWhere.Append("AND TrainingId = " + TrainingId);
            stbSqlWhere.Append("AND AccountId = " + iAccountId);
            stbSqlWhere.Append("AND UnitContent = " + iUnitContentId);

            //获取[我的笔记]总条数
            int iRecordCount = memberBll.GetMemberClassContentNoteCount(stbSqlWhere.ToString());
            int iPageSize = 4, iPageIndex = 0;
            int iPageCount = (int)Math.Ceiling((double)iRecordCount / iPageSize);
            if (!string.IsNullOrEmpty(Request["PageIndex"]))
            {
                int.TryParse(QueryString.Decrypt(Request["PageIndex"]), out iPageIndex);
            }
            if (iPageIndex < 1)
                iPageIndex = 1;
            else if (iPageIndex > iPageCount)
                iPageIndex = iPageCount;
            if (IsNeedPaging)
            {
                //获取分页数据集合
                List_Member_ClassContentNote = memberBll.GetList(iPageSize, iPageIndex, stbSqlWhere.ToString(), "CreateDate", out iRecordCount);
            }
            else
            {
                List_Member_ClassContentNote = memberBll.GetList(stbSqlWhere.ToString(), "CreateDate");
            }

            ViewBag.ClassId = ClassId;
            ViewBag.TrainingId = TrainingId;
            ViewBag.AccountId = iAccountId;
            ViewBag.UnitContent = iUnitContentId;
            ViewBag.RecordCount = iRecordCount;
            ViewBag.PageCount = iPageCount;
            ViewBag.PageIndex = iPageIndex;
            ViewBag.PageSize = iPageSize;

            return List_Member_ClassContentNote;
        }

        /// <summary>
        /// 删除[我的笔记]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult DelContentNote(Member_ClassContentNote model)
        {
            var memberBll = new Member_ClassContentNoteBLL();

            if (memberBll.Delete(model))
            {
                return Json(new { Result = true, Msg = "删除成功!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false, Msg = "删除失败!" }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 编辑[我的笔记]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult EditContentNote(Member_ClassContentNote model)
        {
            var memberBll = new Member_ClassContentNoteBLL();
            if (model.Id == 0)//新增
            {
                if (memberBll.Add(model))
                {
                    return Json(new { Result = true, Msg = "新增成功!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Result = true, Msg = "新增失败!" }, JsonRequestBehavior.AllowGet);
                }
            }
            else//修改
            {
                model.CreateDate = DateTime.Now;
                if (memberBll.Update(model))
                {
                    return Json(new { Result = true, Msg = "修改成功!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Result = true, Msg = "修改失败!" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        #endregion

        #region  在线学习 - 视频
        //learn-video
        //
        // GET: /Learn/LearnOnLine/
        /// <summary>
        /// 在线学习-视频
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [UrlDecrypt]
        public ActionResult LearnOnLineVideoView(int TrainingId, int UnitContent, int ClassId)
        {
            var Model_Course_UnitContent = this.GetCourseUnitContentActivity(TrainingId, UnitContent, ClassId, false);

            #region 学习讨论
            this.GetCourseUnitDetail_Topic(Model_Course_UnitContent.Id);
            #endregion

            ViewBag.TimeLength = Model_Course_UnitContent.TimeLength == null ? 0 : Model_Course_UnitContent.TimeLength.Value;
            ViewBag.ContentType = Model_Course_UnitContent.ContentType;
            ViewBag.Content = string.IsNullOrEmpty(Model_Course_UnitContent.Content) ? string.Empty : Model_Course_UnitContent.Content;
            ViewBag.RecordId = this.InsertClassContentTimeRecord(ClassId, TrainingId, UnitContent);

            return View();
        }

        public ActionResult ReadingNoteList(int TrainingId, int UnitContent, int ClassId)
        {
            var Model_Course_UnitContent = this.GetCourseUnitContentActivity(TrainingId, UnitContent, ClassId, false);
            return Json(new { Data = this.GetReadingNoteList(TrainingId, ClassId, Model_Course_UnitContent.Id, false) }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region  在线学习 - 讨论
        /// <summary>
        /// 在线学习-讨论
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [UrlDecrypt]
        public ActionResult LearnOnLineDiscussView(int TrainingId, int UnitContent, int ClassId)
        {
            //string strTrainingId = QueryString.Decrypt(TrainingId);
            //if (string.IsNullOrEmpty(strTrainingId))
            //{
            //    return Content("<script>location.href ='/Error/Error/Error';</script>");
            //}

            #region 讨论标题

            var Model_Course_UnitContent = this.GetCourseUnitContentActivity(TrainingId, UnitContent, ClassId, false);

            #endregion

            #region 小组成员
            this.GetClassGroupList();
            #endregion

            #region 学习讨论
            this.GetCourseUnitDetail_Topic(Model_Course_UnitContent.Id);
            #endregion

            return View();
        }

        /// <summary>
        /// 获取[班级分组表]信息,讨论组,[小组成员]信息
        /// </summary>
        /// <returns></returns>
        private void GetClassGroupList()
        {
            var grpBll = new Class_GroupBLL();
            var List_Class_Group_Teacher = new List<Class_GroupAll>();
            var List_Class_Group_Member = new List<Class_GroupAll>();
            int iAccountId = 0, iClassId = 0, iGroupRecordCount = 0;

            int.TryParse(Convert.ToString(Request["ClassId"]) == "" ? "0" : QueryString.Decrypt(Request["ClassId"]), out iClassId);//获取 ClassId
            iAccountId = Code.SiteCache.Instance.LoginInfo.UserId;

            //获取 [小组成员-讲师,辅导员]
            List_Class_Group_Teacher = grpBll.GetGroupList(iAccountId, iClassId);

            //获取[小组成员-组员]总条数
            iGroupRecordCount = grpBll.GetGroupList_MemberCount(iClassId, iAccountId);
            int iGroupPageSize = 10, iGroupPageIndex = 0;
            int iGroupPageCount = (int)Math.Ceiling((double)iGroupRecordCount / iGroupPageSize);
            if (!string.IsNullOrEmpty(Request["GroupPageIndex"]))
            {
                int.TryParse(QueryString.Decrypt(Request["GroupPageIndex"]), out iGroupPageIndex);
            }
            
            if (iGroupPageIndex < 1)
                iGroupPageIndex = 1;
            else if (iGroupPageIndex > iGroupPageCount)
                iGroupPageIndex = iGroupPageCount;
            //获取分页数据集合,获取 [小组成员-组员]
            List_Class_Group_Member.AddRange(grpBll.GetGroupList_Member(iGroupPageSize, iGroupPageIndex, iClassId, iAccountId, out iGroupRecordCount));

            ViewBag.GroupAccountId = iAccountId;
            ViewBag.GroupClassId = iClassId;
            ViewBag.GroupRecordCount = iGroupRecordCount;
            ViewBag.GroupPageCount = iGroupPageCount;
            ViewBag.GroupPageIndex = iGroupPageIndex;
            ViewBag.GroupPageSize = iGroupPageSize;
            ViewBag.List_Class_Group_Teacher = List_Class_Group_Teacher;
            ViewBag.List_Class_Group_Member = List_Class_Group_Member;
        }

        /// <summary>
        /// 获取[学习讨论]信息 -- 话题
        /// </summary>
        private void GetCourseUnitDetail_Topic(int iUnitContent)
        {
            var grpBll = new Class_GroupBLL();
            var ReplyBll = new Course_UnitReplyDetailBLL();
            var stbSqlWhere = new StringBuilder();
            var Topic_Reply_Dictry = new Dictionary<Course_UnitReplyDetailOther, List<Course_UnitReplyDetailOther>>();//页面绑定的字典信息
            var List_Course_UnitReplyDetail_ShowTopic = new List<Course_UnitReplyDetailOther>();//话题
            var List_Course_UnitReplyDetail_Reply = new List<Course_UnitReplyDetailOther>();//回复
            var strOrderBy = string.Empty;
            int iAccountId = 0, iClassId = 0;

            int.TryParse(Convert.ToString(Request["ClassId"]) == "" ? "0" : QueryString.Decrypt(Request["ClassId"]), out iClassId);//获取 ClassId
            iAccountId = Code.SiteCache.Instance.LoginInfo.UserId;

            //获取数据总数
            int iTopicRecordCount = ReplyBll.GetListTopicTotalCount(iClassId, iUnitContent);
            int iTopicPageSize = 10, iTopicPageIndex = 0;
            int iTopicPageCount = (int)Math.Ceiling((double)iTopicRecordCount / iTopicPageSize);
            int.TryParse(Request["pageIndex"], out iTopicPageIndex);
            if (iTopicPageIndex < 1)
                iTopicPageIndex = 1;
            else if (iTopicPageIndex > iTopicPageCount)
                iTopicPageIndex = iTopicPageCount;

            List_Course_UnitReplyDetail_ShowTopic = ReplyBll.GetListTopicOther(iTopicPageSize, iTopicPageIndex, iClassId, iUnitContent, out iTopicRecordCount);
            if (List_Course_UnitReplyDetail_ShowTopic != null && List_Course_UnitReplyDetail_ShowTopic.Count > 0)
            {
                foreach (var topicModel in List_Course_UnitReplyDetail_ShowTopic)
                {
                    List_Course_UnitReplyDetail_Reply = ReplyBll.GetListReplyOther(iClassId, iUnitContent, topicModel.CourseUnitReplyDetail.Id);
                    Topic_Reply_Dictry.Add(topicModel, List_Course_UnitReplyDetail_Reply);
                }
            }

            ViewBag.TopicRecordCount = iTopicRecordCount;
            ViewBag.TopicPageCount = iTopicPageCount;
            ViewBag.TopicPageIndex = iTopicPageIndex;
            ViewBag.TopicPageSize = iTopicPageSize;
            ViewBag.Topic_Reply_Dictry = Topic_Reply_Dictry;
        }

        /// <summary>
        /// 讨论新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult EditCourseUnitReply(Course_UnitReplyDetail model)
        {
            //此处借用model.AttList传递TrainingId
            if (this.GetCourseIsOver(model.ClassId, int.Parse(model.AttList), model.AccountId))
                return Json(new { Result = false, Msg = "提交失败.当前课程已结束,不能执行该操作!" }, JsonRequestBehavior.AllowGet);

            model.AttList = string.Empty;

            var UnitContentBll = new Course_UnitContentBLL();
            var ReplyBll = new Course_UnitReplyDetailBLL();

            var Model_UnitContent = UnitContentBll.GetModel(model.UnitContent, string.Empty);
            var iUnitType = Model_UnitContent.UnitType;//[1文本，2影音教材，3讨论，4作业，5测试，6结业考试]

            if (model.Id == 0)//新增
            {
                model.Delflag = false;
                model.Display = true;
                model.CreateDate = DateTime.Now;

                if (iUnitType == 3)//仅当活动为讨论时,计分(视频下的讨论不再记分)
                    this.ScoreSet(2, 0, 0, 0, true, model);//设置分数并更新讨论的进度

                if (ReplyBll.Add(model))
                {
                    return Json(new { Result = true, Msg = "提交成功!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Result = true, Msg = "提交失败!" }, JsonRequestBehavior.AllowGet);
                }
            }
            else//[删除] 修改DelFlag = 1
            {
                if (model.ParentReplyId == 0)
                {
                    if (ReplyBll.Update(model.Id))
                    {
                        if (iUnitType == 3)//仅当活动为讨论时,计分(视频下的讨论不再记分)
                            this.ScoreSet(2, 0, 0, 0, false, model);//设置分数并更新讨论的进度

                        return Json(new { Result = true, Msg = "话题删除成功!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Result = false, Msg = "话题删除失败!" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    if (ReplyBll.Update(model.Id))
                    {
                        if (iUnitType == 3)//仅当活动为讨论时,计分(视频下的讨论不再记分)
                            this.ScoreSet(2, 0, 0, 0, false, model);//设置分数并更新讨论的进度

                        return Json(new { Result = true, Msg = "回复删除成功!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Result = false, Msg = "回复删除失败!" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
        }
        #endregion

        #region  在线学习 - 作业
        /// <summary>
        /// 在线学习-作业
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [UrlDecrypt]
        public ActionResult LearnOnLineTaskView(int TrainingId, int UnitContent, int ClassId)
        {
            #region 作业标题
            var Model_Course_UnitContent = this.GetCourseUnitContentActivity(TrainingId, UnitContent, ClassId, false);
            #endregion

            #region 获取作业信息
            var stbSqlWhere = new StringBuilder();
            Course_UnitHomeWork Model_Course_UnitHomeWork = null;
            int iClassId = 0, iTrainingId = 0, iAccountId = 0, iUnitContent = 0;
            int.TryParse(Convert.ToString(Request["TrainingId"]) == "" ? "0" : QueryString.Decrypt(Request["TrainingId"]), out iTrainingId);
            int.TryParse(Convert.ToString(Request["UnitContent"]) == "" ? "0" : QueryString.Decrypt(Request["UnitContent"]), out iUnitContent);
            int.TryParse(Convert.ToString(Request["ClassId"]) == "" ? "0" : QueryString.Decrypt(Request["ClassId"]), out iClassId);//获取 ClassId
            iAccountId = Code.SiteCache.Instance.LoginInfo.UserId;

            var HomeWorkBll = new Course_UnitHomeWorkBLL();
            stbSqlWhere.AppendFormat(@" UnitContent = {0} AND ClassId = {1} AND AccountId = {2} AND Display = 1 AND Delflag = 0", iUnitContent, iClassId, iAccountId);
            var List_Course_UnitHomeWork = HomeWorkBll.GetList(stbSqlWhere.ToString(), string.Empty);
            if (List_Course_UnitHomeWork.Count > 0)
            {
                Model_Course_UnitHomeWork = List_Course_UnitHomeWork[0];
            }

            double? dblScore = null;
            //查看当前活动是否已打分,若已打分则显示分数
            this.GetActivityScore_Task(ClassId, iAccountId, UnitContent, out dblScore);

            ViewBag.Score = dblScore;//用于打分了的作业显示分数
            ViewBag.Model_Course_UnitHomeWork = Model_Course_UnitHomeWork;
            #endregion

            return View();
        }

        /// <summary>
        /// [在线学习-作业] 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult LearnOnLineTaskEdit(int UnitId, int ClassId, int TrainingId, int UnitContent, string Content, string AttList)
        {
            var stbSqlWhere = new StringBuilder();
            var HomeWorkBll = new Course_UnitHomeWorkBLL();
            var model = new Course_UnitHomeWork();
            int AccountId = Code.SiteCache.Instance.LoginInfo.UserId;
            double? dblScore = null;

            //若课程已结束禁止操作
            if (this.GetCourseIsOver(ClassId, TrainingId, AccountId))
                return Json(new { Result = false, Msg = "提交失败.当前课程已结束,不能执行该操作!" }, JsonRequestBehavior.AllowGet);
            //查看当前活动是否已打分,若已打分则禁止操作
            if (this.GetActivityScore_Task(ClassId, AccountId, UnitContent, out dblScore))
                return Json(new { Result = false, Msg = "提交失败.当前活动已打分,不能执行该操作!" }, JsonRequestBehavior.AllowGet);

            model.UnitContent = UnitContent;
            model.ClassId = ClassId;
            model.Content = Content;
            model.AccountId = AccountId;
            model.AttList = AttList;
            model.Content = Content;

            stbSqlWhere.AppendFormat(@" UnitContent = {0} AND ClassId = {1} AND AccountId = {2} AND Display = 1 AND Delflag = 0", model.UnitContent, model.ClassId, model.AccountId);
            var List_Course_UnitHomeWork = HomeWorkBll.GetList(stbSqlWhere.ToString(), string.Empty);
            if (List_Course_UnitHomeWork.Count > 0)
            {
                var ModelTmp = List_Course_UnitHomeWork[0];

                model.Id = ModelTmp.Id;
                model.Display = ModelTmp.Display;
                model.Delflag = ModelTmp.Delflag;
                model.CreateDate = DateTime.Now;
                model.Score = ModelTmp.Score;
                model.ScoreCreater = ModelTmp.ScoreCreater;

                if (HomeWorkBll.Update(model))
                {
                    //更新总体进度
                    this.UpdateOverallProgress(UnitId, ClassId, TrainingId, AccountId, UnitContent);

                    return Json(new { Result = true, Msg = "作业更新成功!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Result = false, Msg = "作业更新失败!" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                model.Delflag = false;
                model.Display = true;
                model.CreateDate = DateTime.Now;
                model.Score = null;

                if (HomeWorkBll.Add(model))
                {
                    //更新总进度
                    this.UpdateOverallProgress(UnitId, ClassId, TrainingId, AccountId, UnitContent);

                    return Json(new { Result = true, Msg = "作业新增成功!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Result = false, Msg = "作业新增失败!" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        #endregion

        #region 在线学习 - 查看作业
        /// <summary>
        /// 查看作业
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [UrlDecrypt]
        public ActionResult LearnOnLineTaskShowView(int TrainingId, int UnitContent, int ClassId, int UId)
        {
            //获取活动及标题
            var Model_Course_UnitContent = this.GetCourseUnitContentActivity(TrainingId, UnitContent, ClassId, false);

            var homeWorkBll = new Course_UnitHomeWorkBLL();
            var Model_Course_UnitHomeWork = new Course_UnitHomeWork();
            var stbSqlWhere = new StringBuilder();
            var MemberBll = new Member_AccountBLL();
            var strNickName = string.Empty;

            //获取作业信
            stbSqlWhere.AppendFormat(@" UId = {0}", UId);
            Model_Course_UnitHomeWork = homeWorkBll.GetModel(UId, string.Empty);
            if (Model_Course_UnitHomeWork != null)
            {
                strNickName = MemberBll.GetModel(Model_Course_UnitHomeWork.AccountId, string.Empty).Nickname;
            }

            ViewBag.ClassId = ClassId;
            ViewBag.NickName = strNickName;
            ViewBag.UnitContent = UnitContent = Model_Course_UnitHomeWork.Id;
            ViewBag.Model_Course_UnitHomeWork = Model_Course_UnitHomeWork;

            return View();
        }

        #endregion

        #region  在线学习 - 结业考试
        /// <summary>
        /// 在线学习-结业考试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [UrlDecrypt]
        public ActionResult LearnOnLineExamView(int TrainingId, int UnitContent, int ClassId)
        {
            var iAccountId = Code.SiteCache.Instance.LoginInfo.UserId;

            #region [结业考试]标题,答题规则
            var Model_Course_UnitContent = this.GetCourseUnitContentActivity(TrainingId, UnitContent, ClassId, false);

            SessionHelper sess = new SessionHelper();
            if (sess.GetSession("LearnOnLineExam") == null)
            {
                sess.SetSession("LearnOnLineExam", (DateTime.Now).AddMinutes(Model_Course_UnitContent.TimeLimit.Value));
            }

            #endregion

            #region [结业考试]题目信息

            int iTrainingId = 0;
            int.TryParse(Convert.ToString(Request["TrainingId"]) == "" ? "0" : QueryString.Decrypt(Request["TrainingId"]), out iTrainingId);

            this.GetExamCourseUnitTest(iTrainingId);
            #endregion

            //#region 查询该学员是否完成过[结业考试]
            //var TestResultBll = new Member_CourseContentTestAnswerResultBLL();
            //string strWhere = string.Format(" Delflag = 0 AND UnitContent = {1} AND ClassId = {2} AND AccountId = {3}", UnitContent, ClassId, iAccountId);
            //var bolIsFinished = TestResultBll.GetList(strWhere, "CreateDate").Count > 0;//若数据条数大于0,则完成过[结业考试]
            //#endregion

            //ViewBag.IsFinished = bolIsFinished;
            ViewBag.ExamTimeLimit = Model_Course_UnitContent.TimeLimit == null ? 0 : Model_Course_UnitContent.TimeLimit.Value;

            return View();
        }

        /// <summary>
        /// 获取[结业考试]题目信息
        /// </summary>
        /// <param name="iTrainingId"></param>
        /// <param name="strVerson"></param>
        /// <returns></returns>
        private List<Course_UnitTest> GetExamCourseUnitTest(int iTrainingId = 0, string strVerson = "")
        {
            var List_Course_UnitTest = new List<Course_UnitTest>();
            var unitTestBll = new Course_UnitTestBLL();
            var strOrderBy = "CreateDate";
            var stbSqlWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(strVerson))
            {
                stbSqlWhere.AppendFormat(" TrainingId = {0} AND Verson = '{1}'", iTrainingId, strVerson);
            }
            else
            {
                stbSqlWhere.AppendFormat(" Display = 1 AND Delflag = 0 AND TrainingId = {0}", iTrainingId);
            }
            

            List_Course_UnitTest = unitTestBll.GetList(stbSqlWhere.ToString(), strOrderBy);
            ViewBag.List_Course_UnitTest = List_Course_UnitTest;
            ViewBag.ExamRecordCount = List_Course_UnitTest.Count;
            ViewBag.ExamTrainingId = List_Course_UnitTest == null || List_Course_UnitTest.Count <= 0 ? 0 : List_Course_UnitTest[0].TrainingId;
            return List_Course_UnitTest;
        }

        /// <summary>
        /// 在线学习-结业考试提交
        /// </summary>
        /// <returns></returns>
        public ActionResult LearnOnLineExamEdit(List<Member_CourseContentTestAnswerOther> listOther)
        {
            var TestAnswerResultBll = new Member_CourseContentTestAnswerResultBLL();
            var unitBll = new Course_UnitContentBLL();
            var stbSqlWhere = new StringBuilder();
            var strOrderBy = "CreateDate";

            int iTrainingId = 0, iId = 0;

            var Model_ExamAnswerResult = this.GetLearnOnLineExamResult(listOther, out iTrainingId);
            if (Model_ExamAnswerResult == null)
            {
                return Json(new { Result = true, Msg = "试题提交异常!" }, JsonRequestBehavior.AllowGet);
            }

            //若课程已结束禁止操作
            if (this.GetCourseIsOver(Model_ExamAnswerResult.ClassId, iTrainingId, Model_ExamAnswerResult.AccountId))
                return Json(new { Result = true, Msg = "提交失败.当前课程已结束,不能执行该操作!" }, JsonRequestBehavior.AllowGet);
            //查看当前活动是否已打分,若已打分则禁止操作
            if (this.GetActivityScore(Model_ExamAnswerResult.ClassId, iTrainingId, Model_ExamAnswerResult.AccountId, Model_ExamAnswerResult.UnitContent))
                return Json(new { Result = false, Msg = "提交失败.当前活动已打分,不能执行该操作!" }, JsonRequestBehavior.AllowGet);

            //提交时,判断其剩余考试次数
            var model = unitBll.GetModel(Model_ExamAnswerResult.UnitContent, string.Empty);

            stbSqlWhere.AppendFormat(@" UnitContent = {0} and AccountId = {1} and Delflag = 0", Model_ExamAnswerResult.UnitContent, Model_ExamAnswerResult.AccountId);
            var List_Result = TestAnswerResultBll.GetList(stbSqlWhere.ToString(), strOrderBy);
            if (List_Result.Count >= model.TestCnt && model.TestCnt != -1)
            {
                return Json(new { Result = true, Msg = "试题提交失败。你的结业考试 - [ " + model.Title + " ] 剩余答题次数不足!" }, JsonRequestBehavior.AllowGet);
            }

            //提交总分到表 Member_CourseContentTestAnswerResult
            bool bolResult = TestAnswerResultBll.Add(Model_ExamAnswerResult) > 0;
            iId = Model_ExamAnswerResult.Id;

            //提交数据到用户答案表 Member_CourseContentTestAnswer
            if (bolResult)
            {
                bolResult = this.SaveMember_CourseContentTestAnswer(listOther, iId);
            }

            if (bolResult)
            {
                SessionHelper sess = new SessionHelper();
                sess.Remove("LearnOnLineExam");

                int iUnitContent = Model_ExamAnswerResult.UnitContent;
                //更新总进度
                this.UpdateOverallProgress(model.UnitId.Value, Model_ExamAnswerResult.ClassId, iTrainingId, Model_ExamAnswerResult.AccountId, iUnitContent);

                return Json(new { Result = true, Msg = "试题提交成功!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = true, Msg = "试题提交失败!" }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 提交答案到用户答案表
        /// </summary>
        /// <param name="listResultOther"></param>
        /// <param name="iId">总分表的Id</param>
        /// <returns></returns>
        private bool SaveMember_CourseContentTestAnswer(List<Member_CourseContentTestAnswerOther> listResultOther, int iId)
        {
            var unitTestBll = new Course_UnitTestBLL();
            var TestAnswerBll = new Member_CourseContentTestAnswerBLL();

            int iAccountId = 0;
            bool bolRes = false;

            iAccountId = Code.SiteCache.Instance.LoginInfo.UserId;

            foreach (var item in listResultOther)
            {
                var Model_Answer = new Member_CourseContentTestAnswer();

                //获取该题目的正确答案                    
                var Model_Test = unitTestBll.GetModel(item.Id, string.Empty);

                double dblGetCreTmp = 0;
                double.TryParse(item.Credits, out dblGetCreTmp);

                Model_Answer.AnswerResult = iId;
                Model_Answer.Question = item.Id;
                Model_Answer.Answer = item.Answer;
                Model_Answer.Result = Model_Test.QTtype == 4 ? true : Model_Test.Answer == item.Answer;//比对正确答案  若是问答题,直接设置为True
                Model_Answer.AccountId = iAccountId;
                Model_Answer.Delflag = false;
                Model_Answer.CreateDate = DateTime.Now;

                //将数据插入到学员答题内容表,用户的[结业考试]答案保存
                bolRes = TestAnswerBll.Add(Model_Answer);
            }

            return bolRes;
        }

        /// <summary>
        /// 获取[结业考试]结果
        /// </summary>
        /// <param name="listOther"></param>
        /// <returns></returns>
        private Member_CourseContentTestAnswerResult GetLearnOnLineExamResult(List<Member_CourseContentTestAnswerOther> listResultOther, out int TrainingId)
        {
            var strVerson = string.Empty;
            int iUnitContent = 0, iClassId = 0, iAccountId = 0, iQuestionCnt = 0, iRightAnswer = 0, iWrongAnswer = 0, iTrainingId = 0;
            double dblScore = 0, dblTestTotalScore = 0;
            var bolResult = false;

            var unitContentBll = new Course_UnitContentBLL();
            var Model_ExamResult = new Member_CourseContentTestAnswerResult();//保存总分
            var Model_ExamAnswer = new Member_CourseContentTestAnswer();

            if (listResultOther.Count > 0)
            {
                iTrainingId = listResultOther[0].TrainingId;
                iClassId = listResultOther[0].ClassId;
                iAccountId = Code.SiteCache.Instance.LoginInfo.UserId;
                iUnitContent = listResultOther[0].UnitContentId;//一批题目的UnitContentId相同
            }

            TrainingId = iTrainingId;

            var List_Course_UnitTest = this.GetExamCourseUnitTest(iTrainingId);
            iQuestionCnt = List_Course_UnitTest.Count;//总题数
            strVerson = List_Course_UnitTest[0].Verson;
            foreach (var item in listResultOther)
            {
                var modelUnitTest = List_Course_UnitTest.Find(x => x.Id == item.Id);
                if (modelUnitTest.QTtype == 4)//问答题
                {
                    iRightAnswer++;
                }
                else
                {
                    if (item.Answer.Trim() == modelUnitTest.Answer.Trim())//答案正确
                    {
                        iRightAnswer++;
                        dblScore += modelUnitTest.Credit.Value;//用户得分
                    }
                    else//答案错误
                    {
                        iWrongAnswer++;
                    }
                    dblTestTotalScore += modelUnitTest.Credit.Value;//所有题目的总分数
                }
            }

            //获取学员是否过关
            var Model_Content = unitContentBll.GetModel(iUnitContent, string.Empty);
            bolResult = iRightAnswer >= Model_Content.PassLine;

            Model_ExamResult.Verson = strVerson;
            Model_ExamResult.UnitContent = iUnitContent;
            Model_ExamResult.ClassId = iClassId;
            Model_ExamResult.Score = dblScore;
            Model_ExamResult.QuestionCnt = iQuestionCnt;
            Model_ExamResult.RightAnswer = iRightAnswer;
            Model_ExamResult.WrongAnswer = iWrongAnswer;

            Model_ExamResult.Result = bolResult;
            Model_ExamResult.AccountId = iAccountId;
            Model_ExamResult.Delflag = false;
            Model_ExamResult.CreateDate = DateTime.Now;

            return Model_ExamResult;
        }

        /// <summary>
        /// 在线学习-结业考试提交
        /// </summary>
        /// <returns></returns>
        public ActionResult LearnOnLineRefashSession()
        {
            SessionHelper sess = new SessionHelper();
            if (sess.GetSession("LearnOnLineExam") == null)
            {
                sess.SetSession("LearnOnLineExam", DateTime.Now);
                return Json(new { Result = false, Msg = "缓存不存在!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                sess.SetSession("LearnOnLineExam", sess.GetSession("LearnOnLineExam"));
                return Json(new { Result = true, Msg = "继续!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region  在线学习 - 单元测试
        /// <summary>
        /// 在线学习-单元测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [UrlDecrypt]
        public ActionResult LearnOnLineQuizView(int TrainingId, int UnitContent, int ClassId)
        {
            #region [单元测试]标题,答题规则
            var Model_Course_UnitContent = this.GetCourseUnitContentActivity(TrainingId, UnitContent, ClassId, false);
            #endregion

            #region [单元测试]题目信息
            this.GetQuizCourseUnitTest(Model_Course_UnitContent.Id);
            #endregion

            return View();
        }

        /// <summary>
        /// 获取[单元测试]题目信息
        /// </summary>
        /// <param name="iUnitContentId"></param>
        /// <param name="strVerson"></param>
        /// <returns></returns>
        private List<Course_UnitQuestion> GetQuizCourseUnitTest(int iUnitContentId = 0, string strVerson = "")
        {
            var List_Course_UnitQuestion = new List<Course_UnitQuestion>();
            var UnitQuestionBll = new Course_UnitQuestionBLL();
            var strOrderBy = "CreateDate";
            var stbSqlWhere = new StringBuilder();

            if (!string.IsNullOrEmpty(strVerson))
            {
                stbSqlWhere.AppendFormat(" UnitContent = {0} AND Verson = '{1}'", iUnitContentId, strVerson);
            }
            else
            {
                stbSqlWhere.Append(" Display = 1 AND Delflag = 0 AND UnitContent = " + iUnitContentId);
            }

            List_Course_UnitQuestion = UnitQuestionBll.GetList(stbSqlWhere.ToString(), strOrderBy);
            ViewBag.List_Course_UnitQuestion = List_Course_UnitQuestion;
            ViewBag.QuizQuestionCount = List_Course_UnitQuestion.Count;

            return List_Course_UnitQuestion;
        }

        /// <summary>
        /// 在线学习-[单元测试]提交
        /// </summary>
        /// <returns></returns>
        public ActionResult LearnOnLineQuizEdit(List<Member_CourseContentAnswerOther> listOther)
        {
            string strVerson = string.Empty;
            int iClassId = 0, iAccountId = 0, iPlanId = 0, iResultId = 0, iUnitContent = 0, iTrainingId = 0, iQuestionCnt = 0, iRightCount = 0, iWrongCount = 0;
            double dblScoreTotal = 0, dblGetScore = 0, dblResultScore = 0;
            var stbSqlWhere = new StringBuilder();
            //获取 答题总数,答题正确的个数,答题错误的个数,结果
            iQuestionCnt = listOther.Count;//总题数
            bool bolResult = false;

            var unitQuesBll = new Course_UnitQuestionBLL();
            var CourseContentAnswerBll = new Member_CourseContentAnswerBLL();
            var unitBll = new Course_UnitContentBLL();
            var Member_ClassUnitContentSchedulebll = new Member_ClassUnitContentScheduleBLL();

            iAccountId = Code.SiteCache.Instance.LoginInfo.UserId;
            iPlanId = Code.SiteCache.Instance.PlanId;

            //获取[单元测试]总得分
            foreach (var item in listOther)
            {
                //获取该题目的正确答案                    
                var Model_Ques = unitQuesBll.GetModel(item.Id, string.Empty);
                if (!string.IsNullOrEmpty(item.Credits))
                {
                    double dblCre = 0;
                    double.TryParse(item.Credits, out dblCre);
                    dblScoreTotal += dblCre;//获取所有题目的答案的总得分

                    if (item.Answer == Model_Ques.Answer)
                    {
                        iRightCount++;//正确答题数
                        dblResultScore += dblCre;//用户得分
                    }
                    else
                        iWrongCount++;//错误答题数

                    strVerson = item.Verson;//题目版本号,一批题目的版本号都一样
                    iUnitContent = item.UnitContentId;//一批题目的UnitContentId都一样
                    iClassId = item.ClassId;//一批题目的ClassId都一样
                    iTrainingId = item.TrainingId;//一批题目的TrainingId都一样
                }
            }

            //若课程已结束禁止操作
            if (this.GetCourseIsOver(iClassId, iTrainingId, iAccountId))
                return Json(new { Result = true, Msg = "提交失败.当前课程已结束,不能执行该操作!" }, JsonRequestBehavior.AllowGet);
            ////查看当前活动是否已打分,若已打分则禁止操作[待删除.打分操作为自动打分,不作为判断条件]
            //if (this.GetActivityScore(iClassId, iTrainingId, iAccountId, iUnitContent))
            //    return Json(new { Result = false, Msg = "提交失败.当前活动已打分,不能执行该操作!" }, JsonRequestBehavior.AllowGet);

            //提交时,判断其剩余考试次数
            stbSqlWhere.AppendFormat(@"UnitContent = {0} AND ClassId = {1} AND AccountId = {2} AND Delflag = 0", iUnitContent, iClassId, iAccountId);
            var ResultBll = new Member_ContentAnswerResultBLL();
            var List_Member_ContentAnswerResult = ResultBll.GetList(stbSqlWhere.ToString());
            int iRowsCount = List_Member_ContentAnswerResult.Tables[0].Rows.Count;//获取已考试的次数

            var model = unitBll.GetModel(iUnitContent, string.Empty);
            if (iRowsCount >= model.TestCnt && model.TestCnt != -1)
            {
                return Json(new { Result = false, Msg = "试题提交失败。你的单元测试 - [ " + model.Title + " ] 剩余答题次数不足!" }, JsonRequestBehavior.AllowGet);
            }

            var ResultModel = new Member_ContentAnswerResult();
            ResultModel.Verson = strVerson;
            ResultModel.UnitContent = iUnitContent;
            ResultModel.ClassId = iClassId;
            ResultModel.Score = decimal.Parse(dblResultScore.ToString());//用户的答题得分
            ResultModel.QuestionCnt = iQuestionCnt;
            ResultModel.RightAnswer = iRightCount;
            ResultModel.WrongAnswer = iWrongCount;
            ResultModel.Result = bolResult;
            ResultModel.AccountId = iAccountId;
            ResultModel.Delflag = false;
            ResultModel.CreateDate = DateTime.Now;

            //将数据插入到总分表
            bool bolRes = this.InsertMember_ContentAnswerResult(ResultModel, out iResultId);

            if (bolRes)
            {
                foreach (var itemQues in listOther)
                {
                    //获取该题目的正确答案                    
                    var Model_Ques = unitQuesBll.GetModel(itemQues.Id, string.Empty);

                    double dblGetCreTmp = 0;
                    double.TryParse(itemQues.Credits, out dblGetCreTmp);

                    //将用户的[单元测试]答案保存
                    Member_CourseContentAnswer Model_Answer = new Member_CourseContentAnswer();
                    Model_Answer.AnswerResult = iResultId;
                    Model_Answer.Question = itemQues.Id;
                    Model_Answer.Answer = itemQues.Answer;
                    Model_Answer.Result = Model_Ques.Answer == itemQues.Answer;//比对正确答案
                    dblGetScore += Model_Ques.Answer == itemQues.Answer ? dblGetCreTmp : 0;//当前学员答题得分
                    Model_Answer.AccountId = iAccountId;
                    Model_Answer.Delflag = false;
                    Model_Answer.CreateDate = DateTime.Now;

                    //将数据插入到学员答题内容表,用户的[课程评价]答案保存
                    bolRes = CourseContentAnswerBll.Add(Model_Answer);
                }
            }

            if (bolRes)
            {
                //更新总进度
                this.UpdateOverallProgress(model.UnitId.Value, iClassId, iTrainingId, iAccountId, iUnitContent);
                //查询该学员的[单元测试]的成绩中最大分数的数据对象
                var Model_ActivityMaxScore = this.GetActivityMaxScore(iClassId, iTrainingId, iAccountId, iUnitContent);
                if (Model_ActivityMaxScore != null)
                {
                    var strWhere_Activity = string.Format(@" Status = 1 AND Delflag = 0 AND ClassId = {0} AND TrainingId = {1} AND AccountId = {2} AND UnitContent IN ({3})",
                        iClassId, iTrainingId, iAccountId, iUnitContent);
                    var List_ClassUnitContentSchedule = Member_ClassUnitContentSchedulebll.GetList(strWhere_Activity, "Id");
                    if (List_ClassUnitContentSchedule != null && List_ClassUnitContentSchedule.Count == 1)
                    {
                        var Model_ClassUnitContentSchedule = List_ClassUnitContentSchedule[0];
                        ScoreSetHelper.ScoreSet(Model_ClassUnitContentSchedule.Id, 4, iClassId, iPlanId, iAccountId, Model_ActivityMaxScore.Score.ToDouble(), Model_ClassUnitContentSchedule.score == null);
                    }
                }

                return Json(new { Result = true, Msg = "单元测试提交成功!", TotalScore = dblGetScore }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = false, Msg = "单元测试提交失败!" }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 插入数据到[单元测试]总分表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool InsertMember_ContentAnswerResult(Member_ContentAnswerResult model, out int iId)
        {
            var ContentAnswerResultbll = new Member_ContentAnswerResultBLL();

            return (iId = ContentAnswerResultbll.Add(model)) > 0;
        }

        /// <summary>
        /// 获取活动[单元测试]成绩中的最大分数的对象
        /// </summary>
        /// <param name="iClassId"></param>
        /// <param name="iTrainingId"></param>
        /// <param name="iAccountId"></param>
        /// <param name="iUnitContent"></param>
        /// <returns></returns>
        private Member_ContentAnswerResult GetActivityMaxScore(int iClassId, int iTrainingId, int iAccountId, int iUnitContent)
        {
            var Member_ContentAnswerResultbll = new Member_ContentAnswerResultBLL();
            var strWhere_Activity = string.Format(@" Delflag = 0 AND ClassId = {0} AND AccountId = {2} AND UnitContent IN ({3})",
                    iClassId, iTrainingId, iAccountId, iUnitContent);

            return Member_ContentAnswerResultbll.GetModel(strWhere_Activity);
        }

        #endregion

        #region 在线学习 - 查看考试,测试,课程评价

        /// <summary>
        /// 查看[单元测试]答题
        /// </summary>
        /// <param name="TrainingId"></param>
        /// <param name="UnitContent"></param>
        /// <param name="ClassId"></param>
        /// <param name="UId"></param>
        /// <returns></returns>
        [HttpGet]
        [UrlDecrypt]
        public ActionResult LearnOnLineQuizShowView(int TrainingId, int UnitContent, int ClassId, int UId)
        {
            var Model_Course_UnitContent = this.GetCourseUnitContentActivity(TrainingId, UnitContent, ClassId, false);

            var stbSqlWhere = new StringBuilder();
            stbSqlWhere.AppendFormat(@" AnswerResult = {0}", UId);

            var AnswerBll = new Member_CourseContentAnswerBLL();
            var ResultBll = new Member_ContentAnswerResultBLL();
            var List_Member_CourseContentAnswer = AnswerBll.GetList(stbSqlWhere.ToString(), string.Empty);
            var Model_Member_ContentAnswerResult = ResultBll.GetModel(UId);

            #region [单元测试]题目信息
            this.GetQuizCourseUnitTest(Model_Course_UnitContent.Id, Model_Member_ContentAnswerResult.Verson);
            #endregion
            
            ViewBag.List_Member_CourseContentAnswer = List_Member_CourseContentAnswer;
            ViewBag.Model_Member_ContentAnswerResult = Model_Member_ContentAnswerResult;

            return View();
        }

        /// <summary>
        /// 查看[结业考试]答题
        /// </summary>
        /// <param name="TrainingId"></param>
        /// <param name="UnitContent"></param>
        /// <param name="ClassId"></param>
        /// <param name="UId"></param>
        /// <returns></returns> 
        [HttpGet]
        [UrlDecrypt]
        public ActionResult LearnOnLineExamShowView(int TrainingId, int UnitContent, int ClassId, int UId)
        {
            var Model_Course_UnitContent = this.GetCourseUnitContentActivity(TrainingId, UnitContent, ClassId, false);

            var stbSqlWhere = new StringBuilder();
            stbSqlWhere.AppendFormat(@"AnswerResult = {0}", UId);

            var AnswerBll = new Member_CourseContentTestAnswerBLL();
            var ResultBll = new Member_CourseContentTestAnswerResultBLL();
            var List_Member_CourseContentTestAnswer = AnswerBll.GetList(stbSqlWhere.ToString(), string.Empty);
            var Model_Member_CourseContentTestAnswerResult = ResultBll.GetModel(UId, string.Empty);

            #region [结业考试]题目信息
            this.GetExamCourseUnitTest(TrainingId, Model_Member_CourseContentTestAnswerResult.Verson);//结业题目表没有UnitContent
            #endregion

            ViewBag.Model_Member_CourseContentTestAnswerResult = Model_Member_CourseContentTestAnswerResult;
            ViewBag.List_Member_CourseContentTestAnswer = List_Member_CourseContentTestAnswer;

            return View();
        }

        /// <summary>
        /// 查看[课程评价]答题
        /// </summary>
        /// <param name="TrainingId"></param>
        /// <param name="UnitContent"></param>
        /// <param name="ClassId"></param>
        /// <param name="UId"></param>
        /// <returns></returns>
        [HttpGet]
        [UrlDecrypt]
        public ActionResult LearnOnLineEvaluateShowView(int ClassId, int UId)
        {
            var stbSqlWhere = new StringBuilder();
            var stbSqlWhere_Question = new StringBuilder();
            stbSqlWhere.AppendFormat(@" AnswerResult = {0}", UId);

            var AnswerBll = new Class_TraningCommentAnswerBLL();
            var ResultBll = new Class_TraningCommentResultBLL();
            var List_Class_TraningCommentAnswer = AnswerBll.GetListModel(stbSqlWhere.ToString());
            var Model_Class_TraningCommentResult = ResultBll.GetModel(UId);

            #region [课程评价]题目信息
            var List_Class_TraningCommentQuestion = new List<Class_TraningCommentQuestion>();
            var TraningCommentBll = new Class_TraningCommentQuestionBLL();

            stbSqlWhere_Question.Append(" Display = 1 AND Delflag = 0 AND Verson = " + Model_Class_TraningCommentResult.Verson);//根据版本号区分题目批次问题
            List_Class_TraningCommentQuestion = TraningCommentBll.GetListModel(stbSqlWhere_Question.ToString());

            ViewBag.List_Class_TraningCommentQuestion = List_Class_TraningCommentQuestion;
            #endregion

            ViewBag.Model_Class_TraningCommentResult = Model_Class_TraningCommentResult;
            ViewBag.List_Class_TraningCommentAnswer = List_Class_TraningCommentAnswer;

            return View();
        }

        #endregion

        #region 笔记本管理
        /// <summary>
        /// 笔记本管理
        /// </summary>
        /// <returns></returns>
        [UrlDecrypt]
        public ActionResult LearnNoteView(int TrainingId, int ClassId)
        {
            Course_DetailBLL bll = new Course_DetailBLL();

            //指定课程的课程ID
            ViewBag.TrainingId = TrainingId;
            //指定班级的ID
            ViewBag.ClassId = ClassId;
            //获取指定课程ID对应的课程单元信息
            ViewBag.TrainingInfo = bll.GetTrainingInfoById(TrainingId);

            return View();
        }

        #region 获取指定课程的章节信息
        public ActionResult ChapterSectionInfo(int TrainingId, int ParentId)
        {
            return Json(new { Data = GetChapterSectionInfo(TrainingId, ParentId) }, JsonRequestBehavior.AllowGet);
        }

        public List<Course_ChapterSectionInfo> GetChapterSectionInfo(int TrainingId, int ParentId)
        {
            Course_UnitDetailBLL bll = new Course_UnitDetailBLL();
            return bll.GetChapterSectionInfo(TrainingId, ParentId);
        }
        #endregion

        #region 获取指定章的笔记信息
        public ActionResult NotesInfo(int Id)
        {
            return Json(new { Data = GetNotesInfoById(Id) }, JsonRequestBehavior.AllowGet);
        }

        public List<LearnNoteInfo> GetNotesInfoById(int Id)
        {
            Member_ClassContentNoteBLL bll = new Member_ClassContentNoteBLL();
            return bll.GetNotesInfoById(Id);
        }
        #endregion

        #region 获取指定ID的笔记信息
        public ActionResult NotesInfoByID(int Id)
        {
            return Json(new { Data = GetNotesInfoByNoteId(Id) }, JsonRequestBehavior.AllowGet);
        }

        public Member_ClassContentNote GetNotesInfoByNoteId(int Id)
        {
            Member_ClassContentNoteBLL bll = new Member_ClassContentNoteBLL();
            return bll.GetModel(Id, "delflag = 0");
        }
        #endregion

        #region 导出指定章的笔记信息
        public ActionResult ExportNotesById(int Id)
        {
            List<LearnNoteInfo> list = GetNotesInfoById(Id);

            if (list.Count == 0)
            {
                return Content("指定章无笔记信息！");
            }
            else
            {
                string path = Server.MapPath("/Areas/Learn/Execl/LearnOnLine/");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string fileName = "笔记(章)(" + Guid.NewGuid().ToString("N") + ").xls";
                string filePath = path + fileName;

                FileStream fs = new FileStream(filePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
                string data = "笔记时间\t笔记内容";

                sw.WriteLine(data);

                for (int i = 0; i < list.Count; i++)
                {
                    data = "";
                    data += list[i].CreateTime.Replace("\n", "").Replace("\r", "");
                    data += "\t";
                    data += list[i].Content.Replace("\n", "").Replace("\r", "");
                    sw.WriteLine(data);
                }

                sw.Close();
                fs.Close();

                return File(new FileStream(Server.MapPath("/Areas/Learn/Execl/LearnOnLine/" + fileName), FileMode.Open), "application/octet-stream", fileName);
            }
        }
        #endregion

        #region 导出指定课程所有的笔记信息
        public ActionResult ExportAllNotes(int TrainingId)
        {
            List<Course_ChapterSectionInfo> list = GetChapterSectionInfo(TrainingId, 0);

            if (list.Count == 0)
            {
                return Content("指定课程无笔记信息！");
            }
            else
            {
                string path = Server.MapPath("/Areas/Learn/Execl/LearnOnLine/");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string fileName = "笔记(课程)(" + Guid.NewGuid().ToString("N") + ").xls";
                string filePath = path + fileName;

                FileStream fs = new FileStream(filePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
                string data = "隶属章\t笔记时间\t笔记内容";

                sw.WriteLine(data);

                for (int i = 0; i < list.Count; i++)
                {
                    List<LearnNoteInfo> listNotes = GetNotesInfoById(list[i].Id);
                    for (int j = 0; j < listNotes.Count; j++)
                    {
                        data = "";
                        data += list[i].Title.Replace("\n", "").Replace("\r", "");
                        data += "\t";
                        data += listNotes[j].CreateTime.Replace("\n", "").Replace("\r", "");
                        data += "\t";
                        data += listNotes[j].Content.Replace("\n", "").Replace("\r", "");
                        sw.WriteLine(data);
                    }
                }

                sw.Close();
                fs.Close();

                return File(new FileStream(Server.MapPath("/Areas/Learn/Execl/LearnOnLine/" + fileName), FileMode.Open), "application/octet-stream", fileName);
            }
        }
        #endregion
        #endregion

        #region 在线学习 - 课程评价

        /// <summary>
        /// 在线学习 - 课程评价
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [UrlDecrypt]
        public ActionResult LearnOnLineEvaluateView()
        {
            #region 获取[课程评价]题目信息

            var List_Class_TraningCommentQuestion = new List<Class_TraningCommentQuestion>();
            var TraningCommentBll = new Class_TraningCommentQuestionBLL();
            var stbSqlWhere = new StringBuilder();
            stbSqlWhere.Append("Display = 1 AND Delflag = 0");

            List_Class_TraningCommentQuestion = TraningCommentBll.GetListModel(stbSqlWhere.ToString());

            ViewBag.List_Class_TraningCommentQuestion = List_Class_TraningCommentQuestion;

            #endregion

            #region 获取 Traning_Detail Title
            //根据 TrainingId 获取 Title
            int iTrainingId = 0, iClassId = 0;
            var DetailBll = new Traning_DetailBLL();
            int.TryParse(Convert.ToString(Request["TrainingId"]) == "" ? "0" : QueryString.Decrypt(Request["TrainingId"]), out iTrainingId);
            int.TryParse(Convert.ToString(Request["ClassId"]) == "" ? "0" : QueryString.Decrypt(Request["ClassId"]), out iClassId);//获取 ClassId

            var Model_Traning_Detail = DetailBll.GetModel(iTrainingId, string.Empty);

            ViewBag.ClassId = iClassId;
            ViewBag.TrainingId = iTrainingId;
            ViewBag.TraningDetailTitle = Model_Traning_Detail.Title;
            #endregion

            return View();
        }

        /// <summary>
        /// 在线学习 - 课程评价 提交
        /// </summary>
        /// <returns></returns>
        public ActionResult LearnOnLineEvaluateEdit(List<Class_TraningCommentAnswerOther> listOther)
        {
            string strVerson = string.Empty;
            int iClassId = 0, iAccountId = 0, iResultId = 0, iTrainingId = 0;
            double dblScoreTotal = 0;
            iAccountId = Code.SiteCache.Instance.LoginInfo.UserId;
            //将用户的[课程评价]答案保存
            var CommentAnswerBll = new Class_TraningCommentAnswerBLL();

            //获取[课程评价]总得分
            foreach (var item in listOther)
            {
                if (!string.IsNullOrEmpty(item.Credits))
                {
                    double dblCre = 0;
                    var strArr = item.Credits.Split(',');
                    for (int i = 0; i < strArr.Length; i++)
                    {
                        var strItem = strArr[i];
                        double.TryParse(strItem, out dblCre);
                        dblScoreTotal += dblCre;//获取所有题目的答案的总得分
                    }

                    strVerson = item.Verson;//题目版本号,一批题目的版本号都一样
                }

                iClassId = item.ClassId;
                iTrainingId = item.TrainingId;
            }

            //若课程已结束禁止操作
            if (this.GetCourseIsOver(iClassId, iTrainingId, iAccountId))
                return Json(new { Result = true, Msg = "提交失败.当前课程已结束,不能执行该操作!" }, JsonRequestBehavior.AllowGet);

            var ResultModel = new Class_TraningCommentResult();
            ResultModel.AccountId = iAccountId;
            ResultModel.ClassId = iClassId;
            ResultModel.Score = int.Parse(dblScoreTotal.ToString());//fusygoto 是否应该是double
            ResultModel.Verson = strVerson;
            ResultModel.Delflag = false;
            ResultModel.CreateDate = DateTime.Now;
            //将数据插入到总分表
            bool bolRes = this.InsertClass_TraningCommentResult(ResultModel, out iResultId);

            if (bolRes)
            {
                foreach (var itemQues in listOther)
                {
                    double dblScoreQues = 0;//答案的分值

                    if (!string.IsNullOrEmpty(itemQues.Credits))
                    {
                        var strArr = itemQues.Credits.Split(',');

                        for (int i = 0; i < strArr.Length; i++)
                        {
                            double dblCre = 0;
                            var strItem = strArr[i];
                            double.TryParse(strItem, out dblCre);
                            dblScoreQues += dblCre;//获取这道题答案的分数
                        }
                    }

                    //将用户的[课程评价]答案保存
                    Class_TraningCommentAnswer Model_CommAnswer = new Class_TraningCommentAnswer();
                    Model_CommAnswer.AnswerResult = iResultId;
                    Model_CommAnswer.Question = itemQues.Id;
                    Model_CommAnswer.Credit = int.Parse(dblScoreQues.ToString());//fusygoto 是否应该是double
                    Model_CommAnswer.Chose = itemQues.Answer;
                    Model_CommAnswer.AccountId = iAccountId;
                    Model_CommAnswer.Delflag = false;
                    Model_CommAnswer.CreateDate = DateTime.Now;

                    //将数据插入到学员答题内容表
                    bolRes = CommentAnswerBll.Add(Model_CommAnswer) > 0;
                }
            }

            if (bolRes)
            {
                ScoreSetHelper.commentTraningOper(iClassId, iAccountId);//[课程评价]保存时记录学时
                return Json(new { Result = true, Msg = "课程评价提交成功!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = true, Msg = "课程评价提交失败!" }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 插入数据到总分表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool InsertClass_TraningCommentResult(Class_TraningCommentResult model, out int iId)
        {
            var TraningCommentResultbll = new Class_TraningCommentResultBLL();

            return (iId = TraningCommentResultbll.Add(model)) > 0;
        }

        #endregion

        #region 公共方法 - 读取活动到页面

        /// <summary>
        /// 获取页面活动的公共方法
        /// </summary>
        /// <param name="bolNeedUpdateProgress">是否需要更新总进度</param>
        /// <returns></returns>
        private Course_UnitContent GetCourseUnitContentActivity(int TrainingId, int UnitContent, int ClassId, bool bolNeedUpdateProgress = true)
        {
            //获取所有的阅读信息
            var courseBll = new Course_UnitContentBLL();
            var DetailBll = new Traning_DetailBLL();
            var List_Course_UnitContent = new List<Course_UnitContent>();
            var Model_Course_UnitContent = new Course_UnitContent();
            var stbSqlWhere = new StringBuilder();
            var strOrderBy = "Sort";

            int iAccountId = 0, iUnitId = 0;
            iAccountId = Code.SiteCache.Instance.LoginInfo.UserId;

            stbSqlWhere.Append(" 1 = 1");
            if (TrainingId > 0)
                stbSqlWhere.AppendFormat(" AND Course_UnitDetail.TrainingId = {0}", TrainingId);
            if (UnitContent > 0)
                stbSqlWhere.AppendFormat(" AND Course_UnitContent.Id = {0}", UnitContent);

            List_Course_UnitContent = courseBll.GetListOther(stbSqlWhere.ToString(), strOrderBy);
            if (List_Course_UnitContent != null && List_Course_UnitContent.Count == 1)
                Model_Course_UnitContent = List_Course_UnitContent[0];

            //根据 TrainingId 获取 Title            
            var Model_Traning_Detail = DetailBll.GetModel(TrainingId, string.Empty);

            ViewBag.TraningDetailTitle = Model_Traning_Detail.Title;
            ViewBag.UnitId = iUnitId = Model_Course_UnitContent.UnitId.Value;
            ViewBag.AccountId = iAccountId;
            ViewBag.ClassId = ClassId;
            ViewBag.TrainingId = TrainingId;
            ViewBag.UnitContent = UnitContent = Model_Course_UnitContent.Id;
            ViewBag.Model_Course_UnitContent = Model_Course_UnitContent;

            if (bolNeedUpdateProgress && List_Course_UnitContent != null && List_Course_UnitContent.Count > 0)
            {
                //更新总体进度
                this.UpdateOverallProgress(iUnitId, ClassId, TrainingId, iAccountId, UnitContent);
            }

            return Model_Course_UnitContent;
        }

        #endregion

        #region 公共方法 - 页面切换活动

        /// <summary>
        /// 获取页面跳转的路径
        /// </summary>
        /// <param name="IsNext"></param>
        /// <returns></returns>
        public ActionResult RedirectedPage(int IsNext, int TrainingId, int UnitContent, int ClassId)
        {
            string strNewPagePath = string.Empty;

            var Model_Course_UnitContent = this.GetCourseUnitContentIndex(TrainingId, ClassId, UnitContent, IsNext == 1);

            //1文本，2影音教材，3讨论，4作业 5测试，6结业考试 其他:课程评价
            switch (Model_Course_UnitContent.UnitType)
            {
                case 1:
                    strNewPagePath = "/Learn/LearnOnLine/LearnOnLineReadingView";
                    break;
                case 2:
                    strNewPagePath = "/Learn/LearnOnLine/LearnOnLineVideoView";
                    break;
                case 3:
                    strNewPagePath = "/Learn/LearnOnLine/LearnOnLineDiscussView";
                    break;
                case 4:
                    strNewPagePath = "/Learn/LearnOnLine/LearnOnLineTaskView";
                    break;
                case 5:
                    strNewPagePath = "/Learn/LearnOnLine/LearnOnLineQuizView";
                    break;
                case 6:
                    strNewPagePath = "/Learn/LearnOnLine/LearnOnLineExamView";
                    break;
            }

            strNewPagePath += string.Format("?TrainingId={0}&UnitContent={1}&ClassId={2}", QueryString.Encrypt(TrainingId), QueryString.Encrypt(Model_Course_UnitContent.Id), QueryString.Encrypt(ClassId));

            return Json(new { Result = true, RedirectedPagePath = strNewPagePath }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 上下页翻动时,获取 Course_UnitContent 对象
        /// </summary>
        /// <param name="iTrainingId"></param>
        /// <param name="iUnitContentId"></param>
        /// <param name="bolNext"></param>
        /// <returns></returns>
        private Course_UnitContent GetCourseUnitContentIndex(int iTrainingId, int iClassId, int iUnitContentId, bool bolNext)
        {
            var Model_Course_UnitContent = new Course_UnitContent();

            var List_Course_UnitContent = this.GetCourseUnitContentActivities(iTrainingId);

            int iIndex = List_Course_UnitContent.FindIndex(x => x.Id == iUnitContentId);
            int iCount = List_Course_UnitContent.Count;

            if (bolNext)
            {
                if (iIndex < iCount - 1)
                    Model_Course_UnitContent = List_Course_UnitContent[iIndex + 1];
                else if (iIndex == iCount - 1)
                    Model_Course_UnitContent = List_Course_UnitContent[0];
            }
            else
            {
                if (iIndex > 0 && iIndex <= iCount - 1)
                    Model_Course_UnitContent = List_Course_UnitContent[iIndex - 1];
                else if (iIndex == iCount - 1)
                    Model_Course_UnitContent = List_Course_UnitContent[0];
                else if (iIndex == 0)
                    Model_Course_UnitContent = List_Course_UnitContent[iCount - 1];
            }

            ViewBag.ClassId = iClassId;
            ViewBag.AccountId = Code.SiteCache.Instance.LoginInfo.UserId;
            ViewBag.Model_Course_UnitContent = Model_Course_UnitContent;

            return Model_Course_UnitContent;
        }

        /// <summary>
        /// 分页查询所有的活动信息
        /// </summary>
        /// <param name="iUnitContentId"></param>
        /// <returns></returns>
        private List<Course_UnitContent> GetCourseUnitContentActivities(int iTrainingId = 0)
        {
            var List_Course_UnitContent = new List<Course_UnitContent>();
            var unitContentbll = new Course_UnitContentBLL();
            var stbSqlWhere = new StringBuilder();
            var strOrderBy = "Course_UnitDetail.Id,Course_UnitContent.Sort";

            if (iTrainingId > 0)
                stbSqlWhere.AppendFormat("Course_UnitDetail.TrainingId = {0}", iTrainingId);
            List_Course_UnitContent = unitContentbll.GetListOther(stbSqlWhere.ToString(), strOrderBy);

            //按照页面排列  章-节-活动

            return List_Course_UnitContent;
        }

        #endregion

        #region 公用方法 - 更新总体进度

        /*
         * 进度汇总逻辑:
         * 
         * 1.用户每次学习课程时需要往 Member_ClassUnitContentSchedule 中插入数据,作为当前的章节小进度.
         * 2.当当前章节所有的课程学习结束,需要判断当前在章节是否学习结束;
         * 3.在修改章节进度和总进度时,需要重新验证所有的章节进度是否已完成(可能存在章节追加的情况).
         * 4.若当前章节全部学习结束,需要在总进度表中 Member_ClassRegister 更改CurrentSchedule(当前章节进度),TotalSchedule(总课程进度).
         * 
         * 所用表:
            select * from Member_ClassRegister
            select * from Course_UnitContent  --1文本，2影音教材，3讨论，4作业 5测试，6结业考试
            select * from Member_ClassUnitContentSchedule            
            select * from Course_Detail
            select * from Course_UnitDetail
         * 
         * [课程]下 分 [章]下 分 [节]下 分 [活动]
         * 同时也可以在[章]下面没有节,只有[活动],所以需要获取章下面所有的活动
         */
        /// <summary>
        /// 更新总体进度
        /// </summary>
        /// <param name="iUnitId">当前[活动]所在[节]的ID</param>
        /// <param name="iClassId"></param>
        /// <param name="iTrainingId"></param>
        /// <param name="iAccountId"></param>
        /// <param name="iUnitContent"></param>
        private void UpdateOverallProgress(int iUnitId, int iClassId, int iTrainingId, int iAccountId, int iUnitContent)
        {
            var unitBll = new Course_UnitContentBLL();
            var detailBll = new Class_DetailBLL();
            var UnitDetailBll = new Course_UnitDetailBLL();
            var ClassRegisterBll = new Member_ClassRegisterBLL();
            var Member_ClassUnitContentSchedulebll = new Member_ClassUnitContentScheduleBLL();
            int iOverChapterCount = 0, iChapterTotalCount = 0;//完成的[章]的数量,当前课程需要学习的[章]总数,该[章]已完成的[节]的数量(该章的进度)

            //用户的在线学习模块都要插入一条数据到Member_ClassUnitContentSchedule,作为当前小章节的学习进度
            if (this.InsertMember_ClassUnitContentSchedule(iClassId, iTrainingId, iAccountId, iUnitContent))
            {
                var List_Course_UnitDetail = UnitDetailBll.GetList(" ParentId = 0 AND Display = 1 AND Delflag = 0 AND TrainingId = " + iTrainingId, string.Empty);
                iChapterTotalCount = List_Course_UnitDetail.Count;//当前课程学员需要完成学习的总章数

                foreach (var ChapterItem in List_Course_UnitDetail)
                {
                    bool bolResult = this.GetChapterProgress(ChapterItem.Id, iClassId, ChapterItem.TrainingId.Value, iAccountId);
                    if (bolResult)//当前[章]的进度
                    {
                        iOverChapterCount++;
                    }
                }
                //[Status] 1等待学校审核 2学校审核通过 3学校审核不通过 4开班机构审核通过 5开班机构审核不通过
                int iPlanId = Code.SiteCache.Instance.PlanId;
                string strWhere = string.Format(@" AccountId = {0} AND ClassId = {1} AND PlanId = {2} and TrainingId = {3} and Delflag = 0 AND Status = 4",
                    iAccountId, iClassId, iPlanId, iTrainingId);

                var List_Member_ClassRegister = ClassRegisterBll.GetList(strWhere, "CreateDate");
                if (List_Member_ClassRegister != null && List_Member_ClassRegister.Count == 1)
                {
                    var Model_Member_ClassRegister = List_Member_ClassRegister[0];

                    Model_Member_ClassRegister.CurrentSchedule = iOverChapterCount;//[章,节]完成数
                    Model_Member_ClassRegister.TotalSchedule = iChapterTotalCount;//课程总章,节数
                    Model_Member_ClassRegister.CreateDate = DateTime.Now;

                    bool bolResult = ClassRegisterBll.Update(Model_Member_ClassRegister);
                }
            }
        }

        /// <summary>
        /// 获取当前[章]的进度是否已完成
        /// </summary>
        /// <param name="iUnitId">当前节ID</param>
        /// <param name="iClassId">当前登录用户的班级ID</param>
        /// <param name="iTrainingId">当前课程ID</param>
        /// <param name="iAccountId">当前登录用户ID</param>
        /// <param name="iPartOverCount">已完成的[节]的数量</param>
        /// <returns></returns>
        private bool GetChapterProgress(int iUnitId, int iClassId, int iTrainingId, int iAccountId)
        {
            var UnitContentBll = new Course_UnitContentBLL();
            var Member_ClassUnitContentSchedulebll = new Member_ClassUnitContentScheduleBLL();

            int iActivityTotalCount = 0;
            var strActitityId = string.Empty;//活动的Id

            //获取最外层的章的ID,得到该章下面所有的节
            var Molde_Course_UnitDetail = this.GetCourse_UnitDetail(iUnitId, iTrainingId);
            if (Molde_Course_UnitDetail != null)
            {
                //根据课程ID和最外层的章的ID, 获取该[章]下面所有的活动
                string strWhere = string.Format(@" UnitId IN (Select Course_UnitDetail.Id from Course_UnitDetail where (Id = {0} OR parentId = {0}) AND TrainingId = {1} AND Display = 1 AND Delflag = 0) AND Display=1 AND Delflag=0",
                    Molde_Course_UnitDetail.Id, iTrainingId);
                List<Course_UnitContent> List_Course_UnitContent = UnitContentBll.GetList(strWhere, "Sort");
                iActivityTotalCount = List_Course_UnitContent.Count;//得到该[章],[节]下面所有的活动数

                if (iActivityTotalCount <= 0)//当[章],[节]下面没有活动时,默认该章未完成
                    return false;

                //获取活动的Id
                foreach (var item in List_Course_UnitContent)
                {
                    strActitityId += item.Id + ",";
                }
                strActitityId = strActitityId.Substring(0, strActitityId.LastIndexOf(','));

                //联合Member_ClassUnitContentSchedule 查看当前用户有没有将当前小章节的课程学习完
                var strWhere_Activity = string.Format(@" Status = 1 AND Delflag = 0 AND ClassId = {0} AND TrainingId = {1} AND AccountId = {2} AND UnitContent IN ({3})",
                    iClassId, iTrainingId, iAccountId, strActitityId);
                //已完成的[章],[节]下面的活动数
                var iOutPartOverCount = Member_ClassUnitContentSchedulebll.GetList(strWhere_Activity, "Id").Count;

                return iActivityTotalCount == iOutPartOverCount;//当前章节下所有活动已完成学习,若相等,该[章]完成
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 用户的在线学习模块都要插入一条数据到Member_ClassUnitContentSchedule,作为学习进度
        /// 为当前学习[节]的学习进度
        /// </summary>
        /// <param name="iClassId"></param>
        /// <param name="iTrainingId"></param>
        /// <param name="iAccountId"></param>
        /// <param name="iUnitContent"></param>
        /// <returns></returns>
        private bool InsertMember_ClassUnitContentSchedule(int iClassId, int iTrainingId, int iAccountId, int iUnitContent)
        {
            var bolResult = false;
            var model = new Member_ClassUnitContentSchedule();
            var comBll = new Member_ClassUnitContentScheduleBLL();
            var strOrderBy = "CreateDate";

            var stbSqlWhere = new StringBuilder();
            stbSqlWhere.Append("Status = 1 AND Delflag = 0");
            stbSqlWhere.AppendFormat("AND ClassId = {0} AND TrainingId={1} AND AccountId={2} AND UnitContent={3}",
                iClassId, iTrainingId, iAccountId, iUnitContent);
            var List_Member_Class = comBll.GetList(stbSqlWhere.ToString(), strOrderBy);
            //当数据不存在的时候添加一条数据进去,否则仅修改最后时间
            if (List_Member_Class == null || List_Member_Class.Count <= 0)
            {
                model.ClassId = iClassId;
                model.TrainingId = iTrainingId;
                model.AccountId = iAccountId;
                model.UnitContent = iUnitContent;
                model.Delflag = false;
                model.Status = true;
                model.CreateDate = DateTime.Now;

                if (comBll.Add(model))
                {
                    bolResult = true;
                }
            }
            else if (List_Member_Class != null && List_Member_Class.Count == 1)
            {
                model = List_Member_Class[0];
                model.CreateDate = DateTime.Now;

                if (comBll.Update(model))
                {
                    bolResult = true;
                }
            }

            return bolResult;
        }

        /// <summary>
        /// 找到当前[活动小节]的最外层[章]
        /// </summary>
        /// <param name="iId"></param>
        /// <param name="iTrainingId"></param>
        /// <returns></returns>
        private Course_UnitDetail GetCourse_UnitDetail(int iId, int? iTrainingId)
        {
            var unitBll = new Course_UnitDetailBLL();
            var model = new Course_UnitDetail();

            model = unitBll.GetModel(iId, "TrainingId = " + iTrainingId.Value);

            if (model != null)
            {
                if (model.ParentId != 0)
                {
                    return GetCourse_UnitDetail(model.ParentId.Value, iTrainingId);
                }
                else
                {
                    return model;
                }
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region 公共方法 - 设置分数

        /// <summary>
        /// 设置分数
        /// </summary>
        /// <param name="iUnitType">//1阅读，2讨论
        /// <param name="IsAdd">True:新增 False:删除</param>
        /// <param name="obj">数据对象</param>
        private void ScoreSet(int iUnitType, int? iUnitContent = 0, int iClassId = 0, int iTrainingId = 0, bool IsAdd = false, Object obj = null)
        {
            var scheduleBll = new Member_ClassUnitContentScheduleBLL();
            var replyBll = new Course_UnitReplyDetailBLL();

            int iPlanId = 0, iAccountId = 0, iUnitId = 0;
            double dblScore = 0;
            var stbSqlWhere = new StringBuilder();

            if (iUnitType == 1)
            {
                iPlanId = Code.SiteCache.Instance.PlanId;
                iAccountId = Code.SiteCache.Instance.LoginInfo.UserId;

                stbSqlWhere.AppendFormat(" ClassId = {0} AND TrainingId = {1} AND AccountId = {2} AND UnitContent = {3} AND Status = 1 AND Delflag = 0",
                    iClassId, iTrainingId, iAccountId, iUnitContent);

                var List_Schedule = scheduleBll.GetList(stbSqlWhere.ToString(), string.Empty);
                if (List_Schedule != null && List_Schedule.Count > 0 && List_Schedule[0].score == null)//当存在记录,并且Score=null时记录分数
                {
                    Web.Code.ScoreSetHelper.ScoreSet(iUnitContent, iUnitType, iClassId, iPlanId, iAccountId, dblScore);
                }
            }
            else if (iUnitType == 2)//讨论  参与次数要 >=1
            {
                var model = obj as Course_UnitReplyDetail;
                iUnitContent = model.UnitContent;
                iClassId = model.ClassId;
                iPlanId = Code.SiteCache.Instance.PlanId;
                iAccountId = model.AccountId;
                stbSqlWhere.AppendFormat(" ClassId = {0} AND AccountId = {1} AND UnitContent = {2} AND Display = 1 AND Delflag = 0",
                        iClassId, iAccountId, iUnitContent);

                var List_ReplyDetail = replyBll.GetList(stbSqlWhere.ToString(), string.Empty);
                if (List_ReplyDetail != null)
                {
                    if (IsAdd)
                    {
                        if (List_ReplyDetail.Count <= 0)
                        {
                            Web.Code.ScoreSetHelper.ScoreSet(iUnitContent, iUnitType, iClassId, iPlanId, iAccountId, dblScore, true);
                        }
                    }
                    else
                    {
                        Web.Code.ScoreSetHelper.ScoreSet(iUnitContent, iUnitType, iClassId, iPlanId, iAccountId, dblScore, false);
                    }
                }

                //获取当前活动的UnitId
                var unitContentBll = new Course_UnitContentBLL();
                var Model_UnitContent = unitContentBll.GetModel(iUnitContent.Value, string.Empty);
                iUnitId = Model_UnitContent.UnitId == null ? 0 : Model_UnitContent.UnitId.Value;
                //获取TrainingId
                var unitDetailBll = new Course_UnitDetailBLL();
                var Model_UnitDetail = unitDetailBll.GetModel(iUnitId, string.Empty);
                iTrainingId = Model_UnitDetail.TrainingId == null ? 0 : Model_UnitDetail.TrainingId.Value;
                //更新总进度
                this.UpdateOverallProgress(iUnitId, iClassId, iTrainingId, iAccountId, iUnitContent.Value);//分数添加之后更新总进度
            }
        }

        #endregion

        #region 公共方法 - 判断当前班级的课程,当前用户的课程有没有结束


        /// <summary>
        /// 课程是否已结束
        /// </summary>
        /// <param name="iClassId"></param>
        /// <param name="iTrainingId"></param>
        /// <param name="iAccountId"></param>
        /// <returns></returns>
        private bool GetCourseIsOver(int iClassId, int iTrainingId, int iAccountId)
        {
            var classBll = new Class_DetailBLL();
            var registerBll = new Member_ClassRegisterBLL();
            var Model_Member_ClassRegister = new Member_ClassRegister();
            int iPlanId = Code.SiteCache.Instance.PlanId;
            var iResult = 0;

            ///***************************************************************1. 判断当前班级的课程有没有结束  开始***************************************************************/
            //var Model_Class_Detail = classBll.GetModel(iClassId);
            //if (Model_Class_Detail.Status == 6 || Model_Class_Detail.OpenClassTo > DateTime.Now)//班级的课程已结束
            //{
            //    return true;
            //}
            ///***************************************************************判断当前班级的课程有没有结束  结束***************************************************************/

            /***************************************************************2. 判断当前用户的课程有没有结束  开始***************************************************************/
            string strWhere = string.Format(@" AccountId = {0} AND ClassId = {1} AND PlanId = {2} and TrainingId = {3} and Delflag = 0 AND Status = 4",
                    iAccountId, iClassId, iPlanId, iTrainingId);

            var List_Member_ClassRegister = registerBll.GetList(strWhere, "CreateDate");
            if (List_Member_ClassRegister != null && List_Member_ClassRegister.Count == 1)
            {
                Model_Member_ClassRegister = List_Member_ClassRegister[0];
                iResult = Model_Member_ClassRegister.Result == null ? 0 : Convert.ToInt32(Model_Member_ClassRegister.Result.Value);
            }
            /***************************************************************判断当前用户的课程有没有结束  结束***************************************************************/

            return iResult == 1;
        }

        #endregion

        #region 公共方法 - 查看当前活动是否已打分,若已打分则禁止操作

        /// <summary>
        /// 查看当前活动是否已打分,若已打分则禁止操作
        /// </summary>
        /// <param name="iClassId"></param>
        /// <param name="iAccountId"></param>
        /// <param name="iUnitContent"></param>
        /// <param name="outScore"></param>
        /// <returns></returns>
        private bool GetActivityScore_Task(int iClassId, int iAccountId, int iUnitContent, out double? outScore)
        {
            double? dblScore = null;
            var bolResult = false;
            var Course_UnitHomeWorkbll = new Course_UnitHomeWorkBLL();
            var Model_Course_UnitHomeWork = new Course_UnitHomeWork();
            var strWhere_Activity = string.Format(@" Delflag = 0 AND ClassId = {0} AND AccountId = {1} AND UnitContent = {2}",
                               iClassId, iAccountId, iUnitContent);

            var List_Course_UnitHomeWor = Course_UnitHomeWorkbll.GetList(strWhere_Activity, string.Empty);
            if (List_Course_UnitHomeWor != null && List_Course_UnitHomeWor.Count > 0)
            {
                Model_Course_UnitHomeWork = List_Course_UnitHomeWor[0];
                if (Model_Course_UnitHomeWork.Score != null)
                {
                    bolResult = true;
                    dblScore = Model_Course_UnitHomeWork.Score;
                }
            }

            outScore = dblScore;//返回作业得分
            return bolResult;
        }

        /// <summary>
        /// 查看当前活动是否已打分,若已打分则禁止操作
        /// </summary>
        /// <param name="iClassId"></param>
        /// <param name="iAccountId"></param>
        /// <param name="iUnitContent"></param>
        /// <returns></returns>
        private bool GetActivityScore(int iClassId, int iTrainingId, int iAccountId, int iUnitContent)
        {
            var bolResult = false;
            var Member_ClassUnitContentSchedulebll = new Member_ClassUnitContentScheduleBLL();
            var Model_Member_ClassUnitContentSchedule = new Member_ClassUnitContentSchedule();
            var strWhere_Activity = string.Format(@" Delflag = 0 AND Status = 1 AND ClassId = {0} AND TrainingId = {1} AND AccountId = {2} AND UnitContent = {3}",
                               iClassId, iTrainingId, iAccountId, iUnitContent);

            var List_Course_UnitHomeWor = Member_ClassUnitContentSchedulebll.GetList(strWhere_Activity, string.Empty);
            if (List_Course_UnitHomeWor != null && List_Course_UnitHomeWor.Count > 0)
            {
                Model_Member_ClassUnitContentSchedule = List_Course_UnitHomeWor[0];
                if (Model_Member_ClassUnitContentSchedule.score != null)
                {
                    bolResult = true;
                }
            }

            return bolResult;
        }

        #endregion

        #region 公共方法 - 文件下载

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ActionResult DownloadFile(string filePath, string fileName)
        {
            return File(filePath, "file", fileName);
        }

        #endregion

        #region 公共方法 - 获取Url参数加密字符串
        public ActionResult EnCode(string Code)
        {
            string EnCode = Dianda.Common.QueryString.UrlEncrypt(Code);
            return Json(new { Data = EnCode }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 公共方法 - 刷新活动时间

        /// <summary>
        /// 刷新活动时间记录
        /// </summary>
        /// <param name="ClassId"></param>
        /// <param name="TrainingId"></param>
        /// <param name="UnitContent"></param>
        /// <returns></returns>
        public ActionResult RefashTimeRecord(int ClassId, int TrainingId, int UnitContent, int RecordId)
        {
            int iUnitId = 0, iUnitType = 0, iAccountId = 0;
            double dblContentTimeLength = 0;
            var TimeRecordBll = new Member_ClassContentTimeRecordBLL();
            var UnitContentBll = new Course_UnitContentBLL();

            var Model_UnitContent = UnitContentBll.GetModel(UnitContent, string.Empty);
            if (Model_UnitContent == null)
                return View();

            iAccountId = Code.SiteCache.Instance.LoginInfo.UserId;
            iUnitId = Model_UnitContent.UnitId == null ? 0 : Model_UnitContent.UnitId.Value;
            iUnitType = Model_UnitContent.UnitType;
            dblContentTimeLength = Model_UnitContent.TimeLength == null ? 0 : Model_UnitContent.TimeLength.Value;

            var Model_TimeRecord = TimeRecordBll.GetModel(RecordId, string.Empty);
            if (Model_TimeRecord != null)
            {
                Model_TimeRecord.EndTime = DateTime.Now;
                TimeRecordBll.Update(Model_TimeRecord);

                var startDateTime = Model_TimeRecord.StartTime.Value.AddMinutes(dblContentTimeLength);
                var dtCompare = DateTime.Compare(Model_TimeRecord.EndTime.Value, startDateTime);
                if (dtCompare >= 0)//若结束时间 >= 开始时间 + 活动限时,即完成该活动
                {
                    //更新活动进度
                    this.UpdateOverallProgress(iUnitId, ClassId, TrainingId, iAccountId, UnitContent);
                    //若活动是阅读或者视频[1文本，2影音教材，3讨论，4作业，5测试，6结业考试]
                    if (iUnitType == 1 || iUnitType == 2)
                    {
                        this.ScoreSet(1, UnitContent, ClassId, TrainingId);//设置分数
                    }
                }
            }

            return View();
        }

        /// <summary>
        /// 添加一条数据记录到 Member_ClassContentTimeRecord 表,作为学习日志
        /// </summary>
        /// <param name="ClassId"></param>
        /// <param name="TrainingId"></param>
        /// <param name="UnitContent"></param>
        /// <returns></returns>
        private int InsertClassContentTimeRecord(int ClassId, int TrainingId, int UnitContent)
        {
            var TimeRecordBll = new Member_ClassContentTimeRecordBLL();
            var Model_TimeRecord = new Member_ClassContentTimeRecord();
            Model_TimeRecord.ClassId = ClassId;
            Model_TimeRecord.TrainingId = TrainingId;
            Model_TimeRecord.AccountId = Code.SiteCache.Instance.LoginInfo.UserId; ;
            Model_TimeRecord.UnitContent = UnitContent;
            Model_TimeRecord.StartTime = DateTime.Now;
            Model_TimeRecord.EndTime = null;
            Model_TimeRecord.Delflag = false;
            Model_TimeRecord.CreateDate = DateTime.Now;

            TimeRecordBll.AddRecord(Model_TimeRecord);

            return Model_TimeRecord.Id;
        }

        #endregion
    }
}
