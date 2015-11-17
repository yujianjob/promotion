﻿using Dianda.AP.Model.Entrance.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Attributes
{
    public class GroupAuthorizeAttribute : ActionFilterAttribute
    {
        private List<string> ignores = new List<string>
        {
            "Entrance/Login",
            "Entrance/Home",
            "/SiteOption",
            "Api/Member",
            "Api/OutCourse",
            "Api/OutCourseSP"
        };

        public override void OnActionExecuting(ActionExecutingContext filterContext)
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
            string action = filterContext.ActionDescriptor.ActionName;

            if (!ignores.Exists(t => t.Equals(area + "/" + controller, StringComparison.CurrentCultureIgnoreCase)))
            {
                //暂时只判断非异步的get请求
                if (filterContext.HttpContext.Request.HttpMethod.Equals("GET", StringComparison.CurrentCultureIgnoreCase)
                    && !filterContext.HttpContext.Request.IsAjaxRequest())
                {

                    List<PlatformRoles> roles = Code.SiteCache.Instance.PlatformRoles;
                    if (!roles.Exists(t => t.PagePath.Equals(area + "/" + controller + "/" + action, StringComparison.CurrentCultureIgnoreCase)))
                    {

                        if (filterContext.HttpContext.Request.IsAjaxRequest())
                        {
                            filterContext.HttpContext.Response.Clear();
                            filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Redirect;
                            filterContext.Result = new ContentResult() { Content = "top.location.href='/SiteOption/ErrorFunction';" };
                            filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
                            filterContext.HttpContext.Response.End();
                        }
                        else
                        {
                            filterContext.Result = new RedirectResult("/SiteOption/ErrorFunction");
                        }
                    }
                }
            }
        }
    }
}