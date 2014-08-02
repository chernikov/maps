﻿using maps.Model;
using maps.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Areas.Bicycle.Controllers
{
    public class ParkingController : BicycleController
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetAll()
        {
            var list = Repository.BicycleParkings.ToList();

            return Json(new { result = "ok", data = list.Select(p => 
                    new {
                        Id = p.ID,
                        Position = p.Position,
                        Type = p.Type}) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Info(int id)
        {
            var item = Repository.BicycleParkings.FirstOrDefault(p => p.ID == id);

            if (item != null)
            {
                return View(item);
            }
            return null;
        }

        [Authorize]
        public ActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Popup()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View("Edit", new BicycleParkingView());
        }

        [HttpPost]
        public ActionResult SaveParking(BicycleParkingView bicycleParkingView)
        {
            var bycicleParking = (BicycleParking)ModelMapper.Map(bicycleParkingView, typeof(BicycleParkingView), typeof(BicycleParking));
            bycicleParking.UserID = CurrentUser.ID;
            bycicleParking.Exist = true;

            Repository.CreateBicycleParking(bycicleParking);
            
            return Json(new { result = "ok" });
        }

	}
}