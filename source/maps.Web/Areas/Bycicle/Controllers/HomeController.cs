using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using maps.Web.Models.ViewModels;
using maps.Model;

namespace maps.Web.Areas.Bycicle.Controllers
{
    public class HomeController : BycicleController
    {
        //
        // GET: /Bycicle/Home/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveRoute(BycicleDirectionView bycicleDirectionView)
        {
            var bycicleDirection = (BycicleDirection)ModelMapper.Map(bycicleDirectionView, typeof(BycicleDirectionView), typeof(BycicleDirection));
            bycicleDirection.UserID = 1;
            Repository.CreateBycicleDirection(bycicleDirection);
            return Json(new { result = "ok" });
        }
	}
}