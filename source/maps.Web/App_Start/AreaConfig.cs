using maps.Web.Areas.Admin;
using maps.Web.Areas.Bicycle;
using maps.Web.Areas.Default;
using maps.Web.Areas.Goal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace maps.Web.App_Start
{
    public class AreaConfig
    {
        public static void RegisterAreas(RouteCollection routes)
        {
            var adminArea = new AdminAreaRegistration();
            var adminAreaContext = new AreaRegistrationContext(adminArea.AreaName, routes);
            adminArea.RegisterArea(adminAreaContext);

            var bycicleArea = new BicycleAreaRegistration();
            var bycicleAreaContext = new AreaRegistrationContext(bycicleArea.AreaName, routes);
            bycicleArea.RegisterArea(bycicleAreaContext);

            var goalArea = new GoalAreaRegistration();
            var goalAreaContext = new AreaRegistrationContext(goalArea.AreaName, routes);
            goalArea.RegisterArea(goalAreaContext);

            var defaultArea = new DefaultAreaRegistration();
            var defaultAreaContext = new AreaRegistrationContext(defaultArea.AreaName, routes);
            defaultArea.RegisterArea(defaultAreaContext);
        }
    }
}