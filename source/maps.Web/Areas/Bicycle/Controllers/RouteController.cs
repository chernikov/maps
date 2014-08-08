using maps.Model;
using maps.Web.Areas.Admin.Models;
using maps.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace maps.Web.Areas.Bicycle.Controllers
{
    public class RouteController : BicycleController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Map()
        {
            return View();
        }

        public ActionResult My()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveRoute(BycicleDirectionView bycicleDirectionView)
        {
            var bycicleDirection = (BycicleDirection)ModelMapper.Map(bycicleDirectionView, typeof(BycicleDirectionView), typeof(BycicleDirection));
            bycicleDirection.UserID = CurrentUser.ID;
            Repository.CreateBycicleDirection(bycicleDirection);
            ProcessDirection(bycicleDirection);
            Repository.ProcessBycicleDirection(bycicleDirection.ID);
            return Json(new { result = "ok" });
        }

        public ActionResult GetAll()
        {
            var list = Repository.BycicleDirections.Where(p => !p.Processed).ToList();

            return Json(new { result = "ok", data = list.Select(p => p.PolyLine) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBicycleRoutes(int id)
        {
            var bicycleLine = Repository.BicycleLines.FirstOrDefault(p => p.ID == id);

            if (bicycleLine != null)
            {
                var list = bicycleLine.BicycleDirectionLines.Select(p => p.BycicleDirection).ToList();

                return Json(new { result = "ok", data = list.Select(p => p.PolyLine) }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = "error" });

        }

        public ActionResult GetMap()
        {
            var list = Repository.BicycleLines.ToList();

            return Json(new
            {
                result = "ok",
                data = list.Select(p => new
                {
                    p.ID,
                    p.Start,
                    p.End,
                    p.Quantity
                })
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMine()
        {
            var list = Repository.BycicleDirections.Where(p => p.UserID == CurrentUser.ID).ToList();

            return Json(new { result = "ok", data = list.Select(p => p.PolyLine) }, JsonRequestBehavior.AllowGet);
        }

        private void ProcessDirection(BycicleDirection bicycleDirection)
        {
            var geoLinesArray = new List<GeoLine>();

            var polyline = bicycleDirection.Waypoints;
            var subGeoPoints = JsonConvert.DeserializeObject<List<GeoPoint>>(polyline);

            GeoPoint currentPoint = null;
            foreach (var point in subGeoPoints)
            {
                if (currentPoint != null)
                {
                    geoLinesArray.Add(new GeoLine()
                    {
                        Start = currentPoint,
                        End = point,
                        DirectionsIds = new List<int> { bicycleDirection.ID }
                    });
                }
                currentPoint = point;
            }
            foreach (var geoLine in geoLinesArray)
            {
                var exist = Repository.BicycleLines.FirstOrDefault(p => geoLine.Start.Lat == p.StartLat &&
                                                                        geoLine.Start.Lng == p.StartLng &&
                                                                        geoLine.End.Lat == p.EndLat &&
                                                                        geoLine.End.Lat == p.EndLat);
                if (exist != null)
                {
                    Repository.UpdateBicycleLineQuantity(exist.ID);

                    Repository.CreateBicycleDirectionLine(new BicycleDirectionLine()
                    {
                        BicycleDirectionID = bicycleDirection.ID,
                        BicycleLineID = exist.ID
                    });
                }
                else
                {
                    var entity = new BicycleLine()
                    {
                        Start = geoLine.Start.Position,
                        StartLat = geoLine.Start.Lat,
                        StartLng = geoLine.Start.Lng,
                        End = geoLine.End.Position,
                        EndLat = geoLine.End.Lat,
                        EndLng = geoLine.End.Lng,
                        Quantity = 1
                    };
                    Repository.CreateBicycleLine(entity);

                    Repository.CreateBicycleDirectionLine(new BicycleDirectionLine()
                    {
                        BicycleDirectionID = bicycleDirection.ID,
                        BicycleLineID = entity.ID
                    });
                }
            }
        }
    }
}