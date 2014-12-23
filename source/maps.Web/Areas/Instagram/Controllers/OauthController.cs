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
                if (Session["INSTAGRAM_REDIRECT"] != null)
                {
                    return Redirect(Session["INSTAGRAM_REDIRECT"] as string);
                }

                return RedirectToAction("Index", "Home");
            }
            return Content(result.ToString());
        }
    }
}