using System.Web;
using System.Web.Mvc;

namespace Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new Attributes.ExceptionLogAttribute());//错误日志
            filters.Add(new Attributes.UserAuthorizeAttribute());//登录验证
            //filters.Add(new Attributes.GroupAuthorizeAttribute());//权限验证
        }
    }
}