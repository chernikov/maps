using System.Web.Mvc;

namespace maps.Web.Areas.Accessible
{
    public class AccessibleAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Accessible";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Accessible_default",
                "accessible/{controller}/{action}/{id}",
                new { controller="Home", action = "Index", id = UrlParameter.Optional },
                new[] { "maps.Web.Areas.Accessible.Controllers" }
            );
        }
    }
}