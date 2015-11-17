﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Attributes
{
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        private List<string> ignores = new List<string>
        {
            "Entrance/Login",
            "Api/Member",
            "Api/OutCourse",
            "Api/OutCourseSP"
            //"/UISource"
        };

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string area;
            if (filterContext.RouteData.DataTokens["area"] == null)
            {
                area = "";
            }
            else
            {
                area = filterContext.RouteData.DataTokens["area"].ToString();
            }
            string controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            if (!ignores.Exists(t => t.Equals(area + "/" + controller, StringComparison.CurrentCultureIgnoreCase)))
            {
                if (Code.SiteCache.Instance.LoginInfo == null)
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.HttpContext.Response.Clear();
                        filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Redirect;
                        filterContext.Result = new ContentResult() { Content = "top.location.href='/Entrance/Login/Index';" };
                        filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
                        filterContext.HttpContext.Response.End();
                    }
                    else
                    {
                        filterContext.Result = new RedirectResult("/Entrance/Login/Index");
                    }
                }
            }
        }
    }
}