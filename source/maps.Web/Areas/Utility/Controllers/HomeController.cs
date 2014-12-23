using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using maps.Web.Models.ViewModels;
using maps.Model;
using maps.Web.Models.Info;

namespace maps.Web.Areas.Utility.Controllers
{
    public class HomeController : UtilityController
    {
        public ActionResult Index()
        {
            if (CurrentUser != null)
            {
                CurrentUser.CityID = 1;
                Repository.ChangeUserCity(CurrentUser);
            }
            else
            {
                Session["City"] = 1;
            }
            return View();
        }


        [Authorize]
        public ActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("Edit", new NewUtilityIssueView());
        }

        [HttpPost]
        public ActionResult Edit(NewUtilityIssueView newUtilityIssueView)
        {
            if (ModelState.IsValid)
            {
                var utilityIssue = (UtilityIssue)ModelMapper.Map(newUtilityIssueView, typeof(NewUtilityIssueView), typeof(UtilityIssue));

                utilityIssue.UserID = CurrentUser.ID;
                utilityIssue.CityID = CurrentCity.ID;
                Repository.CreateUtilityIssue(utilityIssue);

                if (newUtilityIssueView.UtilityTagList != null)
                {
                    foreach (var tagId in newUtilityIssueView.UtilityTagList)
                    {
                        var tag = Repository.UtilityTags.FirstOrDefault(p => p.ID == tagId);
                        if (tag != null)
                        {
                            Repository.CreateUtilityIssueTag(new UtilityIssueTag()
                            {
                                UtilityTagID = tag.ID,
                                UtilityIssueID = utilityIssue.ID
                            });
                        }
                    }
                }
                if (newUtilityIssueView.Photos != null)
                {
                    foreach (var utilityPhotoView in newUtilityIssueView.Photos)
                    {
                        var utilityPhoto = Repository.UtilityPhotos.FirstOrDefault(p => p.ID == utilityPhotoView.Value.ID);
                        if (utilityPhoto != null)
                        {
                            utilityPhoto.UserID = CurrentUser.ID;
                            utilityPhoto.UtilityIssueID = utilityIssue.ID;
                            Repository.UpdateUtilityPhoto(utilityPhoto);
                        }
                    }
                }

                return View("Thanks", new MessageInfo("Дякую!", "Дякую за повідомлення!"));
            }
            return View(newUtilityIssueView);
        }

        public ActionResult GetAll()
        {
            var list = Repository.UtilityIssues.Where(p => !p.IsRemoved  && p.Status != (int)UtilityIssue.StatusType.Closed).ToList();

            return Json(new
            {
                result = "ok",
                data = list.Select(p =>
                    new
                    {
                        Id = p.ID,
                        Lat = p.Lat,
                        Lng = p.Lng,
                        Status = p.StatusIcon
                    })
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Info(int id)
        {
            var item = Repository.UtilityIssues.FirstOrDefault(p => p.ID == id);
            if (item != null)
            {
                return View(item);
            }
            return null;
        }


        public ActionResult Item(int id)
        {
            var item = Repository.UtilityIssues.FirstOrDefault(p => p.ID == id);
            if (item != null)
            {
                return View(item);
            }
            return RedirectToAction("Index");
        }
    }
}