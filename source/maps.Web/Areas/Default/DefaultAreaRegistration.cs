using System.Web.Mvc;

namespace maps.Web.Areas.Default
{
    public class DefaultAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Default";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Default_default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "maps.Web.Areas.Default.Controllers" }
            );
        }
    }
}