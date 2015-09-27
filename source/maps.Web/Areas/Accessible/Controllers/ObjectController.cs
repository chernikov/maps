using maps.Model;
using maps.Web.Models.Info;
using maps.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Areas.Accessible.Controllers
{
    public class ObjectController : DefaultController
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Add()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            var list = Repository.AccessibleObjects.Where(p => p.VerifiedDate != null && p.CityID == CurrentCity.ID).ToList();
            return Json(new
            {
                result = "ok",
                data = list.Select(p =>
                    new
                    {
                        Id = p.ID,
                        Position = p.Position,
                        Category = p.Category,
                        Type = p.Type
                    })
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("Edit", new AccessibleObjectView());
        }

        [HttpPost]
        public ActionResult Edit(AccessibleObjectView accessibleObjectView)
        {
            if (ModelState.IsValid)
            {
                var accessibleObject = (AccessibleObject)ModelMapper.Map(accessibleObjectView, typeof(AccessibleObjectView), typeof(AccessibleObject));

                accessibleObject.UserID = CurrentUser.ID;
                accessibleObject.CityID = CurrentCity.ID;
                Repository.CreateAccessibleObject(accessibleObject);

               
                if (accessibleObjectView.Photos != null)
                {
                    foreach (var accessibleObjectPhotoView in accessibleObjectView.Photos)
                    {
                        var accessibleObjectPhoto = Repository.AccessibleObjectPhotos.FirstOrDefault(p => p.ID == accessibleObjectPhotoView.Value.ID);
                        if (accessibleObjectPhoto != null)
                        {
                            accessibleObjectPhoto.AccessibleObjectID = accessibleObject.ID;
                            Repository.UpdateAccessibleObjectPhoto(accessibleObjectPhoto);
                        }
                    }
                }

                return View("Thanks", new MessageInfo("Дякую!", "Дякую за повідомлення!"));
            }
            return View(accessibleObjectView);
        }



    }
}