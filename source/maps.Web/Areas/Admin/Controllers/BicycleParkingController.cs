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
            var list = Repository.BicycleParkings.ToList();
            return View(list);
        }

        public ActionResult Create()
        {
            var bicycleparkingView = new BicycleParkingView();
            return View("Edit", bicycleparkingView);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var bicycleparking = Repository.BicycleParkings.FirstOrDefault(p => p.ID == id);

            if (bicycleparking != null)
            {
                var bicycleparkingView = (BicycleParkingView)ModelMapper.Map(bicycleparking, typeof(BicycleParking), typeof(BicycleParkingView));
                return View(bicycleparkingView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        public ActionResult Edit(BicycleParkingView bicycleparkingView)
        {
            if (ModelState.IsValid)
            {
                var bicycleparking = (BicycleParking)ModelMapper.Map(bicycleparkingView, typeof(BicycleParkingView), typeof(BicycleParking));
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
    }
}