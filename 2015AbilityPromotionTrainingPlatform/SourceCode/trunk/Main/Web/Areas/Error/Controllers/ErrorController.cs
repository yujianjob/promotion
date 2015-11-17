using Dianda.AP.BLL;
using Dianda.AP.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Error.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Statistics/Statistics/

        //错误页面500
        public ActionResult Error()
        {
            return View();
        }

        //错误页面406
        public ActionResult Forbin()
        {
            return View();
        }

        //错误页面404
        public ActionResult NotFound()
        {
            return View();
        }
    }
}
