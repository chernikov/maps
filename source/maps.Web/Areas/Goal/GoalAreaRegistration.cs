using System.Web.Mvc;

namespace maps.Web.Areas.Goal
{
    public class GoalAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Goal";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                null,
                "goal/Home/Item/{url}",
                new { controller = "Home", action = "Item", url = UrlParameter.Optional },
                new[] { "maps.Web.Areas.Goal.Controllers" }
            );

            context.MapRoute(
                null,
                "goal/Home/Item/{url}",
                new { controller = "Home", action = "Item", url = UrlParameter.Optional },
                new[] { "maps.Web.Areas.Goal.Controllers" }
            );

            context.MapRoute(
                null,
                "goal/Home/Create",
                new { controller = "Home", action = "Create" },
                new[] { "maps.Web.Areas.Goal.Controllers" }
            );

            context.MapRoute(
                null,
                "goal/Home/Set/{id}",
                new { controller = "Home", action = "Set", id = UrlParameter.Optional },
                new[] { "maps.Web.Areas.Goal.Controllers" }
            );

            context.MapRoute(
                null,
                "goal/Home/Reset/{id}",
                new { controller = "Home", action = "Reset", id = UrlParameter.Optional },
                new[] { "maps.Web.Areas.Goal.Controllers" }
            );
            
            context.MapRoute(
                null,
                "goal/Home/Remove/{id}",
                new { controller = "Home", action = "Remove", id = UrlParameter.Optional },
                new[] { "maps.Web.Areas.Goal.Controllers" }
            );

            context.MapRoute(
                null,
                "goal/{url}",
                new { controller = "Home", action = "Index", url = UrlParameter.Optional },
                new[] { "maps.Web.Areas.Goal.Controllers" }
            );

            context.MapRoute(
                null,
                "goal/{controller}/{action}/{id}",
                new { controller="Home", action = "Index", id = UrlParameter.Optional },
                new[] { "maps.Web.Areas.Goal.Controllers" }
            );
        }
    }
}