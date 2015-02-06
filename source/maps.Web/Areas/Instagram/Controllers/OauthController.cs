using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Areas.Instagram.Controllers
{
    public class OauthController : InstagramController
    {
        public ActionResult Index(string code)
        {
            InstagramApiCaller.Code = code;
            var result = InstagramApiCaller.GetAccess(CallbackUri);
            if (result != null)
            {
                Response.Cookies.Add(SaveToCookie(IstagramCookieCodeName, code));
                Response.Cookies.Add(SaveToCookie(IstagramCookieAccessName, result.ToString()));
                if (Session["INSTAGRAM_REDIRECT"] != null)
                {
                    return Redirect(Session["INSTAGRAM_REDIRECT"] as string);
                }

                return RedirectToAction("Index", "Home");
            }
            return Content(result.ToString());
        }

        private HttpCookie SaveToCookie(string name, string code)
        {
            var codeCookie = new HttpCookie(name);
            codeCookie.Value = code;
            codeCookie.Expires = DateTime.Now.AddYears(10);
            return codeCookie;
        }
    }
}