using maps.Model;
using maps.Web.Models.Api;
using maps.Web.Models.Dto;
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
            var list = Repository.BicycleParkings.Where(p => p.VerifiedDate != null || !p.Exist ).ToList();

            return Json(new { result = "ok", data = list.Select(p => 
                    new {
                        Id = p.ID,
                        Position = p.Position,
                        Exist = p.Exist,
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

        [Authorize]
        public ActionResult AddFuture()
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


        [HttpGet]
        public ActionResult PopupFuture()
        {
            return View();
        }

        public ActionResult CreateFuture()
        {
            return View("EditFuture", new BicycleParkingView());
        }

        [HttpPost]
        public ActionResult SaveFutureParking(BicycleParkingView bicycleParkingView)
        {
            var bycicleParking = (BicycleParking)ModelMapper.Map(bicycleParkingView, typeof(BicycleParkingView), typeof(BicycleParking));
            bycicleParking.UserID = CurrentUser.ID;
            bycicleParking.Exist = false;
            bycicleParking.VotesCount = 1;
            Repository.CreateBicycleParking(bycicleParking);

            Repository.CreateBicycleParkingVote(new BicycleParkingVote()
            {
                BicycleParkingID = bycicleParking.ID,
                UserID = CurrentUser.ID
            });

            return Json(new { result = "ok" });
        }

        public ActionResult SaveVote(int id)
        {
            var parkingVote =
                Repository.BicycleParkingVotes.FirstOrDefault(
                    p => p.BicycleParkingID == id && p.UserID == CurrentUser.ID);
            if (parkingVote == null)
            {
                Repository.CreateBicycleParkingVote(new BicycleParkingVote()
                {
                    BicycleParkingID = id,
                    UserID = CurrentUser.ID
                });
            }
            return Json(new {result = "ok"}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SampleForm()
        {
            var bicycleParking = new BicycleParkingApiModel();

            return View(bicycleParking);
        }
	}
}