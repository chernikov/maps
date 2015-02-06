using maps.Instagram;
using maps.Web.Controllers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Areas.Instagram.Controllers
{
    public abstract class InstagramController : BaseController
    {

        protected string SessionIstagramName = "INSTAGRAM_INSTANCE";
        protected string IstagramCookieCodeName = "INSTAGRAM_CODE_COOKIE";
        protected string IstagramCookieAccessName = "INSTAGRAM_ACCESS_COOKIE";

        protected string CallbackUri 
        {
            get 
            {
                return "http://" + HostName + "/instagram/oauth";
            }
        }

        protected InsagramApiCaller InstagramApiCaller
        {
            get
            {
                if (Session[SessionIstagramName] == null)
                {
                    Session[SessionIstagramName] = new InsagramApiCaller();
                }
                return (InsagramApiCaller)Session[SessionIstagramName];
            }
        }


        public InstagramController()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;


        }

        public void SaveRedirect(string redirect)
        {
            Session["INSTAGRAM_REDIRECT"] = redirect;
        }

        protected void TryGetCookieAccess()
        {
            var codeCookie = Request.Cookies.Get(IstagramCookieCodeName);
            if (codeCookie != null)
            {
                InstagramApiCaller.Code = codeCookie.Value;
            }
            var accessCookie = Request.Cookies.Get(IstagramCookieAccessName);
            if (accessCookie != null)
            {
                var access = JObject.Parse(Server.UrlDecode(accessCookie.Value));
                InstagramApiCaller.JsonAccess = access;
            }
        }
    }
}