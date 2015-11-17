﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Code;
using Dianda.AP.BLL;
using Dianda.AP.Model;
using System.Text;
using Web.Attributes;

namespace Web.Areas.ClassDomain.Controllers
{
    //班级教学
    public class ClassTeacherController : Controller
    {
        public class KeyValue
        {
            public string Title { get; set; }
            public string Id { get; set; }
        }

        //班级列表
        [ValidateInput(false)]
        public ActionResult ClassTeacherList(int? param_plan, int? param_subject, int? param_status, string param_searchTxt, int pageIndex = 1)
        {
            int totalPage;
            var fkBll = new Traning_InfoFkBLL();
            var traningBll = new Training_PlanBLL();
            ViewBag.PlanList = DataTableToListHelper<Dianda.AP.Model.Training_Plan>.ConvertToModel(traningBll.GetList("  Delflag=0 and Display=1 ").Tables[0]);

            ViewBag.SubjectList = fkBll.GetList(" CategoryType=3 and Delflag=0 and Display=1 ", "Sort desc");
            //为审核通过，已开班，已结业，已暂停。Display=1，delflag=0.
            string where = " Display=1 and  Delflag=0 and status in (3,5,6) ";
            var gid = SiteCache.Instance.GroupId;
            if (gid == 6)//辅导员
            {
                where += " and Instructor=" + SiteCache.Instance.ManagerId;
            }
            else//教师
            {
                where += " and exists (SELECT 1 FROM dbo.Traning_Teacher WHERE  Traning_Teacher.TraningId=dbo.Class_Detail.TraningId AND PlatformManagerId=" + SiteCache.Instance.ManagerId + ")";
            }
            if (param_plan.HasValue)//学期计划
            {
                where += " and PlanId =" + param_plan.Value;
            }
            if (param_subject.HasValue)//学科
            {
                where += " and (subject=1 or (subject= 0 and exists( select 1 from Class_TeachSubject where Delflag=0 and ClassId=Class_Detail.id and TeachSubject=" + param_subject.Value + ")))";
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
            return View(list);
        }

        //进度列表
        public ActionResult ClassProgress(string classId, int? isPass, int pageIndex = 1)
        {
            var param_classId = Dianda.Common.QueryString.Decrypt(classId).ToInt();
            int totalPage;

            string where = " Delflag=0 and Status=4 and ClassId= " + param_classId;

            if (isPass.HasValue)
                where += " and isnull(Result,0) =" + isPass.Value;

            var list = DataTableToListHelper<Dianda.AP.Model.Member_ClassRegister>.ConvertToModel(PagingQueryBll.GetPagingDataTable("Member_ClassRegister", where, "id", pageIndex, out totalPage));

            ViewBag.classId = classId;

            ViewBag.pageIndex = pageIndex;
            ViewBag.totalPage = totalPage;
            ViewBag.basecount = 10;

            var classModel = new Class_DetailBLL().GetModel(param_classId);
            if (classModel != null)
            {
                ViewBag.CourseName = new Traning_DetailBLL().GetModel(classModel.TraningId, "").Title;
                ViewBag.ClassName = classModel.Title;
            }
            return View(list);
        }

        //分组列表
        public ActionResult ClassGroup(string classId, int groupId = 0, int pageIndex = 1)
        {
            var param_classId = Dianda.Common.QueryString.Decrypt(classId).ToInt();
            IList<Member_ClassRegister> list = new List<Member_ClassRegister>();
            int totalPage = 0;

            string where = " mb.ClassId = " + param_classId + " and  cg.ClassId= " + param_classId + " AND cgm.Delflag=0 AND mb.Delflag=0";

            if (groupId == 0)//全部组
            {
                list = DataTableToListHelper<Dianda.AP.Model.Member_ClassRegister>
                .ConvertToModel(PagingQueryBll.GetPagingDataTable(@"dbo.Class_GroupMember cgm
LEFT JOIN dbo.Class_Group cg ON cgm.GroupId=cg.Id
LEFT JOIN dbo.Member_ClassRegister mb ON cgm.AccountId=mb.AccountId", where, "cgm.CreateDate", pageIndex, out totalPage, "mb.*,CONVERT(VARCHAR,cg.ClassId)+'|'+CONVERT(VARCHAR,cg.id) AS ClassId_GroupId", 15));
            }
            if (groupId > 0)//单组
            {
                where += " and cgm.GroupId=" + groupId;
                list = DataTableToListHelper<Dianda.AP.Model.Member_ClassRegister>
                .ConvertToModel(PagingQueryBll.GetPagingDataTable(@"dbo.Class_GroupMember cgm
LEFT JOIN dbo.Class_Group cg ON cgm.GroupId=cg.Id
LEFT JOIN dbo.Member_ClassRegister mb ON cgm.AccountId=mb.AccountId", where, "cgm.CreateDate", pageIndex, out totalPage, "mb.*,CONVERT(VARCHAR,cg.ClassId)+'|'+CONVERT(VARCHAR,cg.id) AS ClassId_GroupId", 15));
            }
            if (groupId < 0)//无组
            {
                where = @" NOT EXISTS ( SELECT 1
                     FROM   dbo.Class_Group cg
                            JOIN dbo.Class_GroupMember cgm ON cg.Id = cgm.GroupId
                     WHERE  cg.ClassId = t.ClassId AND t.AccountId=cgm.AccountId
                    AND t.Status=4 AND t.ClassId=" + param_classId + " AND cg.Delflag=0 AND cgm.Delflag=0)" + " AND t.Status=4 AND t.ClassId=" + param_classId;
                list = DataTableToListHelper<Dianda.AP.Model.Member_ClassRegister>
               .ConvertToModel(PagingQueryBll.GetPagingDataTable(@"dbo.Member_ClassRegister t
LEFT JOIN dbo.Member_Account m ON t.AccountId=m.id
", where, "t.CreateDate", pageIndex, out totalPage, "t.*,'' AS ClassId_GroupId", 15));
            }

            var gBll = new Dianda.AP.BLL.Class_GroupBLL();
            var allGroup = gBll.GetList(" ClassId=" + param_classId + " and Delflag=0 AND Display=1", "");

            //无组人员
            //            where = @" NOT EXISTS ( SELECT 1
            //                     FROM   dbo.Class_Group cg
            //                            JOIN dbo.Class_GroupMember cgm ON cg.Id = cgm.GroupId
            //                     WHERE  cg.ClassId = t.ClassId AND t.AccountId=cgm.AccountId
            //                            AND t.Status=4 AND t.ClassId=" + classId + ")" + " AND t.Status=4 AND t.ClassId=" + classId;
            //            var noGrouper = DataTableToListHelper<Dianda.AP.Model.Member_BaseInfo>
            //           .ConvertToModel(PagingQueryBll.GetPagingDataTable(@"dbo.Member_ClassRegister t
            //LEFT JOIN dbo.Member_Account m ON t.AccountId=m.id
            //LEFT JOIN dbo.Member_BaseInfo mb ON m.Id=mb.AccountId", where, "mb.CreateDate", pageIndex, out totalPage, "mb.*,'' AS ClassId_GroupId"));
            var noGrouper = DataTableToListHelper<Dianda.AP.Model.Member_ClassRegister>
                           .ConvertToModel(new Member_ClassRegisterBLL().GetNoGroupper(param_classId));
            ViewBag.noGrouper = noGrouper;

            ViewBag.GroupList = allGroup;
            ViewBag.classId = classId;
            ViewBag.pageIndex = pageIndex;
            ViewBag.totalPage = totalPage;
            ViewBag.basecount = 15;

            var classModel = new Class_DetailBLL().GetModel(param_classId);
            if (classModel != null)
            {
                ViewBag.CourseName = new Traning_DetailBLL().GetModel(classModel.TraningId, "").Title;
                ViewBag.ClassName = classModel.Title;

                var traningTeacher = new Traning_TeacherBLL().GetList(" Delflag=0 AND TraningId=" + classModel.TraningId, "");
                var teacherAccount = (traningTeacher != null && traningTeacher.Count > 0) ? new PlatformManager_DetailBLL().GetModel(traningTeacher.First().PlatformManagerId.Value, "").AccountId : 0;
                ViewBag.TraningTeacher = teacherAccount;
            }


            ViewBag.ManagerId = SiteCache.Instance.ManagerId;

            return View(list);
        }

        //删除班级小组
        public JsonResult DeleteClassGroup(string classId, int groupId)
        {
            try
            {
                var param_classId = Dianda.Common.QueryString.Decrypt(classId).ToInt();
                var bll = new Class_GroupBLL();
                var model = bll.GetModel(groupId, " ClassId=" + param_classId);
                model.Delflag = true;
                bll.Update(model);

                var membll = new Class_GroupMemberBLL();
                var member = membll.GetListModel("GroupId=" + model.Id);
                foreach (var item in member)
                {
                    item.Delflag = true;
                    membll.Update(item);
                }

                return Json(new { Code = 0, Msg = "提交成功" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Code = -1, Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //删除班级小组成员
        public JsonResult DeleteClassGroupMember(string groupId, int accountId)
        {
            try
            {
                var bll = new Class_GroupMemberBLL();
                //var model = bll.GetModel(groupId.Split('|')[1].ToInt(), accountId);
                //model.Delflag = true;
                //bll.Update(model);
                bll.Delete(groupId.Split('|')[1].ToInt(), accountId);
                return Json(new { Code = 0, Msg = "提交成功" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Code = -1, Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //添加小组及成员
        [ValidateInput(false)]
        public JsonResult AddClassGroupMember(string classId, string groupTitle, string accountIds)
        {
            try
            {
                var param_classId = Dianda.Common.QueryString.Decrypt(classId).ToInt();
                var gbll = new Class_GroupBLL();
                var gmbll = new Class_GroupMemberBLL();

                var groupNameCheck = new Class_GroupBLL().GetList(" Delflag=0 and ClassId=" + param_classId + " and Title='" + groupTitle.Trim() + "'", "");
                if (groupNameCheck != null && groupNameCheck.Count > 0)
                    return Json(new { Code = 0, Msg = "班级内存在此小组名称！" }, JsonRequestBehavior.AllowGet);
                var gModel = new Class_Group();
                gModel.ClassId = param_classId;
                gModel.CreateDate = DateTime.Now;
                gModel.Display = true;
                gModel.Delflag = false;
                gModel.Title = groupTitle;
                gbll.Add(gModel);

                var ids = accountIds.Split(',');
                foreach (var item in ids)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        var gmModel = new Class_GroupMember();
                        gmModel.AccountId = item.ToInt();
                        gmModel.GroupId = gModel.Id;
                        gmModel.Delflag = false;
                        gmModel.CreateDate = DateTime.Now;
                        gmbll.Add(gmModel);
                    }
                }

                return Json(new { Code = 0, Msg = "提交成功" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Code = -1, Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //添加小组成员
        [ValidateInput(false)]
        public JsonResult AddMemberToClassGroup(string groupId, string accountIds)
        {
            try
            {
                var gmbll = new Class_GroupMemberBLL();

                var ids = accountIds.Split(',');
                foreach (var item in ids)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        var gmModel = new Class_GroupMember();
                        gmModel.AccountId = item.ToInt();
                        gmModel.GroupId = groupId.ToInt();
                        gmModel.Delflag = false;
                        gmModel.CreateDate = DateTime.Now;
                        gmbll.Add(gmModel);
                    }
                }

                return Json(new { Code = 0, Msg = "提交成功" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Code = -1, Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //作业列表
        public ActionResult ClassTask(string classId, string unit, string status, int pageIndex = 1)
        {
            var param_classId = Dianda.Common.QueryString.Decrypt(classId).ToInt();

            IList<Member_ClassRegister> list = new List<Member_ClassRegister> { };

            var TrainingId = new Class_DetailBLL().GetModel(param_classId).TraningId;

            //作业单元
            var classUnit = new Course_UnitContentBLL().GetList(" Display=1 and  UnitType=4 and delflag=0 AND UnitId IN (SELECT id FROM dbo.Course_UnitDetail WHERE TrainingId=" + TrainingId + ")", "")
                ;
            var unitList = from c in classUnit select c;
            ViewBag.UnitList = unitList;

            int totalPage = 0;

            string param_status = "";//状态

            switch (status)
            {
                case "-1"://未提交
                    param_status = @" and not EXISTS ( SELECT 1
                     FROM   dbo.Course_UnitHomeWork cw
                            JOIN dbo.Course_UnitContent cu ON cw.UnitContent = cu.Id
                     WHERE  m.AccountId = cw.AccountId
                            AND cw.ClassId = m.ClassId
                            AND cu.Id= {1}  and cw.Delflag=0)";
                    break;
                case "0"://单元未得分
                    param_status = @" AND  EXISTS ( SELECT 1
                     FROM   dbo.Course_UnitHomeWork cw
                            JOIN dbo.Course_UnitContent cu ON cw.UnitContent = cu.Id
                     WHERE  m.AccountId = cw.AccountId
                            AND cw.ClassId = m.ClassId
                            AND cu.Id= {1} AND isnull(cw.Score,0)=0 and cw.Delflag=0)
                            
                            AND not EXISTS ( SELECT 1
                     FROM   dbo.Course_UnitHomeWork cw
                            JOIN dbo.Course_UnitContent cu ON cw.UnitContent = cu.Id
                     WHERE  m.AccountId = cw.AccountId
                            AND cw.ClassId = m.ClassId
                            AND cu.Id= {1} AND isnull(cw.Score,0)!=0 and cw.Delflag=0)";
                    break;
                case "1"://单元已得分
                    param_status = @" AND  EXISTS ( SELECT 1
                     FROM   dbo.Course_UnitHomeWork cw
                            JOIN dbo.Course_UnitContent cu ON cw.UnitContent = cu.Id
                     WHERE  m.AccountId = cw.AccountId
                            AND cw.ClassId = m.ClassId
                            AND cu.Id= {1} AND isnull(cw.Score,0)!=0 and cw.Delflag=0)";
                    break;
                default://全部 
                    break;
            }

            var Course_UnitContentBLL = new Course_UnitContentBLL();
            StringBuilder sbSql = new StringBuilder();
            if (string.IsNullOrEmpty(unit))
            {
                foreach (var item in unitList)
                {
                    sbSql.AppendFormat(@"SELECT  m.* ,
                '{1}' as cqId,
                homeworkId = ( SELECT hw.id
                             FROM   dbo.Course_UnitHomeWork hw
                             WHERE  hw.AccountId = m.AccountId
                                    AND hw.ClassId = {0}
                                    AND hw.UnitContent = {1} and hw.Delflag=0
                           )
        FROM    dbo.Member_ClassRegister m
                LEFT JOIN dbo.Class_Detail d ON m.ClassId = d.Id
                LEFT JOIN dbo.Course_UnitDetail ud ON ud.TrainingId = d.TraningId
                LEFT JOIN Course_UnitContent uc ON uc.UnitId = ud.Id
                LEFT JOIN dbo.Member_Account ma ON m.AccountId = ma.Id
        WHERE   m.status=4 AND m.Delflag=0 and m.ClassId = {0}
                AND uc.id = {1} " + param_status, param_classId, item.Id);
                    sbSql.Append(" union ");
                }
            }
            else
            {
                sbSql.AppendFormat(@"SELECT  m.* ,
                '{1}' as cqId,
                homeworkId = ( SELECT hw.id
                             FROM   dbo.Course_UnitHomeWork hw
                             WHERE  hw.AccountId = m.AccountId
                                    AND hw.ClassId = {0}
                                    AND hw.UnitContent = {1} and hw.Delflag=0
                           )
        FROM    dbo.Member_ClassRegister m
                LEFT JOIN dbo.Class_Detail d ON m.ClassId = d.Id
                LEFT JOIN dbo.Course_UnitDetail ud ON ud.TrainingId = d.TraningId
                LEFT JOIN Course_UnitContent uc ON uc.UnitId = ud.Id
                LEFT JOIN dbo.Member_Account ma ON m.AccountId = ma.Id
        WHERE m.status=4 AND m.Delflag=0 and  m.ClassId = {0}
                AND uc.id = {1}" + param_status, param_classId, unit);
            }
            if (sbSql.Length > 0)
            {
                if (sbSql.ToString().Contains("union"))
                {
                    sbSql = sbSql.Remove(sbSql.ToString().LastIndexOf("union"), 5);
                }
                list = DataTableToListHelper<Dianda.AP.Model.Member_ClassRegister>
                                     .ConvertToModel(PagingQueryBll.GetPagingDataTable("(" + sbSql.ToString() + ") as tmp", " 1=1 ", "CreateDate", pageIndex, out totalPage, @"*"));
            }
            ViewBag.classId = classId;
            ViewBag.param_classId = param_classId;
            //分页
            ViewBag.pageIndex = pageIndex;
            ViewBag.totalPage = totalPage;
            ViewBag.basecount = 10;

            var classModel = new Class_DetailBLL().GetModel(param_classId);
            if (classModel != null)
            {
                ViewBag.CourseName = new Traning_DetailBLL().GetModel(classModel.TraningId, "").Title;
                ViewBag.ClassName = classModel.Title;
                ViewBag.planId = classModel.PlanId;
            }


            return View(list);
        }

        //打分
        public JsonResult AddHomeWorkScore(int ObjectId, int classId, int planId, int AccountId, string Val)
        {
            try
            {
                ScoreSetHelper.ScoreSet(ObjectId, 3, classId, planId, AccountId, Val.ToDouble());
                return Json(new { Code = 0, Msg = "提交成功" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Code = -1, Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //小组讨论
        public ActionResult ClassDiscuss(string classId, int? groupId, int unitId = 0, int pageIndex = 1, int pageIndex2 = 1)
        {
            var param_classId = Dianda.Common.QueryString.Decrypt(classId).ToInt();
            int totalPage = 0;
            int totalPage2 = 0;
            var classDetail = new Class_DetailBLL().GetModel(param_classId);

            var topicList = DataTableToListHelper<Dianda.AP.Model.Course_UnitContent>
           .ConvertToModel(PagingQueryBll.GetPagingDataTable(@"  Course_UnitContent
                INNER JOIN Course_UnitDetail ON Course_UnitContent.UnitId = Course_UnitDetail.Id"
           , @" Course_UnitDetail.Display = 1
                                                AND Course_UnitDetail.Delflag = 0
                                                AND Course_UnitContent.Display = 1
                                                AND Course_UnitContent.Delflag = 0
                                                AND Course_UnitDetail.TrainingId = " + classDetail.TraningId + @"
                                                AND UnitType = 3  ", " dbo.Course_UnitDetail.Id", 1, out totalPage, "Course_UnitContent.*", 50));

            ViewBag.topicList = topicList;


            ViewBag.currentTopic = unitId == 0 ? (topicList.Count > 0 ? topicList[0].Content : null) : new Course_UnitContentBLL().GetModel(unitId, "").Content;
            //ViewBag.currentTopicId = unitId == 0 ? (topicList.Count > 0 ? topicList[0].Id : 0) : unitId;

            //班级成员
            var allMember = new Member_ClassRegisterBLL()
                                     .GetList(" ClassId=" + param_classId + " and Delflag=0 and status=4", "")
                                     .GroupBy(s => s.AccountId);
            totalPage2 = allMember.Count();
            ViewBag.classMember = from c in allMember.Skip((pageIndex2 - 1) * 10).Take(10)
                                  select c.Key;

            var paramUnit = unitId == 0 ? (topicList.Count > 0 ? "r.UnitContent=" + topicList[0].Id : " 1=1 ") : " r.UnitContent= " + unitId;
            var where = paramUnit + " and r.ClassId=" + param_classId + " AND r.Delflag=0 AND r.ParentReplyId=0 AND r.Display=1 and r.AccountId!=0";
            if (groupId.HasValue)//有组条件
            {
                if (groupId == 0)
                {
                    where += @" and exists (SELECT 1 FROM  Member_ClassRegister m  
                    INNER JOIN dbo.Class_GroupMember cgm ON m.AccountId = cgm.AccountId
					INNER JOIN dbo.Class_Group cg ON cgm.GroupId=cg.Id
					INNER JOIN dbo.Class_Detail c ON cg.ClassId=c.id 
                    WHERE  r.AccountId = m.AccountId
					AND m.ClassId=r.ClassId
					AND m.status = 4
                    AND m.Delflag = 0)";
                }
                if (groupId == -1)
                {
                    where += @" and not exists (SELECT 1 FROM  Member_ClassRegister m  
                    INNER JOIN dbo.Class_GroupMember cgm ON m.AccountId = cgm.AccountId
					INNER JOIN dbo.Class_Group cg ON cgm.GroupId=cg.Id
					INNER JOIN dbo.Class_Detail c ON cg.ClassId=c.id 
                    WHERE  r.AccountId = m.AccountId
					AND m.ClassId=r.ClassId
					AND m.status = 4
                    AND m.Delflag = 0)";
                }
                if (groupId > 0)
                {
                    where += @" and exists (SELECT 1 FROM  Member_ClassRegister m  
                    INNER JOIN dbo.Class_GroupMember cgm ON m.AccountId = cgm.AccountId
					INNER JOIN dbo.Class_Group cg ON cgm.GroupId=cg.Id
					INNER JOIN dbo.Class_Detail c ON cg.ClassId=c.id 
                    WHERE  r.AccountId = m.AccountId
					AND m.ClassId=r.ClassId
					AND m.status = 4
                    AND m.Delflag = 0 
                    AND cgm.GroupId= " + groupId + ")";
                }

            }
            else//默认全部组
            {
                where += @" and exists (SELECT 1 FROM  Member_ClassRegister m  
                    INNER JOIN dbo.Class_GroupMember cgm ON m.AccountId = cgm.AccountId
					INNER JOIN dbo.Class_Group cg ON cgm.GroupId=cg.Id
					INNER JOIN dbo.Class_Detail c ON cg.ClassId=c.id 
                    WHERE  r.AccountId = m.AccountId
					AND m.ClassId=r.ClassId
					AND m.status = 4
                    AND m.Delflag = 0)";
            }
            //Model
            var list = DataTableToListHelper<Dianda.AP.Model.Course_UnitReplyDetail>
           .ConvertToModel(PagingQueryBll.GetPagingDataTable("Course_UnitReplyDetail r", where, "r.id", pageIndex, out totalPage, "r.*", 3));


            var allGroup = new Class_GroupBLL().GetList(" ClassId=" + param_classId + " and Delflag=0 AND Display=1", "");

            ViewBag.GroupList = allGroup;
            //成员分页
            ViewBag.pageIndex2 = pageIndex2;
            ViewBag.totalPage2 = totalPage2;

            ViewBag.classId = classId;
            ViewBag.param_classId = param_classId;
            //讨论分页
            ViewBag.pageIndex = pageIndex;
            ViewBag.totalPage = totalPage;
            ViewBag.basecount = 3;

            ViewBag.accountId = SiteCache.Instance.LoginInfo.UserId;
            ViewBag.traningId = classDetail.TraningId;

            var classModel = new Class_DetailBLL().GetModel(param_classId);
            if (classModel != null)
            {
                ViewBag.CourseName = new Traning_DetailBLL().GetModel(classModel.TraningId, "").Title;
                ViewBag.ClassName = classModel.Title;

                var traningTeacher = new Traning_TeacherBLL().GetList(" Delflag=0 AND TraningId=" + classModel.TraningId, "");
                var teacherAccount = (traningTeacher != null && traningTeacher.Count > 0) ? new PlatformManager_DetailBLL().GetModel(traningTeacher.First().PlatformManagerId.Value, "").AccountId : 0;
                ViewBag.TraningTeacher = teacherAccount;
            }

            ViewBag.ManagerId = SiteCache.Instance.ManagerId;

            return View(list);
        }

        //删除回复
        public JsonResult DeleteUnitContent(int Id)
        {
            try
            {
                var bll = new Course_UnitReplyDetailBLL();
                var model = bll.GetModel(Id, "");
                model.Delflag = true;
                bll.Update(model);
                return Json(new { Code = 0, Msg = "操作成功" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Code = -1, Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //回复
        public JsonResult ReplyUnitContent(int uId, int classId, int accountId, int pId, string Content)
        {
            try
            {
                var bll = new Course_UnitReplyDetailBLL();
                var rootId = 0;
                rootId = GetRootId(pId);

                var model = new Course_UnitReplyDetail();
                model.UnitContent = uId;
                model.ClassId = classId;
                model.Content = Content;
                model.AccountId = accountId;
                model.Display = true;
                model.ParentReplyId = rootId;
                model.Delflag = false;
                model.CreateDate = DateTime.Now;
                bll.Add(model);
                return Json(new { Code = 0, Msg = "操作成功" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Code = -1, Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //根级回复
        private int GetRootId(int pId)
        {
            if (pId == 0)
                return pId;
            else
            {
                var parent = new Course_UnitReplyDetailBLL().GetModel(pId, "");

                if (parent.ParentReplyId.Value == 0)
                {
                    return parent.Id;
                }
                else
                {
                    var root = new Course_UnitReplyDetailBLL().GetModel(parent.ParentReplyId.Value, "");
                    return root.Id;
                }
            }
        }

        //班级测验
        public ActionResult ClassQuiz(string classId, string unit, string status, int pageIndex = 1)
        {
            var param_classId = Dianda.Common.QueryString.Decrypt(classId).ToInt();

            IList<Member_ClassRegister> list = new List<Member_ClassRegister> { };

            var TrainingId = new Class_DetailBLL().GetModel(param_classId).TraningId;
            //作业单元
            var classUnit = new Course_UnitContentBLL().GetList(" Delflag=0 and Display=1 and   UnitType=5 AND UnitId IN (SELECT id FROM dbo.Course_UnitDetail WHERE TrainingId=" + TrainingId + ")", "")
                ;
            var unitList = from c in classUnit select c;
            ViewBag.UnitList = unitList;
            int totalPage = 0;

            string param_status = "";//状态

            switch (status)
            {
                case "-1"://未提交
                    param_status = @" and not EXISTS ( SELECT 1
                     FROM   dbo.Member_ClassUnitContentSchedule cs
                            JOIN dbo.Course_UnitContent cu ON cs.UnitContent = cu.Id
                     WHERE  m.AccountId = cs.AccountId
                            AND cs.ClassId = m.ClassId
                            AND cu.Id= {1} and cs.Delflag=0)";
                    break;
                case "0"://单元未得分
                    param_status = @" AND  EXISTS ( SELECT 1
                     FROM   dbo.Member_ClassUnitContentSchedule cs
                            JOIN dbo.Course_UnitContent cu ON cs.UnitContent = cu.Id
                     WHERE  m.AccountId = cs.AccountId
                            AND cs.ClassId = m.ClassId
                            AND cu.Id= {1} AND ISNULL(cs.score,0)=0 and cs.Delflag=0) 

                            AND not  EXISTS ( SELECT 1
                     FROM   dbo.Member_ClassUnitContentSchedule cs
                            JOIN dbo.Course_UnitContent cu ON cs.UnitContent = cu.Id
                     WHERE  m.AccountId = cs.AccountId
                            AND cs.ClassId = m.ClassId
                            AND cu.Id= {1} AND ISNULL(cs.score,0)>0 and cs.Delflag=0)";
                    break;
                case "1"://单元已得分
                    param_status = @" AND  EXISTS ( SELECT 1
                     FROM   dbo.Member_ClassUnitContentSchedule cs
                            JOIN dbo.Course_UnitContent cu ON cs.UnitContent = cu.Id
                     WHERE  m.AccountId = cs.AccountId
                            AND cs.ClassId = m.ClassId
                            AND cu.Id= {1} AND ISNULL(cs.score,0)>0 and cs.Delflag=0) ";
                    break;
                default:
                    break;
            }

            StringBuilder sbSql = new StringBuilder();
            if (string.IsNullOrEmpty(unit))
            {
                foreach (var item in unitList)
                {
                    sbSql.AppendFormat(@"SELECT  m.* ,
                '{1}' as cqId,
                homeworkId = ( SELECT hw.id
                             FROM   dbo.Member_ClassUnitContentSchedule hw
                             WHERE  hw.AccountId = m.AccountId
                                    AND hw.ClassId = {0}
                                    AND hw.UnitContent = {1} and hw.Delflag=0
                           )
        FROM    dbo.Member_ClassRegister m
                LEFT JOIN dbo.Class_Detail d ON m.ClassId = d.Id
                LEFT JOIN dbo.Course_UnitDetail ud ON ud.TrainingId = d.TraningId
                LEFT JOIN Course_UnitContent uc ON uc.UnitId = ud.Id
                LEFT JOIN dbo.Member_Account ma ON m.AccountId = ma.Id
        WHERE  m.status=4 AND m.Delflag=0 and m.ClassId = {0}
                AND uc.id = {1} " + param_status, param_classId, item.Id);
                    sbSql.Append(" union ");
                }
            }
            else
            {
                sbSql.AppendFormat(@"SELECT  m.* ,
                '{1}' as cqId,
                homeworkId = ( SELECT hw.id
                             FROM   dbo.Member_ClassUnitContentSchedule hw
                             WHERE  hw.AccountId = m.AccountId
                                    AND hw.ClassId = {0}
                                    AND hw.UnitContent = {1} and hw.Delflag=0
                           )
        FROM    dbo.Member_ClassRegister m
                LEFT JOIN dbo.Class_Detail d ON m.ClassId = d.Id
                LEFT JOIN dbo.Course_UnitDetail ud ON ud.TrainingId = d.TraningId
                LEFT JOIN Course_UnitContent uc ON uc.UnitId = ud.Id
                LEFT JOIN dbo.Member_Account ma ON m.AccountId = ma.Id
        WHERE m.status=4 AND m.Delflag=0 and  m.ClassId = {0}
                AND uc.id = {1}" + param_status, param_classId, unit);
            }
            if (sbSql.Length > 0)
            {
                if (sbSql.ToString().Contains("union"))
                {
                    sbSql = sbSql.Remove(sbSql.ToString().LastIndexOf("union"), 5);
                }
                //Model
                list = DataTableToListHelper<Dianda.AP.Model.Member_ClassRegister>
                                       .ConvertToModel(PagingQueryBll.GetPagingDataTable("(" + sbSql.ToString() + ") as tmp", " 1=1 ", "CreateDate", pageIndex, out totalPage, @"*"));
            }
            ViewBag.classId = classId;
            ViewBag.param_classId = param_classId;
            //分页
            ViewBag.pageIndex = pageIndex;
            ViewBag.totalPage = totalPage;
            ViewBag.basecount = 10;

            var classModel = new Class_DetailBLL().GetModel(param_classId);
            if (classModel != null)
            {
                ViewBag.CourseName = new Traning_DetailBLL().GetModel(classModel.TraningId, "").Title;
                ViewBag.ClassName = classModel.Title;
                ViewBag.planId = classModel.PlanId;
            }


            return View(list);
        }

        //获取学员测验答案列表
        [UrlDecrypt]
        public JsonResult GetQuizAnswerList(int UnitContentId, int AccountId)
        {
            try
            {
                var quizList = new Member_ContentAnswerResultBLL().GetListModel(" Delflag=0 and UnitContent=" + UnitContentId + " and AccountId=" + AccountId);
                return Json(from c in quizList select new { Id = Dianda.Common.QueryString.UrlEncrypt(c.Id), CreateDate = c.CreateDate.ToString("yyyy-MM-dd HH:mm") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Code = -1, Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //测验打分
        public JsonResult AddQuizScore(int ObjectId, int classId, int planId, int AccountId, string Val)
        {
            try
            {
                ScoreSetHelper.ScoreSet(ObjectId, 4, classId, planId, AccountId, Val.ToDouble());
                return Json(new { Code = 0, Msg = "提交成功" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Code = -1, Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //班级考试
        public ActionResult ClassExam(string classId, string status, int pageIndex = 1)
        {
            var param_classId = Dianda.Common.QueryString.Decrypt(classId).ToInt();
            IList<Member_ClassRegister> list = new List<Member_ClassRegister> { };

            var TrainingId = new Class_DetailBLL().GetModel(param_classId).TraningId;

            //考试
            var classUnit = new Course_UnitContentBLL().GetList(" Delflag=0 and UnitType=6 AND UnitId IN (SELECT id FROM dbo.Course_UnitDetail WHERE TrainingId=" + TrainingId + ")", "")
                ;
            var unitList = from c in classUnit select c;

            int totalPage = 0;

            string param_status = "";//状态

            switch (status)
            {
                case "-1"://未提交
                    param_status = @" and not EXISTS ( SELECT 1
                     FROM   dbo.Member_ClassUnitContentSchedule cs
                            JOIN dbo.Course_UnitContent cu ON cs.UnitContent = cu.Id
                     WHERE  m.AccountId = cs.AccountId
                            AND cs.ClassId = m.ClassId
                            AND cu.UnitType = 6 and cs.Delflag=0)";
                    break;
                case "0"://单元未得分
                    param_status = @" AND  EXISTS ( SELECT 1
                     FROM   dbo.Member_ClassUnitContentSchedule cs
                            JOIN dbo.Course_UnitContent cu ON cs.UnitContent = cu.Id
                     WHERE  m.AccountId = cs.AccountId
                            AND cs.ClassId = m.ClassId
                            AND cu.UnitType = 6   AND ISNULL(cs.score,0)=0 and cs.Delflag=0) 

                    AND not EXISTS ( SELECT 1
                     FROM   dbo.Member_ClassUnitContentSchedule cs
                            JOIN dbo.Course_UnitContent cu ON cs.UnitContent = cu.Id
                     WHERE  m.AccountId = cs.AccountId
                            AND cs.ClassId = m.ClassId
                            AND cu.UnitType = 6   AND ISNULL(cs.score,0)>0 and cs.Delflag=0)";
                    break;
                case "1"://单元已得分
                    param_status = @" AND  EXISTS ( SELECT 1
                     FROM   dbo.Member_ClassUnitContentSchedule cs
                            JOIN dbo.Course_UnitContent cu ON cs.UnitContent = cu.Id
                     WHERE  m.AccountId = cs.AccountId
                            AND cs.ClassId = m.ClassId
                            AND cu.UnitType = 6   AND  ISNULL(cs.score,0)>0 and cs.Delflag=0) ";
                    break;
                default:
                    break;
            }

            StringBuilder sbSql = new StringBuilder();

            foreach (var item in unitList)
            {
                sbSql.AppendFormat(@"SELECT  m.* ,
                '{1}' as cqId,
                homeworkId = ( SELECT hw.id
                             FROM   dbo.Member_ClassUnitContentSchedule hw
                             WHERE  hw.AccountId = m.AccountId
                                    AND hw.ClassId = {0}
                                    AND hw.UnitContent = {1} and hw.Delflag=0
                           )
        FROM    dbo.Member_ClassRegister m
                LEFT JOIN dbo.Class_Detail d ON m.ClassId = d.Id
                LEFT JOIN dbo.Course_UnitDetail ud ON ud.TrainingId = d.TraningId
                LEFT JOIN Course_UnitContent uc ON uc.UnitId = ud.Id
                LEFT JOIN dbo.Member_Account ma ON m.AccountId = ma.Id
        WHERE   m.ClassId = {0}
                AND uc.id = {1} AND m.Delflag=0 and m.Status=4
                 " + param_status, param_classId, item.Id);
                sbSql.Append(" union ");
            }

            if (sbSql.Length > 0)
            {
                if (sbSql.ToString().Contains("union"))
                {
                    sbSql = sbSql.Remove(sbSql.ToString().LastIndexOf("union"), 5);
                }
                list = DataTableToListHelper<Dianda.AP.Model.Member_ClassRegister>
                                     .ConvertToModel(PagingQueryBll.GetPagingDataTable("(" + sbSql.ToString() + ") as tmp", " 1=1 ", "CreateDate", pageIndex, out totalPage, @"*"));
            }


            ViewBag.classId = classId;
            ViewBag.param_classId = param_classId;
            //分页
            ViewBag.pageIndex = pageIndex;
            ViewBag.totalPage = totalPage;
            ViewBag.basecount = 10;

            var classModel = new Class_DetailBLL().GetModel(param_classId);
            if (classModel != null)
            {
                ViewBag.CourseName = new Traning_DetailBLL().GetModel(classModel.TraningId, "").Title;
                ViewBag.ClassName = classModel.Title;
                ViewBag.planId = classModel.PlanId;
            }


            return View(list);
        }

        //获取学员考卷答案列表
        [UrlDecrypt]
        public JsonResult GetExamAnswerList(int UnitContentId, int AccountId)
        {
            try
            {
                var quizList = new Member_CourseContentTestAnswerResultBLL().GetList(" Delflag=0 and  UnitContent=" + UnitContentId + " and AccountId=" + AccountId, "");
                return Json(from c in quizList select new { Id = Dianda.Common.QueryString.UrlEncrypt(c.Id), CreateDate = c.CreateDate.ToString("yyyy-MM-dd HH:mm") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Code = -1, Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //考试打分
        public JsonResult AddExamScore(int ObjectId, int classId, int planId, int AccountId, string Val)
        {
            try
            {
                ScoreSetHelper.ScoreSet(ObjectId, 5, classId, planId, AccountId, Val.ToDouble());

                return Json(new { Code = 0, Msg = "提交成功" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Code = -1, Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //评价打分
        public JsonResult AddCommentScore(int classId, int planId, int accountId, string Val)
        {
            try
            {
                ScoreSetHelper.ScoreSet(null, 6, classId, planId, accountId, Val.ToDouble());

                return Json(new { Code = 0, Msg = "提交成功" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Code = -1, Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //结业评价
        public ActionResult ClassGraduate(string classId, int pageIndex = 1)
        {
            var param_classId = Dianda.Common.QueryString.Decrypt(classId).ToInt();
            int totalPage = 0;

            string where = "mc.ClassId = " + param_classId + " AND mc.Delflag=0 AND mc.Status=4";


            //Model
            var list = DataTableToListHelper<Dianda.AP.Model.Member_ClassRegister>
           .ConvertToModel(PagingQueryBll.GetPagingDataTable(@"dbo.Member_ClassRegister mc
        LEFT JOIN dbo.Member_Account ma ON mc.AccountId = ma.Id", where, "mc.CreateDate", pageIndex, out totalPage, @"mc.* "));

            var classUnit = new Member_ContentAnswerResultBLL().GetListModel(" ClassId=" + param_classId).GroupBy(s => s.UnitContent);
            ViewBag.UnitList = from c in classUnit select c.Key;

            ViewBag.classId = classId;
            ViewBag.param_classId = param_classId;
            //分页
            ViewBag.pageIndex = pageIndex;
            ViewBag.totalPage = totalPage;
            ViewBag.basecount = 10;

            var classModel = new Class_DetailBLL().GetModel(param_classId);
            if (classModel != null)
            {
                ViewBag.CourseName = new Traning_DetailBLL().GetModel(classModel.TraningId, "").Title;
                ViewBag.ClassName = classModel.Title;
                ViewBag.planId = classModel.PlanId;
            }


            return View(list);
        }

        //结业打分
        [ValidateInput(false)]
        public ActionResult ResultAction(int classId, string Ids, int Val, string Remark)
        {
            try
            {
                var bll = new Member_ClassRegisterBLL();
                var ids = Ids.Split(',');
                var classDetail = new Dianda.AP.BLL.Class_DetailBLL().GetModel(classId);
                var tranFieldModel = new Traning_DetailBLL().GetModel(classDetail.TraningId, "");

                foreach (var id in ids)
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        var model = bll.GetList("Delflag=0 and ClassId=" + classId + " and AccountId=" + id.ToInt(), "").First();
                        
                        model.Result = Val;

                        var totalScore = (model.ReadingScore.HasValue ? model.ReadingScore.Value : 0)
                            + (model.DiscussScore.HasValue ? model.DiscussScore.Value : 0)
                            + (model.HomeWorkScore.HasValue ? model.HomeWorkScore.Value : 0)
                            + (model.TestingScore.HasValue ? model.TestingScore.Value : 0)
                            + (model.ExaminationScore.HasValue ? model.ExaminationScore.Value : 0)
                            + (model.CommentScore.HasValue ? model.CommentScore.Value : 0);
                        if (Math.Ceiling(totalScore) < 60 && Val == 1&&tranFieldModel.OutSideType==-1)//分数小于60，不操作
                            continue;

                        model.ApplyRemark = Remark;
                        model.ResultCreater = SiteCache.Instance.LoginInfo.UserId;
                        model.TotalScore = totalScore;
                        bll.Update(model);

                        if (Val == 1)//如果结论是合格，则要更新用户学分
                        {
                            var tranEditModel = new Member_TrainingReditBLL().GetList("Delflag=0 and TrainingField=" + tranFieldModel.TraingField
                                + " and PlanId=" + model.PlanId + " and AccountId=" + model.AccountId, "");
                            Member_TrainingRedit tranEdit = null;
                            if (tranEditModel == null || tranEditModel.Count == 0)//创建
                            {
                                tranEdit = new Member_TrainingRedit();
                                tranEdit.AccountId = model.AccountId;
                                tranEdit.CreateDate = DateTime.Now;
                                tranEdit.Credits = tranFieldModel.TotalTime.Value;
                                tranEdit.Delflag = false;
                                tranEdit.PlanId = model.PlanId;
                                tranEdit.TrainingField = tranFieldModel.TraingField;
                                new Member_TrainingReditBLL().Add(tranEdit);
                            }
                            else//更新
                            {
                                tranEdit = tranEditModel.First();
                                tranEdit.Credits += tranFieldModel.TotalTime.Value;
                                new Member_TrainingReditBLL().Update(tranEdit);
                            }
                            var traningCredit = DataTableToListHelper<Training_Credits>.ConvertToModel(
                                new Training_CreditsBLL().GetList(" and PlanId=" + model.PlanId + " and OrganId=" + classDetail.OrganId, "")
                                );//查找学时学分
                            if (traningCredit != null && traningCredit.Count > 0) //轮循所有大类
                            {
                                bool graduateFlag = true;
                                var member_traningBll = new Member_TrainingReditBLL();
                                foreach (var item in traningCredit)
                                {
                                    var traning_credit = member_traningBll.GetList
                                        (" Delflag=0 and PlanId=" + model.PlanId + " and AccountId=" + model.AccountId + " and TrainingField=" + item.TraningField
                                        , "");
                                    if (traningCredit != null && traningCredit.Count > 0)
                                    {
                                        if (traningCredit.First().MinValue > tranEdit.Credits)
                                        {
                                            graduateFlag = false;
                                            break;
                                        }
                                    }
                                }
                                if (graduateFlag)//所有大类学分均合格,更新Member_PlanOverall表的用户合格状态。
                                {
                                    var Member_PlanOverallBll = new Member_PlanOverallBLL();
                                    var Member_PlanOverall = Member_PlanOverallBll.GetListModel(" Delflag=0 and  PlanId=" + model.PlanId + " and AccountId=" + model.AccountId);
                                    if (Member_PlanOverall != null && Member_PlanOverall.Count > 0)
                                    {
                                        var item = Member_PlanOverall.First();
                                        item.Result = 1;
                                        Member_PlanOverallBll.Update(item);
                                    }
                                    else
                                    {
                                        var item = new Member_PlanOverall();
                                        item.Result = 1;
                                        item.PlanId = model.PlanId;

                                        item.AccountId = model.AccountId;
                                        item.CreateDate = DateTime.Now;
                                        item.Delflag = false;

                                        Member_PlanOverallBll.Add(item);
                                    }
                                }
                            }

                        }
                    }
                }

                return Json(new { Code = 0, Msg = "提交成功" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Code = -1, Msg = ex.Message + ex.StackTrace }, JsonRequestBehavior.AllowGet);
            }
        }

        //重新打分
        [ValidateInput(false)]
        public ActionResult ResetScore(int classId, int planId)
        {
            try
            {

                ScoreSetHelper.resetScore(classId, planId);

                return Json(new { Code = 0, Msg = "操作成功" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Code = -1, Msg = ex.Message + ex.StackTrace }, JsonRequestBehavior.AllowGet);
            }
        }

        //添加外部作业
        public JsonResult AddClassOutMission(int classId, string title, string content, int startdate
            , int enddate, string attlist)
        {

            try
            {
                var classObj = new Class_DetailBLL().GetModel(classId);
                var bll = new Class_HomeWorkMissionBLL();
                var model = new Class_HomeWorkMission();
                model.ClassId = classId;
                model.Title = title;
                model.Content = content;
                model.StartDate = startdate != -1 ? classObj.OpenClassFrom.AddDays(startdate) : Convert.ToDateTime("1800-01-01");
                model.EndDate = enddate != -1 ? classObj.OpenClassFrom.AddDays(enddate) : Convert.ToDateTime("1800-01-01");
                if (!string.IsNullOrEmpty(attlist))
                {
                    var json = "[";
                    var attArry = attlist.Remove(attlist.LastIndexOf('|'), 1).Split('|');
                    for (int i = 0; i < attArry.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(attArry[i]))
                        {
                            json += "{" + attArry[i];
                            if (i < attArry.Length - 1)
                                json += "},";
                            else
                                json += "}";
                        }

                    }
                    json += "]";
                    model.AttList = json;
                }
                model.Creater = SiteCache.Instance.LoginInfo.UserId;
                model.CreateDate = DateTime.Now;
                model.Display = true;
                model.Delflag = false;
                bll.Add(model);

                return Json(new { Code = 0, Msg = "提交成功" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Code = -1, Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }


        //外部作业列表
        public ActionResult ClassOutMissionList(int classId, string status, string homework, int pageIndex = 1)
        {
            int totalPage = 0;

            string where = "mc.ClassId = " + classId + " AND mc.Delflag=0 and status=4";

            string param_unit = string.IsNullOrEmpty(homework) ? "" : " AND cm.Id= " + homework;//作业

            string param_status = "";//状态

            switch (status)
            {
                case "-1"://未提交
                    param_status = @" and not EXISTS ( SELECT  1
FROM    dbo.Class_HomeWork ch
        JOIN dbo.Class_HomeWorkMission cm ON cm.Id=ch.HomeWorkId
WHERE   mc.AccountId = ch.AccountId
        AND cm.ClassId = mc.ClassId " + param_unit + " )";
                    break;
                case "0"://单元未得分
                    param_status = @" AND  EXISTS (ELECT  1
FROM    dbo.Class_HomeWork ch
        JOIN dbo.Class_HomeWorkMission cm ON cm.Id=ch.HomeWorkId
WHERE   mc.AccountId = ch.AccountId
        AND cm.ClassId = mc.ClassId " + param_unit + " AND cr.score IS  NULL) ";
                    break;
                case "1"://单元已得分
                    param_status = @" AND  EXISTS ( SELECT 1
                     FROM   dbo.Member_ContentAnswerResult cr
                            JOIN dbo.Course_UnitContent cu ON cr.UnitContent = cu.Id
                     WHERE  mc.AccountId = cr.AccountId
                            AND cr.ClassId = mc.ClassId
                            AND cu.UnitType = 5 " + param_unit + " AND cr.score IS NOT NULL) ";
                    break;
                default:
                    break;
            }

            //Model
            var list = DataTableToListHelper<Dianda.AP.Model.Member_BaseInfo>
           .ConvertToModel(PagingQueryBll.GetPagingDataTable(@"dbo.Member_ClassRegister mc
        LEFT JOIN dbo.Member_Account ma ON mc.AccountId = ma.Id
        LEFT JOIN dbo.Member_BaseInfo mb ON ma.Id = mb.AccountId", where + param_status, "mb.CreateDate", pageIndex, out totalPage, @"mb.* ,
        chId = ( SELECT TOP 1
                        cm.Id
                 FROM   dbo.Class_HomeWorkMission cm
                        JOIN dbo.Class_HomeWork ch ON ch.HomeWorkId=cm.Id
                 WHERE  ch.ClassId = mc.ClassId
                        AND ch.AccountId = mc.AccountId " + param_unit + " )"));

            ViewBag.homeworkList = new Class_HomeWorkMissionBLL().GetListModel(" ClassId=" + classId);

            ViewBag.classId = classId;
            //分页
            ViewBag.pageIndex = pageIndex;
            ViewBag.totalPage = totalPage;
            ViewBag.basecount = 10;
            ViewBag.ClassName = new Class_DetailBLL().GetModel(classId).Title;
            ViewBag.CourseName = new Traning_DetailBLL().GetModel(new Class_DetailBLL().GetModel(classId).TraningId, "").Title;
            return View(list);

        }

        //获取学员外部活动列表
        public JsonResult GetOutCourseRecord(int classId, int traningId, int accountId, int type)
        {
            try
            {
                var recordList = new Course_OutCourseRecordBLL()
                    .GetList(" Delflag=0 and ClassId=" + classId + " and TrainingId=" + traningId + " and AccountId=" + accountId + " and DataType in (" + (type == 1 ? "1,2" : type.ToString()) + ")", "");
                return Json(recordList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Code = -1, Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}