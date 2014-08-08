using maps.Model;
using maps.Web.Areas.Admin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Areas.Admin.Controllers
{
    public class BicycleRouteController : AdminController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            var list = Repository.BycicleDirections.Where(p => p.Processed).ToList();

            return Json(new { result = "ok", data = list.Select(p => new {p.ID, p.PolyLine}) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveLength(int id, double length)
        {
            var item = Repository.BycicleDirections.FirstOrDefault(p => p.ID == id);
            if (item != null)
            {
                item.Length = length;
                Repository.UpdateBycicleDirection(item);

            }
            return Json(new {result = "OK"}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Generate()
        {
            Repository.ClearAllBicycleLines();

            var list = Repository.BycicleDirections.ToList();

            var geoLinesArray = new List<GeoLine>();
            foreach (var entity in list)
            {
                var polyline = entity.Waypoints;
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
                            DirectionsIds = new List<int> { entity.ID }
                        });
                    }
                    currentPoint = point;
                }
                Repository.ProcessBycicleDirection(entity.ID);
            }

            var quantityList = new List<GeoLine>();
            var TOLERANCE = 0.00001;
            foreach (var entity in geoLinesArray)
            {
                var exist = quantityList.FirstOrDefault(p =>
                    (Math.Abs(p.Start.Lat - entity.Start.Lat) < TOLERANCE &&
                    Math.Abs(p.Start.Lng - entity.Start.Lng) < TOLERANCE &&
                    Math.Abs(p.End.Lat - entity.End.Lat) < TOLERANCE &&
                    Math.Abs(p.End.Lng - entity.End.Lng) < TOLERANCE) ||
                     (Math.Abs(p.End.Lat - entity.Start.Lat) < TOLERANCE &&
                    Math.Abs(p.End.Lng - entity.Start.Lng) < TOLERANCE &&
                    Math.Abs(p.Start.Lat - entity.End.Lat) < TOLERANCE &&
                    Math.Abs(p.Start.Lng - entity.End.Lng) < TOLERANCE));
                if (exist != null)
                {
                    exist.Quantity++;
                    exist.DirectionsIds.Add(entity.DirectionsIds.First());
                }
                else
                {
                    quantityList.Add(new GeoLine
                    {
                        Start = entity.Start,
                        End = entity.End,
                        Quantity = 1,
                        DirectionsIds = new List<int>() { entity.DirectionsIds[0] },
                    });
                }
            }

            foreach (var line in quantityList)
            {
                var entity = new BicycleLine()
                {
                    Start = line.Start.Position,
                    StartLat = line.Start.Lat,
                    StartLng = line.Start.Lng,
                    End = line.End.Position,
                    EndLat = line.End.Lat,
                    EndLng = line.End.Lng,
                    Quantity = line.Quantity,
                };
                Repository.CreateBicycleLine(entity);

                foreach (var id in line.DirectionsIds)
                {
                    Repository.CreateBicycleDirectionLine(new BicycleDirectionLine()
                    {
                        BicycleDirectionID = id,
                        BicycleLineID = entity.ID
                    });
                }
            }

            TempData["message"] = "Обробку завершено!";
            return RedirectToAction("Index", "Home");
        }
    }
}