using System.Web.Mvc;

namespace Web.Areas.Addition
{
    public class AdditionAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Addition";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Addition_default",
                "Addition/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
