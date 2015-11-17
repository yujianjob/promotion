using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dianda.AP.Model;
using Dianda.AP.BLL;
using System.Data;
using HttpMultipartParser;
using System.IO;
using System.Text.RegularExpressions;

namespace Web.Areas.Prepare.Controllers
{
    public class AccountEditController : Controller
    {
        //
        // GET: /Prepare/AccountEdit/
        AccountEditBLL bll = new AccountEditBLL();

        public ActionResult Index()
        {
            int AccountId = Code.SiteCache.Instance.LoginInfo.UserId;
            DataTable Nationdt = bll.GetNation(1);
            DataTable Politicaldt = bll.GetNation(3);
            DataTable EduLeveldt = bll.GetNation(4);
            DataTable EduDegreedt = bll.GetNation(5);
            DataTable EduMajordt = bll.GetNation(6);
            DataTable Jobdt = bll.GetTraning_InfoFk(10);
            DataTable Regindt = bll.GetOutRegin(5, 4);
            DataTable WorkRankdt = bll.GetTraning_InfoFk(8);
            DataTable TeachStudySectiondt = bll.GetTraning_InfoFk(4);
            DataTable TeachSubjectdt = bll.GetTraning_InfoFk(3);
            DataTable TeachGradedt = bll.GetTraning_InfoFk(7);
            DataTable TraningTypedt = bll.GetTraning_InfoFk(5);
            DataTable TraningObjectdt = bll.GetTraning_InfoFk(2);
            List<SelectListItem> Nation = new List<SelectListItem>();
            List<SelectListItem> Political = new List<SelectListItem>();
            List<SelectListItem> EduLevel = new List<SelectListItem>();
            List<SelectListItem> EduDegree = new List<SelectListItem>();
            List<SelectListItem> EduMajor = new List<SelectListItem>();
            List<SelectListItem> Regin = new List<SelectListItem>();
            List<SelectListItem> Job = new List<SelectListItem>();
            List<SelectListItem> WorkRank = new List<SelectListItem>();
            List<SelectListItem> TraningType = new List<SelectListItem>();
            List<SelectListItem> TraningObject = new List<SelectListItem>();
                
            for (int i = 0; i < Nationdt.Rows.Count; i++)
            {
                SelectListItem item = new SelectListItem();
                item.Text = Nationdt.Rows[i][1].ToString();
                item.Value = Nationdt.Rows[i][0].ToString();
                Nation.Add(item);
            }
            for (int i = 0; i < Politicaldt.Rows.Count; i++)
            {
                SelectListItem item = new SelectListItem();
                item.Text = Politicaldt.Rows[i][1].ToString();
                item.Value = Politicaldt.Rows[i][0].ToString();
                Political.Add(item);
            }
            for (int i = 0; i < EduLeveldt.Rows.Count; i++)
            {
                SelectListItem item = new SelectListItem();
                item.Text = EduLeveldt.Rows[i][1].ToString();
                item.Value = EduLeveldt.Rows[i][0].ToString();
                EduLevel.Add(item);
            }
            for (int i = 0; i < EduDegreedt.Rows.Count; i++)
            {
                SelectListItem item = new SelectListItem();
                item.Text = EduDegreedt.Rows[i][1].ToString();
                item.Value = EduDegreedt.Rows[i][0].ToString();
                EduDegree.Add(item);
            }
            for (int i = 0; i < EduMajordt.Rows.Count; i++)
            {
                SelectListItem item = new SelectListItem();
                item.Text = EduMajordt.Rows[i][1].ToString();
                item.Value = EduMajordt.Rows[i][0].ToString();
                EduMajor.Add(item);
            }
            for (int i = 0; i < Regindt.Rows.Count; i++)
            {
                SelectListItem item = new SelectListItem();
                item.Text = Regindt.Rows[i][1].ToString();
                item.Value = Regindt.Rows[i][0].ToString();
                Regin.Add(item);
            }
            for (int i = 0; i < Jobdt.Rows.Count; i++)
            {
                SelectListItem item = new SelectListItem();
                item.Text = Jobdt.Rows[i][1].ToString();
                item.Value = Jobdt.Rows[i][0].ToString();
                Job.Add(item);
            }
            for (int i = 0; i < WorkRankdt.Rows.Count; i++)
            {
                SelectListItem item = new SelectListItem();
                item.Text = WorkRankdt.Rows[i][1].ToString();
                item.Value = WorkRankdt.Rows[i][0].ToString();
                WorkRank.Add(item);
            }
            for (int i = 0; i < TraningTypedt.Rows.Count; i++)
            {
                SelectListItem item = new SelectListItem();
                item.Text = TraningTypedt.Rows[i][1].ToString();
                item.Value = TraningTypedt.Rows[i][0].ToString();
                TraningType.Add(item);
            }
            for (int i = 0; i < TraningObjectdt.Rows.Count; i++)
            {
                SelectListItem item = new SelectListItem();
                item.Text = TraningObjectdt.Rows[i][1].ToString();
                item.Value = TraningObjectdt.Rows[i][0].ToString();
                TraningObject.Add(item);
            }
            
            ViewData["NationList"] = Nation;
            ViewData["PoliticalList"] = Political;
            ViewData["EduLevelList"] = EduLevel;
            ViewData["EduDegreeList"] = EduDegree;
            ViewData["EduMajorList"] = EduMajor;
            ViewData["ReginList"] = Regin;
            ViewData["OrganList"] = Regin;
            ViewData["JobList"] = Job;
            ViewData["WorkRankList"] = WorkRank;
            ViewData["TeachStudySectiondt"] = TeachStudySectiondt;
            ViewData["TeachSubjectdt"] = TeachSubjectdt;
            ViewData["TeachGradedt"] = TeachGradedt;
            ViewData["TraningTypeList"] = TraningType;
            ViewData["TraningObjectList"] = TraningObject;

            AccountEdit model = bll.GetModel(AccountId);
            ViewData["UserName"] = Code.SiteCache.Instance.UserName.ToString();
            ViewData["Email"] = bll.GetEmail(AccountId);
            ViewData["Region"] = bll.GetAccountOrgan(AccountId).Rows[0][1];
            ViewData["OrganId"] = bll.GetAccountOrgan(AccountId).Rows[0][0];
            if (bll.GetAccountCount(Code.SiteCache.Instance.LoginInfo.UserId) == 0)
            {
                if(model==null)
                {
                    model = new AccountEdit();
                }
                
                ViewData["Type"] = "Add";
            }
            else
            {
                ViewData["Type"] = "Edit";
                model.TeachStudySection = bll.TeachStudySection(model.AccountId);
                model.TeachSubject = bll.TeachSubject(model.AccountId);
                model.TeachGrade = bll.TeachGrade(model.AccountId);
                ViewData["Nation"] = model.Nation;
                ViewData["PoliticalStatus"] = model.PoliticalStatus;
                ViewData["EduLevel"] = model.EduLevel;
                ViewData["EduDegree"] = model.EduDegree;
                ViewData["EduMajor"] = model.EduMajor;
                ViewData["Sex"] = model.Sex;
                ViewData["Job"] = model.Job;
                ViewData["WorkRank"] = model.WorkRank;
                ViewData["TeachStudySection"] = model.TeachStudySection;
                ViewData["TeachSubject"] = model.TeachSubject;
                ViewData["TeachGrade"] = model.TeachGrade;
                ViewData["TraningType"] = model.TraningType;
                ViewData["TraningObject"] = model.TraningObject;
            }
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult Edit(AccountEditModel model)
        {
            if (bll.Edit(model))
            {
                return Content("<script>alert('修改成功！！！');location.href='/Prepare/AccountEdit/List';</script>");
            }
            else
            {
                return Content("<script>alert('修改失败！！！');location.href='/Prepare/AccountEdit/Index';</script>");
            }
        }


        public ActionResult List()
        {
            DataTable dt = bll.GetList(Code.SiteCache.Instance.LoginInfo.UserId);
            dt.Columns.Add("TeachStudySection");
            dt.Columns.Add("TeachSubject");
            dt.Columns.Add("TeachGrade");
            if (dt.Rows.Count > 0)
            {
                dt.Rows[0]["TeachStudySection"] = bll.TeachStudySectionTo(Code.SiteCache.Instance.LoginInfo.UserId);
                dt.Rows[0]["TeachSubject"] = bll.TeachSubjectTo(Code.SiteCache.Instance.LoginInfo.UserId);
                dt.Rows[0]["TeachGrade"] = bll.TeachGradeTo(Code.SiteCache.Instance.LoginInfo.UserId);
            }
            else
            {

            }
            ViewData["UserName"] = Code.SiteCache.Instance.UserName;
            ViewData["Email"] = bll.GetEmail(Code.SiteCache.Instance.LoginInfo.UserId);
            ViewBag.GetModel = dt;
            return View();
        }

        [ValidateInput(false)]
        public ActionResult Add(AccountEditModel model)
        {
            model.AccountId = Code.SiteCache.Instance.LoginInfo.UserId;
            if (model != null)
            {
                if (bll.Add(model))
                {
                    return Content("<script>alert('修改成功！！！');location.href='/Prepare/AccountEdit/List';</script>");
                }
                else
                {
                    return Content("<script>alert('修改失败！！！');location.href='/Prepare/AccountEdit/Index';</script>");
                }
            }
            else
            {
                return Content("<script>alert('数据为空！！！');location.href='/Prepare/AccountEdit/Index';</script>");
            }
        }


        

        [ValidateInput(false)]
        public ActionResult GetRegion(int id)
        {
            DataTable Regindt = bll.GetRegionTo(id);
            List<SelectListItem> Regin = new List<SelectListItem>();
            for (int i = 0; i < Regindt.Rows.Count; i++)
            {
                SelectListItem item = new SelectListItem();
                item.Text = Regindt.Rows[i][1].ToString();
                item.Value = Regindt.Rows[i][0].ToString();
                Regin.Add(item);
            }
            return Json(Regin, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public ActionResult GetTeacherNo(string TeacherNo)
        {
            if (bll.GetTeacherNo(TeacherNo))
            {
                return Content("no:师训编号已存在");
            }
            else
            {
                return Content("yes:师训编号不存在");
            }
        }
    }
}
