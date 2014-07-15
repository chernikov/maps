using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using maps.Web.Global.Auth;
using maps.Web.Global.Config;
using maps.Model;
using maps.Web.Models.Mappers;
using maps.Web.Mappers;

namespace maps.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        public static string HostName = string.Empty;

        protected static string NotFoundPage = "~/not-found-page";

        protected static string LoginPage = "~/Login";

        [Inject]
        public IRepository Repository { get; set; }

        [Inject]
        public IAuthentication Auth { get; set; }

        [Inject]
        public IConfig Config { get; set; }

        [Inject]
        public ModelMapper ModelMapper { get; set; }

        public User CurrentUser
        {
            get
            {
                if (Auth.CurrentUser.Identity is IUserable)
                {
                    return ((IUserable)Auth.CurrentUser.Identity).User;
                }
                return null;
            }
        }

        public RedirectResult RedirectToNotFoundPage
        {
            get
            {
                return Redirect(NotFoundPage);
            }
        }


        public RedirectResult RedirectToLoginPage
        {
            get
            {
                return Redirect(LoginPage);
            }
        }

        public RedirectResult RedirectBack(string redirect)
        {
                if (Request.UrlReferrer != null)
                {
                    return Redirect(Request.UrlReferrer.ToString());
                }
                return Redirect(redirect);
        }

        protected override void Initialize(RequestContext requestContext)
        {
            if (requestContext.HttpContext.Request.Url != null)
            {
                HostName = requestContext.HttpContext.Request.Url.Authority;
            }
            base.Initialize(requestContext);
        }
    }
}