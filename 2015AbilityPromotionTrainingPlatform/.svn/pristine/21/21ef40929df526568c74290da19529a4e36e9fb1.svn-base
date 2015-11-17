using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Attributes
{
    public class UrlDecryptAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            foreach (ParameterDescriptor pd in filterContext.ActionDescriptor.GetParameters())
            {
                if (pd.ParameterType == typeof(int) || pd.ParameterType == typeof(int?) || pd.ParameterType == typeof(string))
                {
                    string enstring = filterContext.HttpContext.Request.QueryString.Get(pd.ParameterName);
                    if (!string.IsNullOrEmpty(enstring))
                    {
                        string decstring = Dianda.Common.QueryString.Decrypt(HttpUtility.UrlDecode(enstring));
                        if (!string.IsNullOrEmpty(decstring))
                        {
                            if (pd.ParameterType == typeof(string))
                            {
                                filterContext.ActionParameters[pd.ParameterName] = decstring;
                            }
                            else
                            {
                                int outId;
                                if (int.TryParse(decstring, out outId))
                                {
                                    filterContext.ActionParameters[pd.ParameterName] = outId;
                                }
                                else
                                {
                                    if (pd.ParameterType == typeof(int))
                                    {
                                        filterContext.ActionParameters[pd.ParameterName] = pd.DefaultValue == null ? 0 : pd.DefaultValue;
                                    }
                                    else
                                    {
                                        filterContext.ActionParameters[pd.ParameterName] = pd.DefaultValue;
                                    }
                                }
                            }
                        }
                        else
                        {
                            ContentResult result = new ContentResult();
                            result.Content = "<script>setTimeout(function () { window.location.href = '/Entrance/Home/Index?GroupId="
                                + Code.SiteCache.Instance.GroupId + "' }, 3000);</script>操作失败,3秒后自动返回到首页！";
                            filterContext.Result = result;
                        }
                    }
                }
            }
        }
    }

    class UrlModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            throw new NotImplementedException();
        }
    }
}