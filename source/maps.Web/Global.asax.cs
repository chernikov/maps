using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using maps.Web.Global.Auth;
using System.Web.Http;
using maps.Web.App_Start;

namespace maps.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            AreaConfig.RegisterAreas(RouteTable.Routes);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            if (this.Context.Session != null)
            {
                var auth = DependencyResolver.Current.GetService<IAuthentication>();
                auth.AuthCookieProvider = new HttpContextCookieProvider(this.Context);
                this.Context.User = auth.CurrentUser;
            }
        }
    }
}
