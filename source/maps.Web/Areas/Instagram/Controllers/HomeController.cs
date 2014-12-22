using maps.Instagram;
using maps.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Areas.Instagram.Controllers
{
    public class HomeController : InstagramController
    {
        public ActionResult Index()
        {
            return View();
        }
        
        
        public ActionResult Old()
        {
            return View();
        }

        public ActionResult Photos()
        {
            var list = Repository.InstagramPhotoes.Where(p => p.CityID == CurrentCity.ID).OrderByDescending(p => p.ID).ToList();
            return View(list);
        }

        public ActionResult Photo(int id)
        {
            var item = Repository.InstagramPhotoes.FirstOrDefault(p => p.ID == id);
            if (item != null)
            {
                return View(item);
            }
            return RedirectToNotFoundPage;
        }

        public ActionResult GetMore()
        {
            if (string.IsNullOrWhiteSpace(InstagramApiCaller.Code))
            {
                return Redirect(InstagramApiCaller.Auth(CallbackUri));
            }
            if (InstagramApiCaller.JsonAccess != null)
            {
                for (int i = 0; i < 10; i++)
                {
                    var lastEntry = Repository.InstagramPhotoes.Where(p => p.CityID == CurrentCity.ID).OrderBy(p => p.CreatedTime).FirstOrDefault();
                    DateTime lastDateTime;
                    if (lastEntry != null)
                    {
                        lastDateTime = lastEntry.CreatedTime.ToUniversalTime();
                    }
                    else
                    {
                        lastDateTime = DateTime.UtcNow;
                    }
                    var result = InstagramApiCaller.SearchMedia(CurrentCity.CenterLat, CurrentCity.CenterLng, 3000, lastDateTime);
                    var data = (JArray)result["data"];

                    foreach (var item in data)
                    {
                        dynamic obj = JObject.Parse(item.ToString());
                        if (obj.type == "image")
                        {
                            var photo = new InstagramPhoto()
                            {
                                GlobalId = obj.id,
                                CityID = CurrentCity.ID,
                                CreatedTime = InstagramPhoto.UnixTimeStampToDateTime(obj.created_time.ToString()),
                                Lat = double.Parse(obj.location.latitude.ToString()),
                                Lng = double.Parse(obj.location.longitude.ToString()),
                                PhotoUrl = obj.images.standard_resolution.url,
                                Tags = obj.tags.ToString(),
                                UserName = obj.user.username,
                                UserGlobalId = long.Parse(obj.user.id.ToString()),
                                UserFullName = obj.user.full_name
                            };
                            if (!string.IsNullOrWhiteSpace(obj.caption.ToString()))
                            {
                                photo.Caption = obj.caption.text;
                            }
                            Repository.CreateInstagramPhoto(photo);
                        }
                    }
                }
            }
            var totalPhotos = Repository.InstagramPhotoes.Where(p => p.CityID == CurrentCity.ID).Count();
            return Content(totalPhotos.ToString());
        }

        public ActionResult List()
        {
            var data = Repository.InstagramPhotoes.Where(p => p.CityID == CurrentCity.ID)
                .GroupBy(p => p.UserGlobalId)
                .Select(grp => grp.First())
                .Select(p => new
                {
                    Id = p.ID,
                    Lat = p.Lat,
                    Lng = p.Lng,
                    PhotoUrl = p.PhotoUrl
                }).ToList();
            return Json(new { result = "ok", data }, JsonRequestBehavior.AllowGet);
        }
    }
}