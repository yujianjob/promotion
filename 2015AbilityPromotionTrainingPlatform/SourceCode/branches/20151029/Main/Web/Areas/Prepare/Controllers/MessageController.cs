﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dianda.AP.BLL;
using Dianda.AP.Model;
using Dianda.Common;

namespace Web.Areas.Prepare.Controllers
{
    public class MessageController : Controller
    {
        //
        // GET: /Prepare/Message/
        Sys_MessageBLL bll = new Sys_MessageBLL();
        XXW.SiteUtils.SessionHelper session = XXW.SiteUtils.SessionHelper.Instance;
        public ActionResult Index()
        {
            session.SetSession("NotifyCount", null);
            int userid = Code.SiteCache.Instance.LoginInfo.UserId;
            int PartitionId = Code.SiteCache.Instance.LoginInfo.PartitionId;
            int pageIndex = 1, pageSize = 10, pageCount, recordCount;
            if (!String.IsNullOrEmpty(Request["PageIndex"]))
                int.TryParse(QueryString.Decrypt(Request["PageIndex"]), out pageIndex);
            string Type = QueryString.UrlEncrypt("NotRead");
            string where = "";
            if (!string.IsNullOrEmpty(Request["Type"]))
            {
                Type = Request["Type"];
            }
            if (QueryString.Decrypt(Type) == "NotRead")
            {
                where = "a.Status = 0 and a.Display = 1 and a.Delflag = 0 and a.AccountId = '" + userid + "' and a.PartitionId =" + PartitionId + "";
            }
            else if (QueryString.Decrypt(Type) == "Read")
            {
                where = "a.Status = 1 and a.Display = 1 and a.Delflag = 0 and a.AccountId = '" + userid + "' and a.PartitionId =" + PartitionId + "";
            }
            else
            {
                where = "a.Status = 2 and a.Display = 1 and a.Delflag = 0 and a.AccountId = '" + userid + "' and a.PartitionId =" + PartitionId + "";
            }

            List<Sys_Message> list = new List<Sys_Message>();
            list = bll.GetList(pageSize, pageIndex, where, "a.Id desc", out recordCount);
            ViewBag.GetList = list;
            pageCount = (int)Math.Ceiling((double)recordCount / pageSize);
            ViewData["pageSize"] = pageSize;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageCount"] = pageCount;
            ViewData["recordCount"] = recordCount;
            ViewData["Type"] = QueryString.Decrypt(Type);
            return View();
        }


        public ActionResult Details(string id, string Type)
        {
            string where = "";
            int Uid = Convert.ToInt32(QueryString.Decrypt(Request["id"]));
            Type = QueryString.Decrypt(Request["Type"]);
            if (Type == "NotRead")
            {
                where = "a.Status = 0 and a.Display = 1 and a.Delflag = 0 ";
            }
            else if (Type == "Read")
            {
                where = "a.Status = 1 and a.Display = 1 and a.Delflag = 0 ";
            }
            else
            {
                where = "a.Status = 2 and a.Display = 1 and a.Delflag = 0 ";
            }

            Sys_Message model = bll.GetModel(Uid, where);
            ViewBag.model = model;
            ViewData["Type"] = Type;
            if (Type == "NotRead")
            {
                int i = bll.UpdStatus(Uid);
            }
            return View();
        }

        public ActionResult DelDetail(int id,string Type)
        {
            int i = 0;
            if (Type == "NotRead")
            {
                i = bll.DelDetails(id," Status = 2 ");
            }
            else if (Type == "Read")
            {
                i = bll.DelDetails(id," Status = 2 ");
            }
            else
            {
                i = bll.DelDetails(id," Delflag = 1 ");
            }
            
            if (i > 0)
            {
                return Content("yes:删除成功!!! ");
            }
            else
            {
                return Content("no:删除失败!!!");
            }
        }


        public ActionResult BatchDel(string IdList,string Type)
        {
            int i = 0;
            if (Type == "NotRead")
            {
                i = bll.BatchDel(IdList, " Status = 2 ");
            }
            else if (Type == "Read")
            {
                i = bll.BatchDel(IdList, " Status = 2 ");
            }
            else
            {
                i = bll.BatchDel(IdList, " Delflag = 1 ");
            }
            if (i > 0)
            {
                return Content("yes:删除成功!!!");
            }
            else
            {
                return Content("no:删除失败!!!");
            }
        }
    }
}
