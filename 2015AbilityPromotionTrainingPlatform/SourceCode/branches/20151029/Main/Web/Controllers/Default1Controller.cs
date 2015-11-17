using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class Default1Controller : Controller
    {
        //
        // GET: /Default1/

        public ActionResult Index()
        {
            XXW.SiteUtils.SessionHelper.Instance.SetSession("key","value");
           string a= XXW.SiteUtils.SessionHelper.Instance.GetSession("key").ToString ();
            Dianda.CacheFactory.CacheHelper.Instance.Set("Member_{Id}_MsgListCnt","3");
            string b = Dianda.CacheFactory.CacheHelper.Instance.Get("a").ToString ();
            Dianda.Common.StringSecurity.MD5Lib.Encrypt("abc");
            return Content(a);
        }

    }
}
