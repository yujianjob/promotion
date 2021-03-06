﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dianda.AP.Model;
using Dianda.AP.BLL;
using System.Data;
using XXW.Enum;

namespace Web.Areas.Prepare.Controllers
{
    public class Standard_SettingController : Controller
    {
        //
        // GET: /Prepare/Standard_Setting/

        Training_CreditsBLL tcbll = new Training_CreditsBLL();

        public ActionResult setup_period()
        {
            ViewBag.GetFieCount = tcbll.GetFieCount();
            int OrganId = Code.SiteCache.Instance.ManageOrganId;
            int PlanId = Code.SiteCache.Instance.PlanId;
            DataTable dt=tcbll.GetList(" and a.PlanId = " + PlanId + " and a.OrganId = " + OrganId + " ", "Sort desc");
            ViewBag.GetList = dt;
            if (dt.Rows.Count != 0)
            {
                ViewData["Avg"] = tcbll.GetAvgCredits(OrganId,PlanId);
                ViewData["AvgId"] = tcbll.GetAvgId(OrganId, PlanId);
            }
            int GroupId = Code.SiteCache.Instance.GroupId;
            if (GroupId != 1)
            {
                ViewData["Organ"] = tcbll.GetOrgan(OrganId,PlanId);
            }
            ViewData["GroupId"] = GroupId;
            return View();
        }

        public ActionResult UpdateSetup()
        {
            string Value = "";
            if (!string.IsNullOrEmpty(Request["Value"]))
            {
                Value = Request["Value"];
            }

            int i = tcbll.UpdCredits(Value);
            if (i > 0)
            {
                return Content("yes:设置成功");
            }
            else
            {
                return Content("yes:设置出错");
            }
        }


        public ActionResult AddSetup()
        {
            string Value = "";
            int OrganId = Code.SiteCache.Instance.ManageOrganId;
            int PlanId = Code.SiteCache.Instance.PlanId;
            if (!string.IsNullOrEmpty(Request["Value"]))
            {
                Value = Request["Value"];
            }

            if (tcbll.YzAddCredits(OrganId, PlanId))
            {
                return Content("no:设置冲突,请刷新页面后重新设置");
            }
            if (tcbll.AddCredits(Value,OrganId,PlanId) > 0)
            {
                return Content("yes:设置成功");
            }
            else
            {
                return Content("yes:设置出错");
            }
        }
    }
}
