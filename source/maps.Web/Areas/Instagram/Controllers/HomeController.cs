using maps.Instagram;
using maps.Model;
using maps.Web.Models.Info;
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
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public ActionResult Index()
        {
            logger.Debug("Instagram log");
            SaveRedirect("/instagram");
            TryGetCookieAccess();
            if (!InstagramApiCaller.IsReady)
            {
                return Redirect(InstagramApiCaller.Auth(CallbackUri));
            }
            return View();
        }

        public ActionResult Get(double lat, double lng, int radius, DateTime maxTime, DateTime minTime)
        {
            TryGetCookieAccess();
            if (!InstagramApiCaller.IsReady)
            {
                return Json(new { result = "error" });
            }
            var resultList = new List<InstagramPhotoInfo>();
            var lastDateTime = maxTime;
            var maxTimes = 10;
            do
            {
                var result = InstagramApiCaller.SearchMedia(lat, lng, radius, lastDateTime.ToUniversalTime());

                var data = (JArray)result["data"];
                foreach (var item in data)
                {
                    dynamic obj = JObject.Parse(item.ToString());
                    logger.Debug(string.Format("Id resource : {0} {1}", obj.id, InstagramPhotoInfo.UnixTimeStampToDateTime(obj.created_time.ToString())));
                    if (obj.type == "image")
                    {
                        var photo = new InstagramPhotoInfo()
                        {
                            GlobalId = obj.id,
                            CreatedTime = InstagramPhotoInfo.UnixTimeStampToDateTime(obj.created_time.ToString()),
                            Lat = double.Parse(obj.location.latitude.ToString()),
                            Lng = double.Parse(obj.location.longitude.ToString()),
                            PhotoUrl = obj.images.standard_resolution.url,
                            Tags = obj.tags.ToString(),
                            UserName = obj.user.username,
                            UserGlobalId = long.Parse(obj.user.id.ToString()),
                            UserFullName = obj.user.full_name,
                            Link = obj.link,
                        };
                        if (!string.IsNullOrWhiteSpace(obj.caption.ToString()))
                        {
                            photo.Caption = obj.caption.text;
                        }
                        if (!resultList.Any(p => p.GlobalId == photo.GlobalId))
                        {
                            resultList.Add(photo);
                        }
                    }
                }
                if (resultList.Count == 0)
                {
                    break;
                }
                var minListTime = resultList.OrderBy(p => p.CreatedTime).First().CreatedTime;
                if (minListTime == lastDateTime)
                {
                    break;

                }
                if (minListTime < minTime)
                {
                    break;
                }
                lastDateTime = minListTime;
                maxTimes--;
            } while (maxTimes > 0);
            return Json(new { result = "success", data = resultList });
        }

        public ActionResult EmptyModal()
        {
            return View();
        }
    }
}