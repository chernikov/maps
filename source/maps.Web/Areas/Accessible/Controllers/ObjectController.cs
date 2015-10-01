using ImageResizer;
using maps.Model;
using maps.Web.Global;
using maps.Web.Models.Info;
using maps.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tool;

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
            var list = Repository.AccessibleObjects.Where(p => p.CityID == CurrentCity.ID).ToList();
            return Json(new
            {
                result = "ok",
                data = list.Select(p =>
                    new
                    {
                        Id = p.ID,
                        Lat = p.Lat,
                        Lng = p.Lng,
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


        [ValidateInput(false)]
        [HttpPost]
        public FineUploaderResult Upload(FineUpload upload)
        {
            var uDir = "Content/files/accessible/";
            var extension = Path.GetExtension(upload.Filename);
            var uFile = StringExtension.GenerateNewFile() + extension;
            var filePath = Path.Combine(Path.Combine(Server.MapPath("~"), uDir), uFile);
            try
            {
                ImageBuilder.Current.Build(upload.InputStream, filePath, new ResizeSettings("maxwidth=1600&crop=auto"));
                var accessibleObjectPhoto = new Model.AccessibleObjectPhoto()
                {
                    ImagePath = "/" + uDir + uFile,
                };
                Repository.CreateAccessibleObjectPhoto(accessibleObjectPhoto);
                return new FineUploaderResult(true, new { accessibleObjectPhoto });
            }
            catch (Exception ex)
            {
                return new FineUploaderResult(false, error: ex.Message);
            }
        }

        public ActionResult Item(int id)
        {
            var accessibleObjectPhoto = Repository.AccessibleObjectPhotos.FirstOrDefault(p => p.ID == id);
            if (accessibleObjectPhoto != null)
            {
                var accessibleObjectPhotoView = (AccessibleObjectPhotoView)ModelMapper.Map(accessibleObjectPhoto, typeof(AccessibleObjectPhoto), typeof(AccessibleObjectPhotoView));
                return View("AccessibleObjectPhoto", new KeyValuePair<string, AccessibleObjectPhotoView>(Guid.NewGuid().ToString("N"), accessibleObjectPhotoView));
            }
            return null;
        }
    }
}