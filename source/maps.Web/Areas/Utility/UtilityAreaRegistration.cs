using System.Web.Mvc;

namespace maps.Web.Areas.Utility
{
    public class UtilityAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Utility";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Utility_default",
                "utility/{controller}/{action}/{id}",
                new { controller="Home", action = "Index", id = UrlParameter.Optional },
                new[] { "maps.Web.Areas.Utility.Controllers" }
            );
        }
    }
}