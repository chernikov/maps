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

        public ActionResult Edit(int id)
        {
            var accessibleDirection = CurrentUser.AccessibleDirections.FirstOrDefault(p => p.ID == id);
            if (accessibleDirection != null)
            {
                return View(accessibleDirection);
            }
            return null;
        }

        public ActionResult Remove(int id)
        {
            var accessibleDirection = CurrentUser.AccessibleDirections.FirstOrDefault(p => p.ID == id);
            if (accessibleDirection != null)
            {
                Repository.RemoveAccessibleDirection(id);
                return RedirectToAction("My");
            }
            return null;
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(AccessibleDirectionView accessibleDirectionView)
        {
            var accessibleDirection = (AccessibleDirection)ModelMapper.Map(accessibleDirectionView, typeof(AccessibleDirectionView), typeof(AccessibleDirection));
            accessibleDirection.UserID = CurrentUser.ID;
            accessibleDirection.CityID = CurrentCity.ID;
            if (accessibleDirectionView.ID == 0)
            {
                Repository.CreateAccessibleDirection(accessibleDirection);
            }
            else
            {
                Repository.UpdateAccessibleDirection(accessibleDirection);
            }
            return Json(new { result = "ok" });
        }

        public ActionResult GetMine()
        {
            var list = Repository.AccessibleDirections.Where(p => p.UserID == CurrentUser.ID).ToList();

            return Json(new
            {
                result = "ok",
                data = list.Select(p => new
                    {
                        Polyline = p.PolyLine,
                        Id = p.ID
                    })
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMap()
        {
            var list = Repository.AccessibleDirections.Where(p => p.CityID == CurrentCity.ID).ToList();

            return Json(new { result = "ok", data = list.Select(p => p.PolyLine) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Polyline(int id)
        {
            var item = Repository.AccessibleDirections.FirstOrDefault(p => p.ID == id);
            if (item != null)
            {
                return Json(new { result = "ok", data = item.PolyLine, waypoints = item.Waypoints }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = "error" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectAction(int id)
        {
            var accessibleDirection = CurrentUser.AccessibleDirections.FirstOrDefault(p => p.ID == id);
            if (accessibleDirection != null)
            {
                return View(accessibleDirection);
            }
            return null;
        }
    }
}