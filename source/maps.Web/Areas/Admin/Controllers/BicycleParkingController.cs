using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using maps.Web.Models.ViewModels;
using maps.Model;


namespace maps.Web.Areas.Admin.Controllers
{
    public class BicycleParkingController : AdminController
    {

        public ActionResult Index()
        {
            var list = Repository.BicycleParkings;
            if (CurrentCity != null)
            {
                list = list.Where(p => p.CityID == CurrentCity.ID);
            }
            return View(list.ToList());
        }


        public ActionResult Create()
        {
            var bicycleparkingView = new AdminBicycleParkingView()
            {
                CityID = CurrentCity != null ? (int?)CurrentCity.ID : null
            };
            return View("Edit", bicycleparkingView);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var bicycleparking = Repository.BicycleParkings.FirstOrDefault(p => p.ID == id);

            if (bicycleparking != null)
            {
                var bicycleparkingView = (AdminBicycleParkingView)ModelMapper.Map(bicycleparking, typeof(BicycleParking), typeof(AdminBicycleParkingView));
                return View(bicycleparkingView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        public ActionResult Edit(AdminBicycleParkingView bicycleparkingView)
        {
            if (ModelState.IsValid)
            {
                var bicycleparking = (BicycleParking)ModelMapper.Map(bicycleparkingView, typeof(AdminBicycleParkingView), typeof(BicycleParking));
                if (bicycleparking.ID == 0)
                {
                    bicycleparking.UserID = CurrentUser.ID;
                    Repository.CreateBicycleParking(bicycleparking);
                }
                else
                {
                    Repository.UpdateBicycleParking(bicycleparking);
                }
                return RedirectToAction("Index");
            }
            return View(bicycleparkingView);
        }

        public ActionResult Delete(int id)
        {
            var bicycleparking = Repository.BicycleParkings.FirstOrDefault(p => p.ID == id);
            if (bicycleparking != null)
            {
                Repository.RemoveBicycleParking(bicycleparking.ID);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Verify(int id)
        {
            Repository.VerifiedBicycleParking(id,true);

            return RedirectToAction("Index");
        }

        public ActionResult Unverify(int id)
        {
            Repository.VerifiedBicycleParking(id, false);

            return RedirectToAction("Index");
        }

        public ActionResult Distance()
        {
            return View();
        }

        public ActionResult List()
        {
            var list = Repository.BicycleParkings.ToList();
            return Json(new
            {
                result = "ok",
                data = list.Select(p =>
                    new
                    {
                        Id = p.ID,
                        Position = p.Position,
                        Exist = p.Exist,
                        Type = p.Type,
                        CityCenterLat = p.City.CenterLat,
                        CityCenterLng = p.City.CenterLng,
                    })
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveDistance(int id, double distance)
        {
            var item = Repository.BicycleParkings.FirstOrDefault(p => p.ID == id);
            if (item != null)
            {
                item.CenterDistance = distance;
                Repository.UpdateBicycleParking(item);
            }
            return Json(new { result = "ok" }, JsonRequestBehavior.AllowGet);
        }
    }
}