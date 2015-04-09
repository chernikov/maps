using maps.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Models.ViewModels
{
    public class CreateReportView
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public int? RouteID { get; set; }

        private List<Route> _routes
        {
            get
            {
                var _repository = DependencyResolver.Current.GetService<IRepository>();
                return _repository.Routes.ToList();
            }
        }

        public IEnumerable<SelectListItem> SelectListRoutes
        {
            get
            {
                yield return new SelectListItem()
                {
                    Value = "",
                    Text = "Виберіть номер маршруту",
                    Selected = !RouteID.HasValue
                };
                foreach (var item in _routes)
                {
                    yield return new SelectListItem()
                    {
                        Value = item.ID.ToString(),
                        Text = item.Name,
                        Selected = RouteID == item.ID
                    };
                }
            }
        }

        public int? StationID { get; set; }

        public int? BusID { get; set; }

        public string BusNumber { get; set; }

        public int Status { get; set; }

        public int Type { get; set; }

        public DateTime DateTime { get; set; }

        public string Description { get; set; }

        public List<int> SelectedRules { get; set; }

        public List<Rule> Rules
        {
            get
            {
                var _repository = DependencyResolver.Current.GetService<IRepository>();
                return _repository.Rules.OrderBy(p => p.IsRouteScope ? 0 : 1).ToList();
            }
        }

        public Dictionary<string, ReportPhotoView> Photos { get; set; }

        public CreateReportView()
        {
            SelectedRules = new List<int>();
        }
    }

}
