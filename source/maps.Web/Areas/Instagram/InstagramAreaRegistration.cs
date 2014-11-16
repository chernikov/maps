using System.Web.Mvc;

namespace maps.Web.Areas.Instagram
{
    public class InstagramAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Instagram";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Instagram_default",
                "instagram/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "maps.Web.Areas.Instagram.Controllers" }
            );
        }
    }
}