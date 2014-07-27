using maps.Model;
using maps.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Areas.Bicycle.Controllers
{
    public class RouteController : BicycleController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult My()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveRoute(BycicleDirectionView bycicleDirectionView)
        {
            var bycicleDirection = (BycicleDirection)ModelMapper.Map(bycicleDirectionView, typeof(BycicleDirectionView), typeof(BycicleDirection));
            bycicleDirection.UserID = CurrentUser.ID;
            Repository.CreateBycicleDirection(bycicleDirection);
            return Json(new { result = "ok" });
        }

        public ActionResult GetAll()
        {
            var list = Repository.BycicleDirections.ToList();

            return Json(new {result = "ok", data = list.Select(p => p.PolyLine) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMine()
        {
            var list = Repository.BycicleDirections.Where(p => p.UserID == CurrentUser.ID).ToList();

            return Json(new { result = "ok", data = list.Select(p => p.PolyLine) }, JsonRequestBehavior.AllowGet);
        }
	}
}