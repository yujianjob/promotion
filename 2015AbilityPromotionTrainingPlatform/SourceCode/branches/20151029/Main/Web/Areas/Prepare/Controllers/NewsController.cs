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
    public class NewsController : Controller
    {
        //
        // GET: /Prepare/News/
        News_DetailBLL bll = new News_DetailBLL();

        public ActionResult Index()
        {
            int GroupId = Code.SiteCache.Instance.GroupId;
            int OrganId = Code.SiteCache.Instance.ManageOrganId;
            string where = "and Level in ('1','2','3','4')";
            if (GroupId == 2)
            {
                where += " and Level <> 1 and (OrganId = " + OrganId + " or OrganId in (select ParentId from Organ_Detail where Id = " + OrganId + ")) ";
            }
            else if (GroupId == 3 || GroupId == 4)
            {
                where += " and Level <> 1 and Level <> 2 and OrganId =" + OrganId + "";
            }
            int pageIndex = 1, pageSize = 10, pageCount, recordCount;
            if (!String.IsNullOrEmpty(Request["PageIndex"]))
                int.TryParse(QueryString.Decrypt(Request["PageIndex"]), out pageIndex);
            List<News_Detail> list = new List<News_Detail>();

            if (!string.IsNullOrEmpty(Request["wheretype"]))
            {
                where += " and Level = '" + Request["wheretype"].ToString().Replace("'", "%").Replace("\\", "%").Replace("[]", "[\\]") + "' ";
            }
            if (!string.IsNullOrEmpty(Request["whereplan"]))
            {
                where += " and PlanId = '" + Request["whereplan"].ToString().Replace("'", "%").Replace("\\", "%").Replace("[]", "[\\]") + "' ";
            }

            list = bll.GetList(pageSize, pageIndex, where, "a.sort desc", out recordCount);
            ViewBag.GetList = list;
            pageCount = (int)Math.Ceiling((double)recordCount / pageSize);
            ViewData["pageSize"] = pageSize;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageCount"] = pageCount;
            ViewData["recordCount"] = recordCount;
            ViewData["wheretype"] = Request["wheretype"];
            ViewData["whereplan"] = Request["whereplan"];
            ViewData["Plan"] = bll.GetPlan();
            ViewData["GroupId"] = GroupId;
            return View();
        }

        public ActionResult List()
        {
            int GroupId = Code.SiteCache.Instance.GroupId;
            int OrganId = Code.SiteCache.Instance.ManageOrganId;
            int PlanId = Code.SiteCache.Instance.PlanId;
            string where = " and a.DisPlay = 1 and PublishDate <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
            if (GroupId == 1)
            {
                where += " and Level in ('1','2','3','4') ";
            }
            else if (GroupId == 2)
            {
                where += " and (Level in ('1') or (OrganId = " + OrganId + " or OrganId in (select id from Organ_Detail where ParentId = " + OrganId + "))) ";
            }
            else
            {
                where += " and (Level in ('1') or (OrganId = " + OrganId + " or OrganId in (select ParentId from Organ_Detail where Id = " + OrganId + "))) ";
            }

            int pageIndex = 1, pageSize = 10, pageCount, recordCount;
            if (!String.IsNullOrEmpty(Request["PageIndex"]))
                int.TryParse(QueryString.Decrypt(Request["PageIndex"]), out pageIndex);
            List<News_Detail> list = new List<News_Detail>();

            if (!string.IsNullOrEmpty(Request["wheretype"]))
            {
                where += " and Level = '" + Request["wheretype"].ToString().Replace("'", "%").Replace("\\", "%").Replace("[]", "[\\]") + "' ";
            }
            if (!string.IsNullOrEmpty(Request["whereplan"]))
            {
                where += " and PlanId = '" + Request["whereplan"].ToString().Replace("'", "%").Replace("\\", "%").Replace("[]", "[\\]") + "' ";
            }
            list = bll.GetList(pageSize, pageIndex, where, "a.sort desc", out recordCount);
            ViewBag.GetList = list;
            pageCount = (int)Math.Ceiling((double)recordCount / pageSize);
            ViewData["pageSize"] = pageSize;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageCount"] = pageCount;
            ViewData["recordCount"] = recordCount;
            ViewData["wheretype"] = Request["wheretype"];
            ViewData["whereplan"] = Request["whereplan"];
            ViewData["Plan"] = bll.GetPlan();
            ViewData["GroupId"] = GroupId;
            return View();
        }

        public ActionResult Details(string id, string State)
        {
            int Uid = int.Parse(QueryString.Decrypt(id));
            State = QueryString.Decrypt(State);
            News_Detail model = bll.GetModel(Uid, " and a.Delflag = 0 ");
            ViewBag.Model = model;
            ViewData["State"] = State;
            return View();
        }

        public ActionResult NewsAdd()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewsAdd(News_Detail model)
        {
            model.CreateDate = DateTime.Now;
            model.Creater = Code.SiteCache.Instance.LoginInfo.UserId;
            model.Delflag = false;
            model.OrganId = Code.SiteCache.Instance.ManageOrganId;
            model.PlanId = Code.SiteCache.Instance.PlanId;
            model.Level = Code.SiteCache.Instance.GroupId;
            if (bll.YzTitle(model.Title, model.Id))
            {
                return Content("<script>alert('公告标题重复');location.href = '/Prepare/News/NewsAdd'</script>");
            }

            bool flag = bll.Add(model);
            if (flag)
            {
                return Content("<script>alert('添加成功');location.href = '/Prepare/News/Index'</script>");
            }
            else
            {
                return Content("<script>alert('添加失败');location.href = '/Prepare/News/Index'</script>");
            }
        }

        [ValidateInput(false)]
        public ActionResult NewsEdit(string id)
        {
            int i = Convert.ToInt32(QueryString.Decrypt(id));
            News_Detail model = bll.GetModel(i, " and a.Delflag = 0 ");
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewsEdit(News_Detail model)
        {
            model.Creater = Code.SiteCache.Instance.LoginInfo.UserId;
            model.OrganId = Code.SiteCache.Instance.ManageOrganId;
            model.PlanId = Code.SiteCache.Instance.PlanId;

            if (bll.YzTitle(model.Title, model.Id))
            {
                return Content("<script>alert('公告标题重复');location.href='/Prepare/News/NewsEdit?Id=" +QueryString.UrlEncrypt(model.Id) + "';</script>");
            }

            bool flag = bll.Update(model);
            if (flag)
            {
                return Content("<script>alert('编辑成功');location.href = '/Prepare/News/Index'</script>");
            }
            else
            {
                return Content("<script>alert('编辑失败');location.href = '/Prepare/News/Index'</script>");
            }
        }


        public ActionResult NewsDel(int id)
        {
            try
            {
                bool flag = bll.NewsDel(id);
                if (flag)
                {
                    return Content("yes:删除成功");
                }
                else
                {
                    return Content("no:删除失败");
                }
            }
            catch (Exception)
            {
                return Content("no:超时");
            }
        }
    }
}
