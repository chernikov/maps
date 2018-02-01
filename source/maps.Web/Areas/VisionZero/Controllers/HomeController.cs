using AutoMapper;
using ImageResizer;
using maps.Model;
using maps.Web.Global;
using maps.Web.Global.Search;
using maps.Web.Models.Info;
using maps.Web.Models.ViewModels;
using maps.Web.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Tool;

namespace maps.Web.Areas.VisionZero.Controllers
{
    public class HomeController : DefaultController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Item(int id)
        {
            ViewBag.ID = id;
            return View("Index");
        }

        [ValidateInput(false)]
        [HttpPost]
        public FineUploaderResult UploadFile(FineUpload upload)
        {
            try
            {
                upload.InputStream.Position = 0;
                var data = string.Empty;
                using (StreamReader reader = new StreamReader(upload.InputStream, Encoding.UTF8))
                {
                    data = reader.ReadToEnd();
                    var table = VisualizationTable.Parse(data);

                    var visualization = new Visualization()
                    {
                        Name = upload.Filename,
                        UserID = CurrentUser.ID,
                        ShareLink = StringExtension.CreateRandomPassword(10, allowedChars: "abcdefghijkmnopqrstuvwxyz0123456789")
                    };
                    while (true)
                    {
                        var existShareLink = Repository.Visualizations.Any(p => p.ShareLink == visualization.ShareLink);
                        if (existShareLink)
                        {
                            visualization.ShareLink = StringExtension.CreateRandomPassword(10, allowedChars: "abcdefghijkmnopqrstuvwxyz0123456789");
                        }
                        else
                        {
                            break;
                        };
                    }
                    Repository.CreateVisualization(visualization);
                    foreach (var column in table.Columns)
                    {
                        if (!table.HiddenIndexes.Contains(column.Number))
                        {
                            column.VisualizationID = visualization.ID;
                            Repository.CreateVisualizationColumn(column);
                        }
                    }
                    foreach (var item in table.Items)
                    {
                        item.VisualizationID = visualization.ID;
                        Repository.CreateVisualizationItem(item);
                    }
                }
            }
            catch (Exception ex)
            {
                return new FineUploaderResult(false, error: ex.Message);
            }
            return new FineUploaderResult(true);
        }


        public ActionResult Shared(string id)
        {
            var visualization = Repository.Visualizations.FirstOrDefault(p => string.Compare(p.ShareLink, id, true) == 0);
            if (visualization != null)
            {
                return View(visualization);
            }
            return RedirectToAction("Index");
        }


        public ActionResult ShareLink(int id)
        {
            var visualization = Repository.Visualizations.FirstOrDefault(p => p.ID == id);

            if (visualization != null)
            {
                return View(visualization);
            }
            return null;
        }

        public ActionResult List()
        {
            if (CurrentUser != null)
            {
                var list = Repository.Visualizations.Where(p => p.UserID == CurrentUser.ID || p.VisualizationUsers.Any(r => r.UserID == CurrentUser.ID)).ToList();
                return View(list);
            }
            return null;
        }

        public ActionResult Filter(int id)
        {
            var visualizationColumns = Repository.VisualizationColumns.Where(p => p.VisualizationID == id).ToList();
            var visualizationFilter = new VisualizationFilter(id, visualizationColumns);
            return View(visualizationFilter);
        }

        public ActionResult ShowData(int id)
        {
            var items = Repository.VisualizationItems
                .Where(p => p.VisualizationID == id && !p.IsHidden).Select(p => new
            {
                Id = p.ID,
                lat = p.Lat,
                lng = p.Lng,
                accuracy = p.Accuracy,
                data = p.Data
            }).ToList();
            return Json(new { result = "ok", data = items }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Shake(int id)
        {
            var items = Repository.VisualizationItems.Where(p => p.VisualizationID == id && p.Lat != 0 && p.Lng != 0).ToList();
            var shake = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < items.Count; i++)
            {
                var itemI = items[i];
                for (int j = i; j < items.Count; j++)
                {
                    var itemJ = items[j];

                    if (itemI.Lat == itemJ.Lat && itemI.Lng == itemJ.Lng)
                    {
                        itemI.Lat += (double)shake.Next(100) * 0.00001 - 0.0005;
                        itemI.Lng += (double)shake.Next(100) * 0.00001 - 0.0005;
                        Repository.UpdateVisualizationItem(itemI);
                    }
                }
            }
            return Json(new { result = "ok" });
        }

        [HttpPost]
        public ActionResult FilterData(VisualizationFilter filter)
        {
            var items = Repository.VisualizationItems.Where(p => p.VisualizationID == filter.ID && !p.IsHidden).ToList();

            var filteredItems = new List<VisualizationItem>();

            if (!string.IsNullOrWhiteSpace(filter.SearchString))
            {
                items = SearchEngine.Get(filter.SearchString, items.AsQueryable()).ToList();
            }
            foreach (var item in items)
            {
                var data = JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(item.Data);
                var pass = true;
                var i = 0;
                foreach (var field in filter.Fields)
                {
                    var value = data[i];
                    switch (field.Type)
                    {
                        case VisualizationColumn.VisualizationType.Date:
                            if (field.StartDate.HasValue || field.EndDate.HasValue)
                            {
                                var date = DateTime.Parse(value.Value);
                                if (field.StartDate.HasValue && field.StartDate.Value > date)
                                {
                                    pass = false;
                                }
                                if (field.EndDate.HasValue && field.EndDate.Value < date)
                                {
                                    pass = false;
                                }
                            }
                            break;
                        case VisualizationColumn.VisualizationType.Time:
                            if (field.StartTime.HasValue || field.EndTime.HasValue)
                            {
                                var time = TimeSpan.Parse(value.Value);
                                if (field.StartTime.HasValue && field.StartTime.Value > time)
                                {
                                    pass = false;
                                }
                                if (field.EndTime.HasValue && field.EndTime.Value < time)
                                {
                                    pass = false;
                                }
                            }
                            break;
                        case VisualizationColumn.VisualizationType.List:
                            if (field.Value != "All")
                            {
                                var filterValue = field.Value.ToLower().Trim();
                                if (value.Value == null ||
                                    value.Value.ToLower().Trim() != filterValue)
                                {
                                    pass = false;
                                }
                            }
                            break;
                    }
                    if (!pass)
                    {
                        break;
                    }
                    i++;
                }
                if (pass)
                {
                    filteredItems.Add(item);
                }
            }

            var jsonItems = filteredItems.Select(p => new
               {
                   Id = p.ID,
                   lat = p.Lat,
                   lng = p.Lng,
                   accuracy = p.Accuracy,
                   data = p.Data
               }).ToList();


            return Json(new { result = "ok", data = jsonItems }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Info(int id)
        {
            var item = Repository.VisualizationItems.FirstOrDefault(p => p.ID == id);
            var data = JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(item.Data);
            ViewBag.ID = id;
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var visualization = Repository.Visualizations.FirstOrDefault(p => p.ID == id);

            if (visualization != null && visualization.UserID == CurrentUser.ID)
            {
                Repository.RemoveVisualization(visualization.ID);
            }

            return RedirectToAction("Index");

        }

        public ActionResult Table(int id)
        {
            var visualization = Repository.Visualizations.FirstOrDefault(p => p.ID == id);

            if (visualization != null)
            {
                return View(visualization);
            }
            return null;
        }

        public ActionResult ChangeVisible(int id)
        {
            Repository.ChangeVisibleVisualizationItems(id);

            return Json(new { result = "ok" });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var visualizationItem = Repository.VisualizationItems.FirstOrDefault(p => p.ID == id);
            if (visualizationItem != null)
            {
                var visualizationItemView = Mapper.Map<VisualizationItem, VisualizationItemView>(visualizationItem);
                return View(visualizationItemView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        public ActionResult Edit(VisualizationItemView visualizationItemView)
        {
            var visualizationItem = Mapper.Map<VisualizationItemView, VisualizationItem>(visualizationItemView);
            var data = JsonConvert.SerializeObject(visualizationItemView.DataItems, new KeyValueConverter());
            visualizationItem.Accuracy = 10;
            visualizationItem.Data = data;
            Repository.UpdateVisualizationItem(visualizationItem);
            visualizationItem = Repository.VisualizationItems.First(p => p.ID == visualizationItem.ID);
            return RedirectToAction("Item", new { id = visualizationItem.VisualizationID });
        }
    }
}
