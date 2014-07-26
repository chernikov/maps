using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using maps.Web.Models.ViewModels;
using maps.Model;

namespace maps.Web.Areas.Bicycle.Controllers
{
    public class HomeController : BicycleController
    {
        //
        // GET: /Bycicle/Home/
        public ActionResult Index()
        {
            return View();
        }

	}
}