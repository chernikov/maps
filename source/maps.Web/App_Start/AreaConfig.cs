using maps.Web.Areas.Accessible;
using maps.Web.Areas.Admin;
using maps.Web.Areas.Bicycle;
using maps.Web.Areas.Bus;
using maps.Web.Areas.Default;
using maps.Web.Areas.Goal;
using maps.Web.Areas.Instagram;
using maps.Web.Areas.Sport;
using maps.Web.Areas.Utility;
using maps.Web.Areas.VisionZero;
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

            var busArea = new BusAreaRegistration();
            var busAreaContext = new AreaRegistrationContext(busArea.AreaName, routes);
            busArea.RegisterArea(busAreaContext);

            var bycicleArea = new BicycleAreaRegistration();
            var bycicleAreaContext = new AreaRegistrationContext(bycicleArea.AreaName, routes);
            bycicleArea.RegisterArea(bycicleAreaContext);

            var instagramArea = new InstagramAreaRegistration();
            var instagramAreaContext = new AreaRegistrationContext(instagramArea.AreaName, routes);
            instagramArea.RegisterArea(instagramAreaContext);

            var goalArea = new GoalAreaRegistration();
            var goalAreaContext = new AreaRegistrationContext(goalArea.AreaName, routes);
            goalArea.RegisterArea(goalAreaContext);

            var utilityArea = new UtilityAreaRegistration();
            var utilityAreaContext = new AreaRegistrationContext(utilityArea.AreaName, routes);
            utilityArea.RegisterArea(utilityAreaContext);

            var accessibleArea = new AccessibleAreaRegistration();
            var accessibleAreaContext = new AreaRegistrationContext(accessibleArea.AreaName, routes);
            accessibleArea.RegisterArea(accessibleAreaContext);

            var sportArea = new SportAreaRegistration();
            var sportAreaContext = new AreaRegistrationContext(sportArea.AreaName, routes);
            sportArea.RegisterArea(sportAreaContext);


            var visionZeroArea = new VisionZeroAreaRegistration();
            var visionZeroAreaContext = new AreaRegistrationContext(visionZeroArea.AreaName, routes);
            visionZeroArea.RegisterArea(visionZeroAreaContext);

            var defaultArea = new DefaultAreaRegistration();
            var defaultAreaContext = new AreaRegistrationContext(defaultArea.AreaName, routes);
            defaultArea.RegisterArea(defaultAreaContext);
        }
    }
}