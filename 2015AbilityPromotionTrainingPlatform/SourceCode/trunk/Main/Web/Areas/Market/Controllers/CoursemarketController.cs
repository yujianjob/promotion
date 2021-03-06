﻿using Dianda.AP.BLL;
using Dianda.AP.Model;
using Dianda.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Web.Attributes;
using Web.Code;

namespace Web.Areas.Market.Controllers
{
    public class CoursemarketController : Controller
    {
        Organ_DetailBLL organ_DetailBLL = new Organ_DetailBLL();

        #region 教师/学校--课程超市列表
        /// <summary>
        /// 教师--课程超市列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="organId"></param>
        /// <param name="field"></param>
        /// <param name="searchTitle"></param>
        /// <returns></returns>
        //[UrlDecrypt]
        public ActionResult CoursemarketSingleList(int? pageIndex, int? organId, int? field, string searchTitle)
        {
            ViewBag.Title = "课程超市";

            int i = TypeConverter.ObjectToInt(pageIndex, 1);
            int groupId = Code.SiteCache.Instance.GroupId;//4,学校管理7，普通教师
            Organ_DetailBLL organ_DetailBLL = new Organ_DetailBLL();
            int CountyOrganId = 0;

            int oType = 1;
            if (groupId == 7)
            {
                oType = (int)organ_DetailBLL.GetModel(Code.SiteCache.Instance.OrganId).OType;
                CountyOrganId = (int)organ_DetailBLL.GetModel(Code.SiteCache.Instance.OrganId).ParentId;
            }
            else if (groupId == 2 || groupId == 3 || groupId == 4)
            {
                oType = (int)organ_DetailBLL.GetModel(Code.SiteCache.Instance.ManageOrganId).OType;
                if (groupId == 4)
                    CountyOrganId = (int)organ_DetailBLL.GetModel(Code.SiteCache.Instance.ManageOrganId).ParentId;
                else
                    CountyOrganId = Code.SiteCache.Instance.ManageOrganId;
            }
            ViewBag.OType = oType;
            int partitionId = Code.SiteCache.Instance.LoginInfo.PartitionId;
            Member_BaseInfoBLL member_BaseInfoBLL = new Member_BaseInfoBLL();
            Member_BaseInfo m = member_BaseInfoBLL.GetModelByAccountId(Code.SiteCache.Instance.LoginInfo.UserId);
            string where = " and td.PartitionId='" + partitionId + "'and cd.PlanId='" + Code.SiteCache.Instance.PlanId + "' and cd.PartitionId='" + partitionId + "'";

            if (oType == 1)
            {
                ViewBag.OrganList = organ_DetailBLL.GetListModel(" OType=1 and Delflag='false'");
            }
            else if (oType == 2 || oType == 5)
            {
                ViewBag.OrganList = organ_DetailBLL.GetListModel(" OType=5 and Delflag='false'");
            }
            else
            {
                where += " and 1=2";
            }

            if (m != null)
            {
                where += " and (td.OrganId='" + CountyOrganId + "' or td.Range=2)";
            }
            else
            {
                where += " and 1=2";
            }

            if (organId != null && organId > 0)
            {
                where += " and td.OrganId='" + organId + "' ";
            }
            else { organId = -1; }
            if (!string.IsNullOrEmpty(searchTitle))
            { where += " and td.Title like'%" + searchTitle.Replace("'", "''") + "%' "; }
            string where2 = where;
            if (field != null && field > 0)
            { where += " and td.TraingField='" + field + "' "; }
            Traning_DetailBLL traning_DetailBLL = new Traning_DetailBLL();
            int total = 0;
            List<Traning_Detail> tdlist = traning_DetailBLL.GetListHasClass(10, i, where, "CreateDate desc", out total);

            ViewData["searchTitle"] = string.IsNullOrEmpty(searchTitle) ? "" : searchTitle;
            ViewData["organId"] = organId;
            ViewData["field"] = field;
            ViewData["partitionId"] = partitionId;
            ViewData["groupId"] = groupId;
            ViewBag.pageIndex = i;
            ViewBag.totalPage = total;

            Traning_FieldBLL traning_FieldBLL = new Traning_FieldBLL();

            List<Traning_Field> traning_FieldList = traning_FieldBLL.GetList(" Delflag='false' and display='1' and Id<>3", " Sort");
            ViewBag.FieldList = traning_FieldList;
            List<Traning_Detail> tdlistall = traning_DetailBLL.GetListHasClass(-1, -1, where2, "TraingField,CreateDate desc", out total);
            List<int> list = new List<int>(traning_FieldList.Count);
            int sum = 0;
            foreach (Traning_Field tf in traning_FieldList)
            {
                int c = tdlistall.Where(a => a.TraingField == tf.Id).Count();
                list.Add(c);
                sum += c;
            }

            ViewBag.Sum = sum;
            ViewBag.FCount = list;
            return View(tdlist);
        }
        #endregion

        #region 教师/学校--课程详细
        /// <summary>
        /// 教师--课程详细
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [UrlDecrypt]
        public ActionResult CoursemarketSingleEnrollC(int Id)
        {
            ViewBag.Title = "课程详细";
            int groupId = Code.SiteCache.Instance.GroupId;//4,学校管理7，普通教师
            ViewData["groupId"] = groupId;
            Traning_DetailBLL traning_DetailBLL = new Traning_DetailBLL();
            Traning_Detail traning_Detail = new Traning_Detail();
            traning_Detail = traning_DetailBLL.GetModel(Id, "");
            return View(traning_Detail);
        }
        #endregion

        #region 教师/学校--课程详细-班级列表
        /// <summary>
        /// 教师--课程详细-班级列表
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public ActionResult CoursemarketTCList(int? pageIndex, int Id, int Type)
        {
            Code.SiteCache cache = Code.SiteCache.Instance;
            int groupId = cache.GroupId;//4,学校管理7，普通教师
            ViewData["groupId"] = groupId;
            int i = TypeConverter.ObjectToInt(pageIndex, 1);
            Class_DetailBLL class_DetailBLL = new Class_DetailBLL();
            IList<Class_Detail> clist = new List<Class_Detail>();
            int total = 0;
            int partitionId = 1;
            string where = " display=1 and delflag='false' and TraningId='" + Id + "' and PartitionId='" + partitionId + "'";
            switch (Type)
            {
                case 1: where += " and status=3"; break;
                case 2: where += " and status=5"; break;
                case 3: where += " and status=6"; break;
                //case 1: where += " and GETDATE()>SignUpStartTime and GETDATE()<dateadd(d,1,SignUpEndTime)"; break;
                //case 2: where += " and GETDATE()>OpenClassFrom and GETDATE()<dateadd(d,1,OpenClassTo)"; break;
                //case 3: where += " and GETDATE()>dateadd(d,1,OpenClassTo)  "; break;
            }
            clist = DataTableToListHelper<Dianda.AP.Model.Class_Detail>.ConvertToModel(PagingQueryBll.GetPagingDataTable(@"Class_Detail", where, "Id", i, out total, "*", 5));
            ViewBag.pageIndex = i;
            ViewBag.totalPage = total;
            ViewBag.type = Type;
            return View(clist);
        }
        #endregion

        #region 教师--班级详细
        /// <summary>
        /// 教师--班级详细
        /// </summary>
        /// <returns></returns>
        [UrlDecrypt]
        public ActionResult CoursemarketSingleEnroll(int Id)
        {
            ViewBag.Title = "课程报名";
            Class_DetailBLL class_DetailBLL = new Class_DetailBLL();
            Class_Detail class_Detail = new Class_Detail();
            class_Detail = class_DetailBLL.GetModel(Id);
            return View(class_Detail);
        }
        #endregion

        #region 教师--报名申请
        [ValidateInput(false)]
        /// <summary>
        /// 教师--报名申请
        /// </summary>
        /// <param name="ClassId"></param>
        /// <returns></returns>
        public JsonResult ClassRegister(int ClassId)
        {
            Class_Detail class_Detail = new Class_Detail();
            Member_ClassRegisterBLL member_ClassRegisterBLL = new Member_ClassRegisterBLL();
            Member_ClassRegister member_ClassRegister = new Member_ClassRegister();
            member_ClassRegister.ClassId = ClassId;
            member_ClassRegister.PlanId = Code.SiteCache.Instance.PlanId;
            Class_DetailBLL class_DetailBLL = new Class_DetailBLL();
            member_ClassRegister.TrainingId = class_DetailBLL.GetModel(ClassId).TraningId;
            member_ClassRegister.AccountId = Code.SiteCache.Instance.LoginInfo.UserId;
            member_ClassRegister.Status = 1;
            member_ClassRegister.ManagerId = Code.SiteCache.Instance.LoginInfo.UserId;
            member_ClassRegister.Delflag = false;
            member_ClassRegister.CreateDate = DateTime.Now;
            Course_UnitDetailBLL course_UnitDetailBLL = new Course_UnitDetailBLL();
            member_ClassRegister.TotalSchedule = course_UnitDetailBLL.GetList(" TrainingId='" + class_DetailBLL.GetModel(ClassId).TraningId + "' and ParentId=0 and Display=1 and Delflag='false'", "").Count;

            //if (!IsCanRegister(ClassId, true, Code.SiteCache.Instance.LoginInfo.UserId))
            string checkmessage = IsCanRegisterAll(ClassId, true, Code.SiteCache.Instance.LoginInfo.UserId);
            if (checkmessage != "")
            {
                return Json(new { Code = -1, Msg = checkmessage }, JsonRequestBehavior.AllowGet);
            }

            List<Member_ClassRegister> listno = member_ClassRegisterBLL.GetList(" ClassId='" + ClassId + "' and AccountId='" + Code.SiteCache.Instance.LoginInfo.UserId + "' and delflag='false' and Status in (1,2,4)", "");
            if (listno.Count > 0)
            {
                return Json(new { Code = -1, Msg = "您已经提交了报名申请" }, JsonRequestBehavior.AllowGet);
            }

            List<Member_ClassRegister> listpass = member_ClassRegisterBLL.GetList(" TrainingId='" + member_ClassRegister.TrainingId + "' and AccountId='" + Code.SiteCache.Instance.LoginInfo.UserId + "' and delflag='false' and (Result=1 or (Result is NULL and Status in(1,2,4)))", "");
            if (listpass.Count > 0)
            {
                return Json(new { Code = -1, Msg = "您已经学过该课程或已报名该课程下的其他班级" }, JsonRequestBehavior.AllowGet);
            }

            List<Member_ClassRegister> listup = member_ClassRegisterBLL.GetList(" ClassId='" + ClassId + "' and AccountId='" + Code.SiteCache.Instance.LoginInfo.UserId + "' and delflag='false' and Status in (3,5)", "");
            if (listup.Count > 0)
            {
                member_ClassRegister.Id = listup[0].Id;
                member_ClassRegister.Status = 1;
                member_ClassRegisterBLL.Update(member_ClassRegister);

                if (member_ClassRegister.Id > 0)
                {
                    class_Detail = class_DetailBLL.GetModel(member_ClassRegister.ClassId);
                    class_Detail.People += 1;
                    class_DetailBLL.Update(class_Detail);

                    Member_ClassRegisterApplicationBLL member_ClassRegisterApplicationBLL = new Member_ClassRegisterApplicationBLL();
                    Member_ClassRegisterApplication member_ClassRegisterApplication = new Member_ClassRegisterApplication();
                    member_ClassRegisterApplication.ClassRegisterId = member_ClassRegister.Id;
                    member_ClassRegisterApplication.Status = 1;
                    member_ClassRegisterApplication.Creater = Code.SiteCache.Instance.LoginInfo.UserId;
                    member_ClassRegisterApplication.CreateDate = DateTime.Now;
                    member_ClassRegisterApplication.Delflag = false;
                    member_ClassRegisterApplicationBLL.Add(member_ClassRegisterApplication);
                    return Json(new { Code = 0, Msg = "提交成功" }, JsonRequestBehavior.AllowGet);
                }
                else { return Json(new { Code = -1, Msg = "提交失败" }, JsonRequestBehavior.AllowGet); }
            }
            else
            {
                int RegisterId = member_ClassRegisterBLL.Add(member_ClassRegister);
                class_Detail = class_DetailBLL.GetModel(member_ClassRegister.ClassId);
                class_Detail.People += 1;
                class_DetailBLL.Update(class_Detail);
                if (RegisterId > 0)
                {
                    Member_ClassRegisterApplicationBLL member_ClassRegisterApplicationBLL = new Member_ClassRegisterApplicationBLL();
                    Member_ClassRegisterApplication member_ClassRegisterApplication = new Member_ClassRegisterApplication();
                    member_ClassRegisterApplication.ClassRegisterId = RegisterId;
                    member_ClassRegisterApplication.Status = 1;
                    member_ClassRegisterApplication.Creater = Code.SiteCache.Instance.LoginInfo.UserId;
                    member_ClassRegisterApplication.CreateDate = DateTime.Now;
                    member_ClassRegisterApplication.Delflag = false;
                    member_ClassRegisterApplicationBLL.Add(member_ClassRegisterApplication);
                    return Json(new { Code = 0, Msg = "提交成功" }, JsonRequestBehavior.AllowGet);
                }
                else { return Json(new { Code = -1, Msg = "提交失败" }, JsonRequestBehavior.AllowGet); }
            }
        }
        #endregion

        #region 教师--报名申请校验
        /// <summary>
        /// 检测是否能报名
        /// </summary>
        /// <param name="ClassId"></param>
        /// <returns></returns>
        public static bool CheckCanEnroll(int ClassId)
        {
            Class_Detail class_Detail = new Class_Detail();
            Member_ClassRegisterBLL member_ClassRegisterBLL = new Member_ClassRegisterBLL();
            Member_ClassRegister member_ClassRegister = new Member_ClassRegister();
            member_ClassRegister.ClassId = ClassId;
            member_ClassRegister.PlanId = Code.SiteCache.Instance.PlanId;
            Class_DetailBLL class_DetailBLL = new Class_DetailBLL();
            member_ClassRegister.TrainingId = class_DetailBLL.GetModel(ClassId).TraningId;
            member_ClassRegister.AccountId = Code.SiteCache.Instance.LoginInfo.UserId;
            member_ClassRegister.Status = 1;
            member_ClassRegister.ManagerId = Code.SiteCache.Instance.LoginInfo.UserId;
            member_ClassRegister.Delflag = false;
            member_ClassRegister.CreateDate = DateTime.Now;
            Course_UnitDetailBLL course_UnitDetailBLL = new Course_UnitDetailBLL();
            member_ClassRegister.TotalSchedule = course_UnitDetailBLL.GetList(" TrainingId='" + class_DetailBLL.GetModel(ClassId).TraningId + "' and ParentId=0 and Display=1 and Delflag='false'", "").Count;

            List<Member_ClassRegister> listno = member_ClassRegisterBLL.GetList(" ClassId='" + ClassId + "' and AccountId='" + Code.SiteCache.Instance.LoginInfo.UserId + "' and delflag='false' and Status in (1,2,4)", "");
            if (listno.Count > 0)
            {
                return false;
            }

            //List<Member_ClassRegister> listpass = member_ClassRegisterBLL.GetList(" TrainingId='" + member_ClassRegister.TrainingId + "' and AccountId='" + Code.SiteCache.Instance.LoginInfo.UserId + "' and delflag='false' and (Result=1 or (Result is NULL and Status=4))", "");
            //if (listpass.Count > 0)
            //{
            //    return false;
            //}

            return true;
        }
        #endregion

        #region 学校--课程详细
        /// <summary>
        /// 学校--课程详细
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [UrlDecrypt]
        public ActionResult CoursemarketEnroll(int Id)
        {
            ViewBag.Title = "课程批量报名";
            ViewBag.OrganId = 1;
            Class_DetailBLL class_DetailBLL = new Class_DetailBLL();
            Class_Detail class_Detail = new Class_Detail();
            class_Detail = class_DetailBLL.GetModel(Id);
            return View(class_Detail);
        }
        #endregion

        #region 学校--教师选择list
        /// <summary>
        /// 学校--教师选择list
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="OrganId"></param>
        /// <returns></returns>
        public ActionResult ChooseTeachersList(int? pageIndex, int ClassId, int Type)
        {
            Class_DetailBLL class_DetailBLL = new Class_DetailBLL();
            Class_Detail ClassModel = class_DetailBLL.GetModel(ClassId);
            int TrainingId = ClassModel.TraningId;
            int OrganId = Code.SiteCache.Instance.ManageOrganId;
            int Groupid = Code.SiteCache.Instance.GroupId;
            int i = TypeConverter.ObjectToInt(pageIndex, 1);
            Member_AccountBLL member_AccountBLL = new Member_AccountBLL();
            List<Member_AccountBaseInfo> member_AccBaseInfoList = new List<Member_AccountBaseInfo>();
            int total = 0;
            member_AccBaseInfoList = member_AccountBLL.GetMarketMemberList(12, i, Type, ClassModel, TrainingId, OrganId, Groupid, "Id", out total);
            ViewBag.pageIndex = i;
            ViewBag.totalPage = total;
            ViewBag.Type = Type;
            ViewBag.ClassId = ClassId;
            return View(member_AccBaseInfoList);
        }
        #endregion

        #region 学校/区县--批量报名申请
        [ValidateInput(false)]
        /// <summary>
        /// 学校--批量报名申请
        /// </summary>
        /// <param name="ClassId"></param>
        /// <param name="Members"></param>
        /// <returns></returns>
        public JsonResult ClassRegisterAll(int ClassId, string Members, string MembersNames)
        {
            Class_DetailBLL class_DetailBLL = new Class_DetailBLL();
            string[] members = Members.Trim().Substring(1, Members.Length - 2).Split(',');
            string[] membersNames = MembersNames.Trim().Substring(1, MembersNames.Length - 2).Split(',');
            string names = "";
            string namespass = "";
            string namescannotmessage = "";
            string message = "";

            for (int i = 0; i < members.Length; i++)
            {
                Member_ClassRegisterBLL member_ClassRegisterBLL = new Member_ClassRegisterBLL();
                Member_ClassRegister member_ClassRegister = new Member_ClassRegister();

                List<Member_ClassRegister> listno = member_ClassRegisterBLL.GetList(" ClassId='" + ClassId + "' and AccountId='" + members[i] + "' and delflag='false' and Status in (1,2,4)", "");
                if (listno.Count > 0)
                {
                    names += membersNames[i] + ",";
                }

                List<Member_ClassRegister> listpass = member_ClassRegisterBLL.GetList(" TrainingId='" + member_ClassRegister.TrainingId + "' and AccountId='" + members[i] + "' and delflag='false' and (Result=1 or (Result is NULL and Status in(1,2,4)))", "");
                if (listpass.Count > 0)
                {
                    namespass += membersNames[i] + ",";
                }
                string checkmessage = IsCanRegisterAll(ClassId, false, Convert.ToInt32(members[i]));
                if (checkmessage != "")
                {
                    namescannotmessage += membersNames[i] + checkmessage;
                }
            }


            if (names != "")
            {
                message += names.Trim(',') + "已报名该课程;";
            }
            if (namespass != "")
            {
                message += namespass.Trim(',') + "已学过该课程;";
            }
            if (namescannotmessage != "")
            {
                message += namescannotmessage;
            }
            if (message != "")
            {
                return Json(new { Code = -1, Msg = message }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Class_Detail class_Detail = new Class_Detail();
                class_Detail = class_DetailBLL.GetModel(ClassId);
                class_Detail.People += members.Length;
                class_DetailBLL.Update(class_Detail);
                for (int i = 0; i < members.Length; i++)
                {
                    Member_ClassRegisterBLL member_ClassRegisterBLL = new Member_ClassRegisterBLL();
                    Member_ClassRegister member_ClassRegister = new Member_ClassRegister();


                    member_ClassRegister.ClassId = ClassId;
                    member_ClassRegister.PlanId = Code.SiteCache.Instance.PlanId;
                    member_ClassRegister.TrainingId = class_DetailBLL.GetModel(ClassId).TraningId;
                    member_ClassRegister.AccountId = Convert.ToInt32(members[i]);
                    member_ClassRegister.Status = 2;
                    member_ClassRegister.BatchCode = Guid.NewGuid();
                    member_ClassRegister.ManagerId = Code.SiteCache.Instance.LoginInfo.UserId;
                    member_ClassRegister.Delflag = false;
                    member_ClassRegister.CreateDate = DateTime.Now;
                    Course_UnitDetailBLL course_UnitDetailBLL = new Course_UnitDetailBLL();
                    member_ClassRegister.TotalSchedule = course_UnitDetailBLL.GetList(" TrainingId='" + class_DetailBLL.GetModel(ClassId).TraningId + "' and ParentId=0 and Display=1 and Delflag='false'", "").Count;


                    List<Member_ClassRegister> listup = member_ClassRegisterBLL.GetList(" ClassId='" + ClassId + "' and AccountId='" + members[i] + "' and delflag='false' and Status in (3,5)", "");
                    if (listup.Count > 0)
                    {
                        member_ClassRegister.Id = listup[0].Id;
                        member_ClassRegister.Status = 2;
                        member_ClassRegisterBLL.Update(member_ClassRegister);
                        if (member_ClassRegister.Id > 0)
                        {
                            Member_ClassRegisterApplicationBLL member_ClassRegisterApplicationBLL = new Member_ClassRegisterApplicationBLL();
                            Member_ClassRegisterApplication member_ClassRegisterApplication = new Member_ClassRegisterApplication();
                            member_ClassRegisterApplication.ClassRegisterId = member_ClassRegister.Id;
                            member_ClassRegisterApplication.Status = 2;
                            member_ClassRegisterApplication.Creater = Code.SiteCache.Instance.LoginInfo.UserId;
                            member_ClassRegisterApplication.CreateDate = DateTime.Now;
                            member_ClassRegisterApplication.Delflag = false;
                            member_ClassRegisterApplicationBLL.Add(member_ClassRegisterApplication);
                        }
                        else { return Json(new { Code = -1, Msg = "提交失败" }, JsonRequestBehavior.AllowGet); }
                    }
                    else
                    {
                        int RegisterId = member_ClassRegisterBLL.Add(member_ClassRegister);
                        if (RegisterId > 0)
                        {
                            Member_ClassRegisterApplicationBLL member_ClassRegisterApplicationBLL = new Member_ClassRegisterApplicationBLL();
                            Member_ClassRegisterApplication member_ClassRegisterApplication = new Member_ClassRegisterApplication();
                            member_ClassRegisterApplication.ClassRegisterId = RegisterId;
                            member_ClassRegisterApplication.Status = 2;
                            member_ClassRegisterApplication.Creater = Code.SiteCache.Instance.LoginInfo.UserId;
                            member_ClassRegisterApplication.CreateDate = DateTime.Now;
                            member_ClassRegisterApplication.Delflag = false;
                            member_ClassRegisterApplicationBLL.Add(member_ClassRegisterApplication);
                        }
                        else { return Json(new { Code = -1, Msg = "提交失败" }, JsonRequestBehavior.AllowGet); }
                    }
                }
            }
            return Json(new { Code = 0, Msg = "提交成功" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 学校/区县/培训机构--审核列表
        /// <summary>
        /// 学校--审核列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CoursemarketVerify(int? pageIndex, int? planId, int? status,int? torganId, int? trainingId, int? myclassId, string searchTitle)
        {
            ViewBag.Title = "教师报名管理";
            int i = TypeConverter.ObjectToInt(pageIndex, 1);
            int groupId = Code.SiteCache.Instance.GroupId;//4,学校管理7，普通教师
            Organ_DetailBLL organ_DetailBLL = new Organ_DetailBLL();
            int OrganId = 0;
            string where = "";
            string Organwhere = "";
            if (groupId == 4)
            {
                OrganId = (int)organ_DetailBLL.GetModel(Code.SiteCache.Instance.ManageOrganId).ParentId;
                ViewBag.Yes = 2;
                ViewBag.No = 3;
                Organwhere=where += " and ma.OrganId='" + Code.SiteCache.Instance.ManageOrganId + "'";
                //where += " and mcr.Status<=3";
            }
            else if (groupId == 2)
            {
                OrganId = Code.SiteCache.Instance.ManageOrganId;
                ViewBag.Yes = 4;
                ViewBag.No = 5;
                Organwhere = where += " and mcr.Status in(2,3,4,5)";
                where += " and cd.OrganId='" + Code.SiteCache.Instance.ManageOrganId + "'";
            }
            else if (groupId == 3)
            {
                OrganId = Code.SiteCache.Instance.ManageOrganId;
                ViewBag.Yes = 4;
                ViewBag.No = 5;
                Organwhere = where += " and mcr.Status in(2,3,4,5)";
                where += " and cd.OrganId='" + Code.SiteCache.Instance.ManageOrganId + "'";
            }
            else
            {
                where += " and 1=2";
            }
            if (planId != null && planId > 0)
            {
                where += " and mcr.PlanId='" + planId + "' ";
            }
            else { planId = -1; }
            if (status != null && status > 0)
            {
                where += " and mcr.Status='" + status + "' ";
            }
            else { status = -1; }
            if (torganId != null && torganId > 0)
            {
                where += " and cd.OrganId='" + torganId + "' ";
            }
            else { torganId = -1; }
            if (trainingId != null && trainingId > 0)
            {
                where += " and mcr.TrainingId='" + trainingId + "' ";
            }
            else { trainingId = -1; }
            if (myclassId != null && myclassId > 0)
            {
                where += " and mcr.ClassId='" + myclassId + "' ";
            }
            else { myclassId = -1; }

            if (!string.IsNullOrEmpty(searchTitle))
            { where += " and (mbi.RealName like'%" + searchTitle.Replace("'", "''") + "%' or ma.Nickname like '%" + searchTitle.Replace("'", "''") + "%' or ma.UserName like '%" + searchTitle.Replace("'", "''") + "%' or mbi.TeacherNo like '%" + searchTitle.Replace("'", "''") + "%' or mbi.CredentialsNumber like '%" + searchTitle.Replace("'", "''") + "%' or td.Title like '%" + searchTitle.Replace("'", "''") + "%' or cd.Title like '%" + searchTitle.Replace("'", "''") + "%')"; }


            int total = 0;
            Member_ClassRegisterBLL member_ClassRegisterBLL = new Member_ClassRegisterBLL();
            List<ClassRegisterManage> tdlist = member_ClassRegisterBLL.GetListManage(10, i, where, "CreateDate desc", out total);

            ViewData["searchTitle"] = string.IsNullOrEmpty(searchTitle) ? "" : searchTitle;
            ViewData["planId"] = planId;
            ViewData["status"] = status;
            ViewData["torganId"] = torganId;
            ViewData["trainingId"] = trainingId;
            ViewData["myclassId"] = myclassId;
            ViewData["groupId"] = groupId;
            ViewBag.pageIndex = i;
            ViewBag.totalPage = total;



            ViewBag.OrganList = organ_DetailBLL.GetOrganList(Organwhere);

            Training_PlanBLL training_PlanBLL = new Training_PlanBLL();
            ViewBag.PlanList = training_PlanBLL.GetListModel(" Display=1 and Delflag='false'");

            Traning_FieldBLL traning_FieldBLL = new Traning_FieldBLL();
            ViewBag.FieldList = traning_FieldBLL.GetList(" Display=1 and Delflag='false' and IsSpec=0", " Sort");

            Traning_DetailBLL traning_DetailBLL = new Traning_DetailBLL();
            ViewBag.TraningList = traning_DetailBLL.GetList(" Display=1 and Delflag='false' and PartitionId='" + Code.SiteCache.Instance.LoginInfo.PartitionId + "' and Status=5 and (OrganId='" + OrganId + "' or Range=2)", " Id");

            Class_DetailBLL class_DetailBLL = new Class_DetailBLL();
            ViewBag.ClassList = class_DetailBLL.GetListModel(" Display=1 and Delflag='false' ");

            return View(tdlist);
        }
        #endregion

        #region 学校--审核--查看功能

        [ValidateInput(false)]
        [UrlDecrypt]
        public ActionResult DetailInformation(int Id)
        {
            ViewBag.Title = "报名详细信息";
            Member_ClassRegisterBLL member_ClassRegisterBLL = new Member_ClassRegisterBLL();
            Member_ClassRegister member_ClassRegister = new Member_ClassRegister();
            member_ClassRegister = member_ClassRegisterBLL.GetModel(Id, "");

            Class_DetailBLL class_DetailBLL = new Class_DetailBLL();
            Class_Detail class_Detail = new Class_Detail();
            class_Detail = class_DetailBLL.GetModel(member_ClassRegister.ClassId);

            Member_AccountBLL member_AccountBLL = new Member_AccountBLL();
            ViewBag.AcountInfo = member_AccountBLL.GetModel(member_ClassRegister.AccountId, "");

            Member_BaseInfoBLL member_BaseInfoBLL = new Member_BaseInfoBLL();
            ViewBag.BaseInfo = member_BaseInfoBLL.GetModelByAccountId(member_ClassRegister.AccountId);

            Member_ClassRegisterApplicationBLL member_ClassRegisterApplicationBLL = new Member_ClassRegisterApplicationBLL();
            ViewBag.CApplicationList = member_ClassRegisterApplicationBLL.GetList(" Delflag='false' and ClassRegisterId='" + member_ClassRegister.Id + "'", "CreateDate");
            return View(class_Detail);
        }


        #endregion

        #region 学校/培训机构--审核弹跳页面
        /// <summary>
        /// 学校--审核弹跳页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult Verify(int Id)
        {
            //int groupid = 4;
            int groupid = Code.SiteCache.Instance.GroupId;
            if (groupid == 4)
            {
                ViewBag.Yes = 2;
                ViewBag.No = 3;
            }
            else if (groupid == 3 || groupid == 2)
            {
                ViewBag.Yes = 4;
                ViewBag.No = 5;
            }
            ViewBag.Id = Id;
            return View();
        }
        #endregion

        [ValidateInput(false)]
        [UrlDecrypt]
        public ActionResult CoursemarketVerifyPage(int Id)
        {
            int groupId = Code.SiteCache.Instance.GroupId;//4,学校管理7，普通教师
            ViewData["groupId"] = groupId;
            ViewData["mId"] = Id;
            ViewBag.Title = "报名审核";
            Member_ClassRegisterBLL member_ClassRegisterBLL = new Member_ClassRegisterBLL();
            Member_ClassRegister member_ClassRegister = new Member_ClassRegister();
            member_ClassRegister = member_ClassRegisterBLL.GetModel(Id, "");

            Class_DetailBLL class_DetailBLL = new Class_DetailBLL();
            Class_Detail class_Detail = new Class_Detail();
            class_Detail = class_DetailBLL.GetModel(member_ClassRegister.ClassId);

            Member_AccountBLL member_AccountBLL = new Member_AccountBLL();
            ViewBag.AcountInfo = member_AccountBLL.GetModel(member_ClassRegister.AccountId, "");

            Member_BaseInfoBLL member_BaseInfoBLL = new Member_BaseInfoBLL();
            ViewBag.BaseInfo = member_BaseInfoBLL.GetModelByAccountId(member_ClassRegister.AccountId);

            Member_ClassRegisterApplicationBLL member_ClassRegisterApplicationBLL = new Member_ClassRegisterApplicationBLL();
            ViewBag.CApplicationList = member_ClassRegisterApplicationBLL.GetList(" Delflag='false' and ClassRegisterId='" + member_ClassRegister.Id + "'", "CreateDate");
            return View(class_Detail);
        }

        #region 学校/培训机构--审核
        [ValidateInput(false)]
        /// <summary>
        /// 学校/培训机构--审核
        /// </summary>
        /// <param name="Statue"></param>
        /// <param name="Id"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public ActionResult MyVerify(int Status, int mpId, string content)
        {
            try
            {
                Member_ClassRegisterBLL member_ClassRegisterBLL = new Member_ClassRegisterBLL();
                Member_ClassRegister member_ClassRegister = member_ClassRegisterBLL.GetModel(mpId, "");
                member_ClassRegister = member_ClassRegisterBLL.GetModel(mpId, "");

                Class_DetailBLL class_DetailBLL = new Class_DetailBLL();
                Class_Detail class_Detail = new Class_Detail();
                class_Detail = class_DetailBLL.GetModel(member_ClassRegister.ClassId);

                if ((GetStatusPeople(Status) - GetStatusPeople(member_ClassRegister.Status)) == 1)
                {
                    class_Detail.People += 1;
                }
                else if ((GetStatusPeople(Status) - GetStatusPeople(member_ClassRegister.Status)) == -1)
                {
                    class_Detail.People -= 1;
                }
                if (class_Detail.LimitPeopleCnt != -1)
                {
                    if (class_Detail.LimitPeopleCnt < class_Detail.People)
                    {
                        TempData["Msg"] = "超过报名限制！";
                        return RedirectToAction("../../Market/Coursemarket/CoursemarketVerify");
                    }
                }
                class_DetailBLL.Update(class_Detail);

                //if ((Status == 3 && member_ClassRegister.Status != 3) || (Status == 5 && member_ClassRegister.Status != 5))
                //{
                //    Class_DetailBLL class_DetailBLL = new Class_DetailBLL();
                //    Class_Detail class_Detail = new Class_Detail();
                //    class_Detail = class_DetailBLL.GetModel(member_ClassRegister.ClassId);
                //    class_Detail.People -= 1;
                //    class_DetailBLL.Update(class_Detail);
                //}
                //else if ((Status == 2 && member_ClassRegister.Status == 3) || (Status == 4 && member_ClassRegister.Status == 5) || (Status == 2 && member_ClassRegister.Status == 5))
                //{
                //    Class_DetailBLL class_DetailBLL = new Class_DetailBLL();
                //    Class_Detail class_Detail = new Class_Detail();
                //    class_Detail = class_DetailBLL.GetModel(member_ClassRegister.ClassId);
                //    class_Detail.People += 1;
                //    if (class_Detail.LimitPeopleCnt != -1)
                //    {
                //        if (class_Detail.LimitPeopleCnt < class_Detail.People)
                //        {
                //            TempData["Msg"] = "超过报名限制！";
                //            return RedirectToAction("../../Market/Coursemarket/CoursemarketVerify");
                //        }
                //    }
                //    class_DetailBLL.Update(class_Detail);
                //}
                member_ClassRegister.Status = Status;
                member_ClassRegister.ApplyRemark = content;
                member_ClassRegisterBLL.Update(member_ClassRegister);

                Member_ClassRegisterApplicationBLL member_ClassRegisterApplicationBLL = new Member_ClassRegisterApplicationBLL();
                Member_ClassRegisterApplication member_ClassRegisterApplication = new Member_ClassRegisterApplication();
                member_ClassRegisterApplication.ClassRegisterId = mpId;
                member_ClassRegisterApplication.Status = Status;
                member_ClassRegisterApplication.Remark = content;
                member_ClassRegisterApplication.Creater = Code.SiteCache.Instance.LoginInfo.UserId;
                member_ClassRegisterApplication.Delflag = false;
                member_ClassRegisterApplication.CreateDate = DateTime.Now;
                member_ClassRegisterApplicationBLL.Add(member_ClassRegisterApplication);
                TempData["Msg"] = "提交成功！";
                return RedirectToAction("../../Market/Coursemarket/CoursemarketVerify");
            }
            catch (Exception)
            {
                TempData["Msg"] = "提交失败！";
                return RedirectToAction("../../Market/Coursemarket/CoursemarketVerify");
            }
        }

        public int GetStatusPeople(int Status)
        {
            int people = 0;
            switch (Status)
            {
                case 1: people= 1; break;
                case 2: people= 1; break;
                case 3: people = 0; break;
                case 4: people = 1; break;
                case 5: people = 0; break;
                default: people= 1; break;
            } 
            return people;
        }


        #endregion

        #region 学校/培训机构--批量审核
        [ValidateInput(false)]
        public JsonResult MyVerifyAll(int Status, string Ids)
        {
            try
            {
                string[] myIds = Ids.Trim(',').Split(',');
                foreach (string Id in myIds)
                {
                    Member_ClassRegisterBLL member_ClassRegisterBLL = new Member_ClassRegisterBLL();
                    Member_ClassRegister member_ClassRegister = new Member_ClassRegister();
                    member_ClassRegister = member_ClassRegisterBLL.GetModel(Convert.ToInt32(Id), "");

                    Class_DetailBLL class_DetailBLL = new Class_DetailBLL();
                    Class_Detail class_Detail = new Class_Detail();
                    class_Detail = class_DetailBLL.GetModel(member_ClassRegister.ClassId);

                    if ((GetStatusPeople(Status) - GetStatusPeople(member_ClassRegister.Status)) == 1)
                    {
                        class_Detail.People += 1;
                    }
                    else if ((GetStatusPeople(Status) - GetStatusPeople(member_ClassRegister.Status)) == -1)
                    {
                        class_Detail.People -= 1;
                    }
                    class_DetailBLL.Update(class_Detail);

                    //if ((Status == 3 && member_ClassRegister.Status != 3) || (Status == 5 && member_ClassRegister.Status != 5))
                    //{
                    //    Class_DetailBLL class_DetailBLL = new Class_DetailBLL();
                    //    Class_Detail class_Detail = new Class_Detail();
                    //    class_Detail = class_DetailBLL.GetModel(member_ClassRegister.ClassId);
                    //    class_Detail.People -= 1;
                    //    class_DetailBLL.Update(class_Detail);
                    //}
                    //else if ((Status == 2 && member_ClassRegister.Status == 3) || (Status == 4 && member_ClassRegister.Status == 5) || (Status == 2 && member_ClassRegister.Status == 5))
                    //{
                    //    Class_DetailBLL class_DetailBLL = new Class_DetailBLL();
                    //    Class_Detail class_Detail = new Class_Detail();
                    //    class_Detail = class_DetailBLL.GetModel(member_ClassRegister.ClassId);
                    //    class_Detail.People += 1;
                    //    class_DetailBLL.Update(class_Detail);
                    //}
                    member_ClassRegister.Status = Status;
                    member_ClassRegisterBLL.Update(member_ClassRegister);

                    Member_ClassRegisterApplicationBLL member_ClassRegisterApplicationBLL = new Member_ClassRegisterApplicationBLL();
                    Member_ClassRegisterApplication member_ClassRegisterApplication = new Member_ClassRegisterApplication();
                    member_ClassRegisterApplication.ClassRegisterId = Convert.ToInt32(Id);
                    member_ClassRegisterApplication.Status = Status;
                    member_ClassRegisterApplication.Creater = Code.SiteCache.Instance.LoginInfo.UserId;
                    member_ClassRegisterApplication.Delflag = false;
                    member_ClassRegisterApplication.CreateDate = DateTime.Now;
                    member_ClassRegisterApplicationBLL.Add(member_ClassRegisterApplication);
                }
                return Json(new { Code = 0, Msg = "提交成功" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Code = -1, Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 根据课程查询班级
        /// <summary>
        /// 根据课程查询班级
        /// </summary>
        /// <param name="Course"></param>
        /// <returns></returns>
        public string GetClassListBytCourse(int Course)
        {
            //主题
            Class_DetailBLL class_DetailBLL = new Class_DetailBLL();
            List<Class_Detail> list = class_DetailBLL.GetListModel(" TraningId=" + Course + " and  Delflag='false' and Display='true'");

            return (new JavaScriptSerializer()).Serialize(list);
        }
        #endregion


        #region 判断课程是否能报名


        public string IsCanRegisterAll(int ClassId, bool Issingle, int MemberId)
        {
            string message = "";
            if (AlertIsCanRegister(6, IsCanRegister(ClassId, Issingle, MemberId, 6)) != "")
            {
                return AlertIsCanRegister(6, IsCanRegister(ClassId, Issingle, MemberId, 6));
            }

            for (int type = 1; type <= 5; type++)
            {
                message += AlertIsCanRegister(type, IsCanRegister(ClassId, Issingle, MemberId, type)) == "" ? "" : AlertIsCanRegister(type, IsCanRegister(ClassId, Issingle, MemberId, type)) + ",";
            }
            return message == "" ? message : message.TrimEnd(',') + ";";
        }

        public bool IsCanRegister(int ClassId, bool Issingle, int MemberId, int Type)
        {
            int ParentOrganId = 1;
            int oType = 1;
            int partitionId = Code.SiteCache.Instance.LoginInfo.PartitionId;
            int UserId = MemberId;
            Member_AccountBLL member_AccountBLL = new Member_AccountBLL();
            Member_Account ma = member_AccountBLL.GetModel(UserId, "");
            int OrganId = 1;
            if (Issingle)
            {
                ParentOrganId = (int)organ_DetailBLL.GetModel(Code.SiteCache.Instance.OrganId).ParentId;
                oType = (int)organ_DetailBLL.GetModel(Code.SiteCache.Instance.OrganId).OType;
                OrganId = Code.SiteCache.Instance.OrganId;
            }
            else
            {
                ParentOrganId = (int)organ_DetailBLL.GetModel(Code.SiteCache.Instance.ManageOrganId).ParentId;
                oType = (int)organ_DetailBLL.GetModel(Code.SiteCache.Instance.ManageOrganId).OType;
                OrganId = Code.SiteCache.Instance.ManageOrganId;
            }

            Member_BaseInfoBLL member_BaseInfoBLL = new Member_BaseInfoBLL();
            Member_BaseInfo m = member_BaseInfoBLL.GetModelByAccountId(UserId);
            string where = " and td.PartitionId='" + partitionId + "'and cd.PlanId='" + Code.SiteCache.Instance.PlanId + "' and cd.PartitionId='" + partitionId + "' and cd.Id='" + ClassId + "'";

            if (oType == 1)
            {
                where += " and td.OrganId='" + OrganId + "'";
            }
            else if (oType == 2)
            {
                if (m != null)
                {
                    switch (Type)
                    {
                        case 1:
                            where += " and ((css.StudySection in(select StudySection from Member_StudySection where AccountId='" + UserId + "' and Delflag='false')and css.delflag='false' )or cd.StudyLevel=1)";
                            break;
                        case 2:
                            where += " and ((cts.TeachSubject in(select TeachSubject from Member_TeachSubject where AccountId='" + UserId + "' and Delflag='false')and cts.delflag='false' )or cd.Subject=1)";
                            break;
                        case 3:
                            where += " and ((ctg.TeachGrade in(select TeachGrade from Member_TeachGrade where AccountId='" + UserId + "' and Delflag='false')and ctg.delflag='false' )or cd.TeachGrade=1)";
                            break;
                        case 4:
                            where += " and ((ctr.TeachRank in(select WorkRank from Member_WorkRank where AccountId='" + UserId + "' and Delflag='false')and ctr.delflag='false' )or cd.TeachRank=1)";
                            break;
                        case 5:
                            //where += " and ((CHARINDEX('," + OrganId + ",',','+cd.OrganRange+',')>0 or CHARINDEX('," + OrganId + ",',','+(select Id from Organ_Detail where ParentId in( select OrganRange from Class_Detail where Id in cd.OrganRange))+',')>0) or cd.OrganRange='0')";
                            where += " and ((CHARINDEX('," + OrganId + ",',','+cd.OrganRange+',')>0  or " + OrganId + " in(select Id from Organ_Detail where charindex(','+cast(ParentId as varchar)+',',','+cd.OrganRange+',')>0) or cd.OrganRange='0'))";
                            break;
                    }
                    where += " and (td.OrganId='" + ParentOrganId + "' or td.Range=2)";

                }
                else
                {
                    where += "and 1=2";
                }
            }
            else if (oType == 5)
            {
                if (m != null)
                {
                    switch (Type)
                    {
                        case 1:
                            where += " and ((css.StudySection in(select StudySection from Member_StudySection where AccountId='" + UserId + "' and Delflag='false')and css.delflag='false' )or cd.StudyLevel=1)";
                            break;
                        case 2:
                            where += " and ((cts.TeachSubject in(select TeachSubject from Member_TeachSubject where AccountId='" + UserId + "' and Delflag='false')and cts.delflag='false' )or cd.Subject=1)";
                            break;
                        case 3:
                            where += " and ((ctg.TeachGrade in(select TeachGrade from Member_TeachGrade where AccountId='" + UserId + "' and Delflag='false')and ctg.delflag='false' )or cd.TeachGrade=1)";
                            break;
                        case 4:
                            where += " and ((ctr.TeachRank in(select WorkRank from Member_WorkRank where AccountId='" + UserId + "' and Delflag='false')and ctr.delflag='false' )or cd.TeachRank=1)";
                            break;
                        case 5:
                            where += " and ((CHARINDEX('," + ma.OrganId + ",',','+cd.OrganRange+',')>0  or " + ma.OrganId + " in(select Id from Organ_Detail where charindex(','+cast(ParentId as varchar)+',',','+cd.OrganRange+',')>0) or cd.OrganRange='0'))";
                            break;
                    }
                    where += " and (td.OrganId='" + OrganId + "' or td.Range=2)";
                }
                else
                {
                    where += "and 1=2";
                }
            }
            else
            {
                where += " and 1=2";
            }
            Traning_DetailBLL traning_DetailBLL = new Traning_DetailBLL();
            int total = 0;
            List<Traning_Detail> tdlist = traning_DetailBLL.GetListHasClass(10, 1, where, "CreateDate desc", out total);
            return total > 0;
        }
        #endregion

        public string AlertIsCanRegister(int type, bool IsCanRegister)
        {
            string message = "";
            switch (type)
            {
                case 1: if (!IsCanRegister) { message = "任教学段不符合条件"; } break;
                case 2: if (!IsCanRegister) { message = "任教学科不符合条件"; } break;
                case 3: if (!IsCanRegister) { message = "任教年级不符合条件"; } break;
                case 4: if (!IsCanRegister) { message = "教师职称不符合条件"; } break;
                case 5: if (!IsCanRegister) { message = "学校范围不符合条件"; } break;
                case 6: if (!IsCanRegister) { message = "该班级已不能报名"; } break;
                default: message = "您不符合报名的条件"; break;
            }
            return message;
        }


        public static bool CheckIsCanRegister(int ClassId, int MemberId)
        {
            Organ_DetailBLL organ_DetailBLL = new Organ_DetailBLL();
            int ParentOrganId = 1;
            int oType = 1;
            int partitionId = Code.SiteCache.Instance.LoginInfo.PartitionId;
            int UserId = MemberId;
            int OrganId = 1;
            ParentOrganId = (int)organ_DetailBLL.GetModel(Code.SiteCache.Instance.ManageOrganId).ParentId;
            oType = (int)organ_DetailBLL.GetModel(Code.SiteCache.Instance.ManageOrganId).OType;
            OrganId = Code.SiteCache.Instance.ManageOrganId;
            Member_AccountBLL member_AccountBLL = new Member_AccountBLL();
            Member_Account ma = member_AccountBLL.GetModel(UserId, "");
            Member_BaseInfoBLL member_BaseInfoBLL = new Member_BaseInfoBLL();
            Member_BaseInfo m = member_BaseInfoBLL.GetModelByAccountId(UserId);
            string where = " and td.PartitionId='" + partitionId + "'and cd.PlanId='" + Code.SiteCache.Instance.PlanId + "' and cd.PartitionId='" + partitionId + "' and cd.Id='" + ClassId + "'";

            if (oType == 1)
            {
                where += " and td.OrganId='" + OrganId + "'";
            }
            else if (oType == 2)
            {
                if (m != null)
                {
                    where += " and ((css.StudySection in(select StudySection from Member_StudySection where AccountId='" + UserId + "' and Delflag='false')and css.delflag='false' )or cd.StudyLevel=1)";
                    where += " and ((cts.TeachSubject in(select TeachSubject from Member_TeachSubject where AccountId='" + UserId + "' and Delflag='false')and cts.delflag='false' )or cd.Subject=1)";
                    where += " and ((ctg.TeachGrade in(select TeachGrade from Member_TeachGrade where AccountId='" + UserId + "' and Delflag='false')and ctg.delflag='false' )or cd.TeachGrade=1)";
                    where += " and ((ctr.TeachRank in(select WorkRank from Member_WorkRank where AccountId='" + UserId + "' and Delflag='false')and ctr.delflag='false' )or cd.TeachRank=1)";
                    where += " and (CHARINDEX('," + OrganId + ",',','+cd.OrganRange+',')>0 or cd.OrganRange='0')";
                    where += " and (td.OrganId='" + ParentOrganId + "' or td.Range=2)";
                }
                else
                {
                    where += "and 1=2";
                }
            }
            else if (oType == 5)
            {
                if (m != null)
                {
                    where += " and ((css.StudySection in(select StudySection from Member_StudySection where AccountId='" + UserId + "' and Delflag='false')and css.delflag='false' )or cd.StudyLevel=1)";
                    where += " and ((cts.TeachSubject in(select TeachSubject from Member_TeachSubject where AccountId='" + UserId + "' and Delflag='false')and cts.delflag='false' )or cd.Subject=1)";
                    where += " and ((ctg.TeachGrade in(select TeachGrade from Member_TeachGrade where AccountId='" + UserId + "' and Delflag='false')and ctg.delflag='false' )or cd.TeachGrade=1)";
                    where += " and ((ctr.TeachRank in(select WorkRank from Member_WorkRank where AccountId='" + UserId + "' and Delflag='false')and ctr.delflag='false' )or cd.TeachRank=1)";
                    where += " and (CHARINDEX('," + ma.OrganId + ",',','+cd.OrganRange+',')>0 or cd.OrganRange='0')";
                    where += " and (td.OrganId='" + OrganId + "' or td.Range=2)";
                }
                else
                {
                    where += "and 1=2";
                }
            }
            else
            {
                where += " and 1=2";
            }
            Traning_DetailBLL traning_DetailBLL = new Traning_DetailBLL();
            int total = 0;
            List<Traning_Detail> tdlist = traning_DetailBLL.GetListHasClass(10, 1, where, "CreateDate desc", out total);
            return total > 0;
        }


        public static string ALertRegisterMessage(int ClassId, int MemberId)
        {
            CoursemarketController c = new CoursemarketController();
            string checkmessage = c.IsCanRegisterAll(ClassId, false, Convert.ToInt32(MemberId));
            return checkmessage;
        }


    }
}
