using maps.Model;
using maps.Web.Controllers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Areas.Housing.Controllers
{
    public class HomeController : BaseController
    { 

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(string input)
        {
            var rows = input.Split('\n');
            
            foreach(var row in rows)
            {
                var cols = row.Split('\t').ToList().Select(p => p.Trim()).ToList();

                var number = int.Parse(cols[0]) + 1;
                var name = cols[1].Replace(" ,", ", ");
                
                for(int i = 0; i < 11; i++)
                {
                    var year = 2005 + i;
                    var powerOn = int.Parse(cols[3 + i * 3]);
                    var consumed = int.Parse(cols[4 + i * 3]);

                    if (consumed > 0) {
                        var buildingElectricity = new BuildingElectricity()
                        {
                            BuildingID = number,
                            PowerOn = powerOn,
                            Year = year,
                            Consumed = consumed
                        };
                        Repository.CreateBuildingElectricity(buildingElectricity);
                    }
                }
            }
            return View();
        }


        public ActionResult Geo()
        {
            var list = Repository.Buildings.Where(p => p.Lat == 0).ToList();
            var webClient = new WebClient();
            foreach(var item in list)
            {
                try
                {
                    var address = HttpUtility.UrlEncode(item.Address);
                    var askUrl = "https://maps.googleapis.com/maps/api/geocode/json?key=AIzaSyBxdpYRxJgtDdNFXPh-o3Jcsi7f1mSm4pk&address=" + address;

                    var result = webClient.DownloadString(askUrl);

                    var obj = JObject.Parse(result);

                    var position = obj["results"][0]["geometry"]["location"];
                    item.Lat = position["lat"].Value<double>();
                    item.Lng = position["lng"].Value<double>();
                    Repository.UpdateBuilding(item);
                } catch { }
            }

            return null;
        }


        public ActionResult Data(int id)
        {
            var building = Repository.Buildings.FirstOrDefault(p => p.ID == id);
            if (building != null)
            {
                return View(building);
            }
            return null;
        }
    }
}
