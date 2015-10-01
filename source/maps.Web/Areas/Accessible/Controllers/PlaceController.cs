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
    public class PlaceController : DefaultController
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
            var list = Repository.AccessiblePlaces.Where(p => p.CityID == CurrentCity.ID).ToList();
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
            return View("Edit", new AccessiblePlaceView());
        }

        [HttpPost]
        public ActionResult Edit(AccessiblePlaceView accessiblePlaceView)
        {
            if (ModelState.IsValid)
            {
                var accessiblePlace = (AccessiblePlace)ModelMapper.Map(accessiblePlaceView, typeof(AccessiblePlaceView), typeof(AccessiblePlace));

                accessiblePlace.UserID = CurrentUser.ID;
                accessiblePlace.CityID = CurrentCity.ID;
                Repository.CreateAccessiblePlace(accessiblePlace);

               
                if (accessiblePlaceView.Photos != null)
                {
                    foreach (var accessiblePlacePhotoView in accessiblePlaceView.Photos)
                    {
                        var accessiblePlacePhoto = Repository.AccessiblePlacePhotos.FirstOrDefault(p => p.ID == accessiblePlacePhotoView.Value.ID);
                        if (accessiblePlacePhoto != null)
                        {
                            accessiblePlacePhoto.AccessiblePlaceID = accessiblePlace.ID;
                            Repository.UpdateAccessiblePlacePhoto(accessiblePlacePhoto);
                        }
                    }
                }

                return View("Thanks", new MessageInfo("Дякую!", "Дякую за повідомлення!"));
            }
            return View(accessiblePlaceView);
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
                var accessiblePlacePhoto = new Model.AccessiblePlacePhoto()
                {
                    ImagePath = "/" + uDir + uFile,
                };
                Repository.CreateAccessiblePlacePhoto(accessiblePlacePhoto);
                return new FineUploaderResult(true, new { accessiblePlacePhoto });
            }
            catch (Exception ex)
            {
                return new FineUploaderResult(false, error: ex.Message);
            }
        }

        public ActionResult Item(int id)
        {
            var accessiblePlacePhoto = Repository.AccessiblePlacePhotos.FirstOrDefault(p => p.ID == id);
            if (accessiblePlacePhoto != null)
            {
                var accessiblePlacePhotoView = (AccessiblePlacePhotoView)ModelMapper.Map(accessiblePlacePhoto, typeof(AccessiblePlacePhoto), typeof(AccessiblePlacePhotoView));
                return View("AccessiblePlacePhoto", new KeyValuePair<string, AccessiblePlacePhotoView>(Guid.NewGuid().ToString("N"), accessiblePlacePhotoView));
            }
            return null;
        }
    }
}