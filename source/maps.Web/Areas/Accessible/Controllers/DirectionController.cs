using maps.Model;
using maps.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Areas.Accessible.Controllers
{
    public class DirectionController : DefaultController
    {
        // GET: Accessible/Destination
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
        public ActionResult Save(AccessibleDirectionView accessibleDirectionView)
        {
            var accessibleDirection = (AccessibleDirection)ModelMapper.Map(accessibleDirectionView, typeof(AccessibleDirectionView), typeof(AccessibleDirection));
            accessibleDirection.UserID = CurrentUser.ID;
            accessibleDirection.CityID = CurrentCity.ID;
            Repository.CreateAccessibleDirection(accessibleDirection);
            return Json(new { result = "ok" });
        }

        public ActionResult GetMine()
        {
            var list = Repository.AccessibleDirections.Where(p => p.UserID == CurrentUser.ID).ToList();

            return Json(new { result = "ok", data = list.Select(p => p.PolyLine) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMap()
        {
            var list = Repository.AccessibleDirections.Where(p => p.CityID == CurrentCity.ID).ToList();

            return Json(new { result = "ok", data = list.Select(p => p.PolyLine) }, JsonRequestBehavior.AllowGet);
        }
    }
}