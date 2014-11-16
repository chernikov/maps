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
                return RedirectToAction("GetMore", "Home");
            }
            return Content(result.ToString());
        }
    }
}