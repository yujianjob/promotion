using System.Web.Mvc;

namespace Web.Areas.ClassDomain
{
    public class ClassDomainAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ClassDomain";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ClassDomain_default",
                "ClassDomain/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
