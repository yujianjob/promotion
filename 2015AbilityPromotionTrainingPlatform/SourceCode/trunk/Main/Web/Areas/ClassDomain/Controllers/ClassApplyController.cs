﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Code;
using Dianda.AP.BLL;
using Dianda.AP.Model;
using Web.Attributes;

namespace Web.Areas.ClassDomain.Controllers
{
    //班级申请相关
    public class ClassApplyController : Controller
    {

        //班级首页
        public ActionResult Index()
        {
            var fkBll = new Traning_InfoFkBLL();
            var traningBll = new Training_PlanBLL();
            var traningDBll = new Traning_DetailBLL();

            var organ = new Organ_DetailBLL().GetModel(SiteCache.Instance.ManageOrganId);
            if (organ.OType == 1)//培训机构职能选择自己机构开设的课程
            {
                ViewBag.CourseList = traningDBll.GetList(" OrganId =" + organ.Id + " and Delflag=0 and Display=1 and status=5", "");
                //(, "");" Range=2"
            }
            else//进修学院则可以选择自己机构开设的课程，也可以选择自己机构所属的区县下的所有课程。通过Traning_Detail.ParentOrganId。如果Range=2，则表面是市级共享课程。则可以无条件选择。
            {
                ViewBag.CourseList = traningDBll.GetList(" ((OrganId =" + organ.Id + " or ParentOrganId=" + organ.Id + ")  ) and Delflag=0 and Display=1 and status=5", "");
            }//or Range=2
            ViewBag.OrganId = SiteCache.Instance.ManageOrganId;
            ViewBag.PartitionId = organ.PartitionId;
            ViewBag.PlanList = DataTableToListHelper<Dianda.AP.Model.Training_Plan>.ConvertToModel(traningBll.GetList("  Delflag=0 and Display=1 and IsOpen=1").Tables[0]);
            ViewBag.ClassesList = fkBll.GetList(" CategoryType=5 and Delflag=0 and Display=1 ", "Sort desc");
            ViewBag.SubjectList = fkBll.GetList(" CategoryType=3 and Delflag=0 and Display=1 ", "Sort desc");
            ViewBag.StudyLevelList = fkBll.GetList(" CategoryType=4 and Delflag=0 and Display=1 ", "Sort desc");
            ViewBag.TeachGradeList = fkBll.GetList(" CategoryType=7 and Delflag=0 and Display=1 ", "Sort desc");
            ViewBag.TeachRankList = fkBll.GetList(" CategoryType=8 and Delflag=0 and Display=1 ", "Sort desc");


            return View();
        }

        //根据选择的课程返回学校范围
        public JsonResult GetTrainingForm(string courseid)
        {
            try
            {
                var course = new Traning_DetailBLL().GetModel(courseid.ToInt(), "");

                return Json(new { Code = course.TrainingForm }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Code = -1, Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //根据选择的课程返回学校范围
        public JsonResult GetSchoolByCourse(string courseid)
        {
            try
            {
                var course = new Traning_DetailBLL().GetModel(courseid.ToInt(), "");
                var organBll = new Organ_DetailBLL();
                var list = new List<Organ_Detail>();
                if (course.Range == 1) //区级
                {
                    list = organBll.GetListModel(" Delflag=0 and  OType IN ( 1, 2, 3 ) and PartitionId=" + SiteCache.Instance.LoginInfo.PartitionId + " and ParentId=" + SiteCache.Instance.ManageOrganId);
                }
                else//市级
                {
                    list = organBll.GetShiOrganDetailList();
                }
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Code = -1, Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //新建班级
        [ValidateInput(false)]
        public JsonResult CreateClass(string Title, string TraningId, string PlanId, string SignUpStartTime, string SignUpEndTime
            , string OpenClassFrom, string OpenClassTo, string ClassForm, string People, string LimitPeopleCnt, string Address
            , string Content, string Subject, string StudyLevel, string TeachGrade, string TeachRank, string OrganRange
            , string OrganId, string Status, string PartitionId)
        {
            try
            {
                var model = new Dianda.AP.Model.Class_Detail();
                model.Title = Title;
                model.TraningId = TraningId.ToInt();
                model.PlanId = PlanId.ToInt();
                model.SignUpStartTime = SignUpStartTime.ToDateTime();
                model.SignUpEndTime = SignUpEndTime.ToDateTime();
                model.OpenClassFrom = OpenClassFrom.ToDateTime();
                model.OpenClassTo = OpenClassTo.ToDateTime();
                model.ClassForm = ClassForm.ToInt();
                model.People = People.ToInt();
                model.LimitPeopleCnt = LimitPeopleCnt.ToInt();
                model.Address = Address;
                model.Content = Content;
                model.Subject = Subject.Contains("all") ? true : false;
                model.StudyLevel = StudyLevel.Contains("all") ? true : false; ;
                model.TeachGrade = TeachGrade.Contains("all") ? true : false; ;
                model.TeachRank = TeachRank.Contains("all") ? true : false;
                model.OrganRange = OrganRange.Contains("all") ? "0" : OrganRange; ;
                model.ManagerId = SiteCache.Instance.ManagerId;
                model.Status = Status.ToInt();
                model.CreateDate = DateTime.Now;
                model.OrganId = OrganId.ToInt();
                model.PartitionId = PartitionId.ToInt();
                model.Display = true;
                var bll = new Dianda.AP.BLL.Class_DetailBLL();
                var cid = bll.Add(model);

                if (!model.Subject)//不是全部则建立关联数据
                {
                    var arr = Subject.Split(',');
                    var subject = new Class_TeachSubjectBLL();

                    foreach (var item in arr)
                    {
                        var classSubject = new Dianda.AP.Model.Class_TeachSubject();
                        classSubject.ClassId = cid;
                        classSubject.CreateDate = DateTime.Now;
                        classSubject.Delflag = false;
                        classSubject.TeachSubject = item.ToInt();
                        subject.Add(classSubject);
                    }
                }
                if (!model.StudyLevel)//不是全部则建立关联数据
                {
                    var arr = StudyLevel.Split(',');

                    foreach (var item in arr)
                    {
                        var section = new Class_StudySectionBLL();
                        var teachSection = new Dianda.AP.Model.Class_StudySection();
                        teachSection.ClassId = cid;
                        teachSection.CreateDate = DateTime.Now;
                        teachSection.Delflag = false;
                        teachSection.StudySection = item.ToInt();
                        section.Add(teachSection);
                    }
                }
                if (!model.TeachGrade)//不是全部则建立关联数据
                {
                    var arr = TeachGrade.Split(',');
                    var section = new Class_TeachGradeBLL();
                    foreach (var item in arr)
                    {
                        var classSection = new Dianda.AP.Model.Class_TeachGrade();
                        classSection.ClassId = cid;
                        classSection.CreateDate = DateTime.Now;
                        classSection.Delflag = false;
                        classSection.TeachGrade = item.ToInt();
                        section.Add(classSection);
                    }
                }
                if (!model.TeachRank)//不是全部则建立关联数据
                {
                    var arr = TeachRank.Split(',');
                    var section = new Class_TeachRankBLL();
                    foreach (var item in arr)
                    {
                        var classSection = new Dianda.AP.Model.Class_TeachRank();
                        classSection.ClassId = cid;
                        classSection.CreateDate = DateTime.Now;
                        classSection.Delflag = false;
                        classSection.TeachRank = item.ToInt();
                        section.Add(classSection);
                    }
                }
                if (Status == "2")//为提交状态时，新建审核记录
                {
                    var apply = new Dianda.AP.Model.Class_ApplyApplication();
                    apply.ClassId = cid;
                    apply.AccountId = SiteCache.Instance.LoginInfo.UserId;
                    apply.Remark = "提交审核";
                    apply.CreateDate = DateTime.Now;
                    var applyBll = new Class_ApplyApplicationBLL();
                    applyBll.Add(apply);
                }
                return Json(new { Code = 0, Msg = "提交成功" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Code = -1, Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //班级操作
        public ActionResult ClassAction(string strAction, int Id, int? Instructor)
        {
            try
            {
                var bll = new Class_DetailBLL();
                var model = bll.GetModel(Id.ToInt());
                switch (strAction)
                {
                    case "clear"://清除
                        model.Delflag = true;
                        break;
                    case "complete"://结业
                        model.Status = 6;
                        break;
                    case "cancel"://撤销
                        model.Status = 1;
                        break;
                    case "setInstructor"://设置辅导员
                        model.Instructor = Instructor.Value;
                        break;
                    case "start"://开班
                        var traingModel = new Traning_DetailBLL().GetModel(model.TraningId, "");
                        if (traingModel.OutSideType == -1)//内部课程要有课程信息及比例
                        {
                            var list = new Course_DetailBLL().GetList(" Delflag=0 and TrainingId=" + model.TraningId, "");
                            if (list == null || list.Count == 0)
                                return Json(new { Code = -1, Msg = "请设置课程信息!" }, JsonRequestBehavior.AllowGet);
                            var courseDetail = list.First();
                            if (courseDetail.ReadingRate + courseDetail.DisscusRate + courseDetail.HomeWorkRate
                                + courseDetail.QuestionRate + courseDetail.TestingRate + courseDetail.CommentRate != 100)
                            {
                                return Json(new { Code = -1, Msg = "请设置课程考核比例!" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        model.Status = 5;
                        break;

                }

                bll.Update(model);

                return Json(new { Code = 0, Msg = "提交成功" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Code = -1, Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //编辑班级页面
        public ActionResult EditClassDetail(string id)
        {
            int param_id = Dianda.Common.QueryString.Decrypt(id).ToInt();
            var fkBll = new Traning_InfoFkBLL();
            var traningBll = new Training_PlanBLL();
            var traningDBll = new Traning_DetailBLL();
            var bll = new Class_DetailBLL();
            var model = bll.GetModel(param_id);
            var organ = new Organ_DetailBLL().GetModel(SiteCache.Instance.ManageOrganId);
            if (organ.OType == 1)//培训机构职能选择自己机构开设的课程
            {
                ViewBag.CourseList = traningDBll.GetList(" OrganId =" + organ.Id + " and Delflag=0 and Display=1 ", "");
            }
            else//进修学院则可以选择自己机构开设的课程，也可以选择自己机构所属的区县下的所有课程。通过Traning_Detail.ParentOrganId。如果Range=2，则表面是市级共享课程。则可以无条件选择。
            {
                ViewBag.CourseList = traningDBll.GetList(" ((OrganId =" + organ.Id + " or ParentOrganId=" + organ.Id + ") or Range=2 ) and Delflag=0 and Display=1 ", "");
            }
            ViewBag.OrganId = SiteCache.Instance.ManageOrganId;
            ViewBag.PartitionId = organ.PartitionId;
            ViewBag.PlanList = DataTableToListHelper<Dianda.AP.Model.Training_Plan>.ConvertToModel(traningBll.GetList("  Delflag=0 and Display=1 and IsOpen=1").Tables[0]);
            ViewBag.ClassesList = fkBll.GetList(" CategoryType=5 and Delflag=0 and Display=1 ", "Sort desc");
            ViewBag.SubjectList = fkBll.GetList(" CategoryType=3 and Delflag=0 and Display=1 ", "Sort desc");
            ViewBag.Subject = model.Subject;
            ViewBag.existSubjectList = new Class_TeachSubjectBLL().GetList(" Delflag=0 and ClassId=" + param_id, "");
            ViewBag.StudyLevelList = fkBll.GetList(" CategoryType=4 and Delflag=0 and Display=1 ", "Sort desc");
            ViewBag.StudyLevel = model.StudyLevel;
            ViewBag.existStudyLevel = new Class_StudySectionBLL().GetList(" Delflag=0 and ClassId=" + param_id, "");
            ViewBag.TeachGradeList = fkBll.GetList(" CategoryType=7 and Delflag=0 and Display=1 ", "Sort desc");
            ViewBag.TeachGrade = model.TeachGrade;
            ViewBag.existTeachGrade = new Class_TeachGradeBLL().GetList(" Delflag=0 and ClassId=" + param_id, "");
            ViewBag.TeachRankList = fkBll.GetList(" CategoryType=8 and Delflag=0 and Display=1 ", "Sort desc");
            ViewBag.TeachRank = model.TeachRank;
            ViewBag.existTeachRank = new Class_TeachRankBLL().GetList(" Delflag=0 and ClassId=" + param_id, "");

            var course = new Traning_DetailBLL().GetModel(model.TraningId, "");
            var organBll = new Organ_DetailBLL();
            var list = new List<Organ_Detail>();

            //if (course.Range == 1) //区级
            //{
            //    list = organBll.GetListModel("  OType IN ( 1, 2, 3 ) and PartitionId=" + SiteCache.Instance.LoginInfo.PartitionId + " and ParentId=" + SiteCache.Instance.ManageOrganId);
            //}
            //else//市级
            //{
            //    list = organBll.GetShiOrganDetailList();
            //}
            list = organBll.GetListModel(" id in ( "+model.OrganRange+")");
            
            ViewBag.schoolList = list;
            ViewBag.OrangRange = model.OrganRange;
            return View(model);

        }

        //编辑班级
        [ValidateInput(false)]
        public JsonResult EditClass(string id, string Title, string TraningId, string PlanId, string SignUpStartTime, string SignUpEndTime
            , string OpenClassFrom, string OpenClassTo, string ClassForm, string LimitPeopleCnt, string Address
            , string Content, string Subject, string StudyLevel, string TeachGrade, string TeachRank, string OrganRange
            , string Status)
        {
            try
            {
                var bll = new Class_DetailBLL();
                var model = bll.GetModel(id.ToInt());
                model.Title = Title;
                model.TraningId = TraningId.ToInt();
                model.PlanId = PlanId.ToInt();
                model.SignUpStartTime = SignUpStartTime.ToDateTime();
                model.SignUpEndTime = SignUpEndTime.ToDateTime();
                model.OpenClassFrom = OpenClassFrom.ToDateTime();
                model.OpenClassTo = OpenClassTo.ToDateTime();
                model.ClassForm = ClassForm.ToInt();
                model.People = 0;
                model.LimitPeopleCnt = LimitPeopleCnt.ToInt();
                model.Address = Address;
                model.Content = Content;
                model.Subject = Subject.Contains("all") ? true : false;
                model.StudyLevel = StudyLevel.Contains("all") ? true : false; ;
                model.TeachGrade = TeachGrade.Contains("all") ? true : false; ;
                model.TeachRank = TeachRank.Contains("all") ? true : false;
                model.OrganRange = OrganRange.Contains("all") ? "0" : OrganRange; ;
                model.ManagerId = SiteCache.Instance.ManagerId;
                model.Status = Status.ToInt();
                model.CreateDate = DateTime.Now;

                bll.Update(model);

                Member_ClassRegisterBLL member_ClassRegisterBLL = new Member_ClassRegisterBLL();
                List<Member_ClassRegister> member_ClassRegisterList = member_ClassRegisterBLL.GetList("ClassId='" + id + "'", "");
                if (member_ClassRegisterList != null && member_ClassRegisterList.Count > 0)
                {
                    foreach (Member_ClassRegister m in member_ClassRegisterList)
                    {
                        m.Delflag = true;
                        member_ClassRegisterBLL.Update(m);
                    }
                }

                if (!model.StudyLevel)//不是全部则建立关联数据
                {

                    var arr = StudyLevel.Split(',');
                    var section = new Class_StudySectionBLL();

                    var oldSection = section.GetList(" Delflag=0 and ClassId=" + model.Id, "");
                    foreach (var old in oldSection)
                    {
                        var oldId = old.StudySection.ToString();
                        var exists = arr.Where(s => s == oldId);
                        if (exists == null || exists.Count() == 0)//旧的在新的里面不存，则删除
                        {
                            old.Delflag = true;
                            section.Update(old);
                        }
                    }
                    foreach (var item in arr)//新的在旧的里面不存在，则新加
                    {
                        var newid = item.ToInt();
                        var exists = oldSection.Where(s => s.StudySection == newid && s.Delflag == false);
                        if (exists == null || exists.Count() == 0)//
                        {
                            var classSection = new Dianda.AP.Model.Class_StudySection();
                            classSection.ClassId = model.Id;
                            classSection.CreateDate = DateTime.Now;
                            classSection.Delflag = false;
                            classSection.StudySection = item.ToInt();
                            section.Add(classSection);
                        }

                    }
                }
                if (!model.Subject)//不是全部则建立关联数据
                {
                    var arr = Subject.Split(',');
                    var section = new Class_TeachSubjectBLL();
                    var oldSection = section.GetList(" Delflag=0 and ClassId=" + model.Id, "");
                    foreach (var old in oldSection)
                    {
                        var oldId = old.TeachSubject.ToString();
                        var exists = arr.Where(s => s == oldId);
                        if (exists == null || exists.Count() == 0)//旧的在新的里面不存，则删除
                        {
                            old.Delflag = true;
                            section.Update(old);
                        }
                    }
                    foreach (var item in arr)
                    {
                        var newid = item.ToInt();
                        var exists = oldSection.Where(s => s.TeachSubject == newid && s.Delflag == false);
                        if (exists == null || exists.Count() == 0)//
                        {
                            var teachSubject = new Dianda.AP.Model.Class_TeachSubject();
                            teachSubject.ClassId = model.Id;
                            teachSubject.CreateDate = DateTime.Now;
                            teachSubject.Delflag = false;
                            teachSubject.TeachSubject = item.ToInt();
                            section.Add(teachSubject);
                        }
                    }
                }
                if (!model.TeachGrade)//不是全部则建立关联数据
                {
                    var arr = TeachGrade.Split(',');
                    var section = new Class_TeachGradeBLL();
                    var oldSection = section.GetList(" Delflag=0 and ClassId=" + model.Id, "");
                    foreach (var old in oldSection)
                    {
                        var oldId = old.TeachGrade.ToString();
                        var exists = arr.Where(s => s == oldId);
                        if (exists == null || exists.Count() == 0)//旧的在新的里面不存，则删除
                        {
                            old.Delflag = true;
                            section.Update(old);
                        }
                    }
                    foreach (var item in arr)
                    {
                        var newid = item.ToInt();
                        var exists = oldSection.Where(s => s.TeachGrade == newid && s.Delflag == false);
                        if (exists == null || exists.Count() == 0)//
                        {
                            var classSection = new Dianda.AP.Model.Class_TeachGrade();
                            classSection.ClassId = model.Id;
                            classSection.CreateDate = DateTime.Now;
                            classSection.Delflag = false;
                            classSection.TeachGrade = item.ToInt();
                            section.Add(classSection);
                        }
                    }
                }

                if (!model.TeachRank)//不是全部则建立关联数据
                {
                    var arr = TeachRank.Split(',');
                    var section = new Class_TeachRankBLL();
                    var oldSection = section.GetList(" Delflag=0 and ClassId=" + model.Id, "");
                    foreach (var old in oldSection)
                    {
                        var oldId = old.TeachRank.ToString();
                        var exists = arr.Where(s => s == oldId);
                        if (exists == null || exists.Count() == 0)//旧的在新的里面不存，则删除
                        {
                            old.Delflag = true;
                            section.Update(old);
                        }
                    }
                    foreach (var item in arr)
                    {
                        var newid = item.ToInt();
                        var exists = oldSection.Where(s => s.TeachRank == newid && s.Delflag == false);
                        if (exists == null || exists.Count() == 0)//
                        {
                            var classSection = new Dianda.AP.Model.Class_TeachRank();
                            classSection.ClassId = model.Id;
                            classSection.CreateDate = DateTime.Now;
                            classSection.Delflag = false;
                            classSection.TeachRank = item.ToInt();
                            section.Add(classSection);
                        }
                    }
                }

                if (Status == "2")//为提交状态时，新建审核记录
                {
                    var apply = new Dianda.AP.Model.Class_ApplyApplication();
                    apply.ClassId = id.ToInt();
                    apply.AccountId = SiteCache.Instance.LoginInfo.UserId;
                    apply.Remark = "提交审核";
                    apply.CreateDate = DateTime.Now;
                    var applyBll = new Class_ApplyApplicationBLL();
                    applyBll.Add(apply);
                }
                return Json(new { Code = 0, Msg = "提交成功" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Code = -1, Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //班级列表
        [ValidateInput(false)]
        public ActionResult ClassList(string param_plan, string param_subject, int? param_status, string param_searchTxt, int pageIndex = 1)
        {
            int totalPage;
            var fkBll = new Traning_InfoFkBLL();
            var traningBll = new Training_PlanBLL();
            ViewBag.PlanList = DataTableToListHelper<Dianda.AP.Model.Training_Plan>.ConvertToModel(traningBll.GetList("  Delflag=0 and Display=1 ").Tables[0]);
            ViewBag.SubjectList = fkBll.GetList(" CategoryType=3 and Delflag=0 and Display=1 ", "Sort desc");
            var organ = new Organ_DetailBLL().GetModel(SiteCache.Instance.ManageOrganId);
            //组织分区下未删除状态
            string where = " Delflag=0 and OrganId=  " + SiteCache.Instance.ManageOrganId + " and PartitionId=" + organ.PartitionId;
            if (!string.IsNullOrEmpty(param_plan))//学期计划
            {
                where += " and PlanId =" + Dianda.Common.QueryString.Decrypt(param_plan);
            }
            if (!string.IsNullOrEmpty(param_subject))//学科
            {
                where += " and (subject=1 or exists( select 1 from Class_TeachSubject where ClassId=Class_Detail.id and TeachSubject=" + Dianda.Common.QueryString.Decrypt(param_subject) + "))";
            }
            if (param_status.HasValue)//状态
            {
                where += " and Status =" + param_status.Value;
            }
            if (!string.IsNullOrEmpty(param_searchTxt))//查询条件
            {
                where += " and( Title like '%" + ExtendHelper.Split(param_searchTxt) + "%' or exists( SELECT 1 FROM dbo.Traning_Detail WHERE Title LIKE '%"
                    + ExtendHelper.Split(param_searchTxt) + "%' AND id=Class_Detail.TraningId))";
            }
            var list = DataTableToListHelper<Dianda.AP.Model.Class_Detail>.ConvertToModel(PagingQueryBll.GetPagingDataTable("Class_Detail", where, "id", pageIndex, out totalPage));

            ViewBag.pageIndex = pageIndex;
            ViewBag.totalPage = totalPage;
            ViewBag.basecount = 10;
            ViewBag.instructorList = DataTableToListHelper<Dianda.AP.Model.Member_Account>.ConvertToModel(new Member_AccountBLL().GetAccountByMannagerGroup(organ.PartitionId, organ.Id, 6));
            return View(list);
        }

        //所有辅导员
        public JsonResult getAllInstructor(int type,int pageIndex=1)
        {
            var organ = new Organ_DetailBLL().GetModel(SiteCache.Instance.ManageOrganId);
            IList < Dianda.AP.Model.Member_Account > list;
            if (type == 0)
                list = DataTableToListHelper<Dianda.AP.Model.Member_Account>.ConvertToModel(new Member_AccountBLL().GetAccountByMannagerGroup(0, 0, 6));
            else
                list = DataTableToListHelper<Dianda.AP.Model.Member_Account>.ConvertToModel(new Member_AccountBLL().GetAccountByMannagerGroup(organ.PartitionId, organ.Id, 6));

            return Json(new { instructorList = list.Skip((pageIndex-1)*8).Take(8), totalPage = Math.Ceiling( Convert.ToDouble( list.Count)/8) }, JsonRequestBehavior.AllowGet);
        }

         //组织
        public JsonResult GetOrganRecord(string txtSearch)
        {
            var organ = DataTableToListHelper<Organ_Detail>.ConvertToModel(new Organ_DetailBLL().GetList(string.IsNullOrEmpty(txtSearch) ? " OType in (1,5)" : " Title like '%" + txtSearch.Trim() + "%'").Tables[0]); 
            
            return Json(organ, JsonRequestBehavior.AllowGet);
        }

        //组织
        public JsonResult GetChildOrganRecord(int pid)
        {
            var organ = DataTableToListHelper<Organ_Detail>.ConvertToModel(new Organ_DetailBLL().GetList(" ParentId ="+pid).Tables[0]);

            return Json(organ, JsonRequestBehavior.AllowGet);
        }
    }
}
