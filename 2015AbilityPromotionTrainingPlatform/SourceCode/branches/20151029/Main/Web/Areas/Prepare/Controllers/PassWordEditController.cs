using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Dianda.AP.Model;
using Dianda.AP.BLL;
using Dianda.Common.StringSecurity;

namespace Web.Areas.Prepare.Controllers
{
    public class PassWordEditController : Controller
    {
        //
        // GET: /Prepare/Account/
        UserBLL bll = new UserBLL();

        public ActionResult Index()
        {
            int AccountId = Code.SiteCache.Instance.LoginInfo.UserId;
            ViewData["UserPass"] = bll.GetPassWord(AccountId);
            ViewData["AccountId"] = AccountId;
            return View();
        }


        public ActionResult UpdPass(string PassWord)
        {
            int AccountId = Code.SiteCache.Instance.LoginInfo.UserId;
            if (bll.UpdPass(AccountId, PassWord))
            {
                return Content("yes:修改成功！！！");
            }
            else
            {
                return Content("no:修改失败！！！");
            }
        }
    }
}
