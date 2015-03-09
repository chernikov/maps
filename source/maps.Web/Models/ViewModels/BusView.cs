using maps.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Models.ViewModels
{
    public class BusView
    {
        public int ID { get; set; }

        public string Number { get; set; }

        public int BrandID { get; set; }


        private List<Brand> _brands
        {
            get
            {
                var repository = DependencyResolver.Current.GetService<IRepository>();
                return repository.Brands.ToList();
            }
        }

        public IEnumerable<SelectListItem> SelectListBrands
        {
            get
            {
                foreach (var item in _brands)
                {
                    yield return new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.ID.ToString(),
                        Selected = BrandID == item.ID
                    };
                }
            }
        }

       

        public int RouteID { get; set; }

        private List<Route> _routes
        {
            get
            {
                var repository = DependencyResolver.Current.GetService<IRepository>();
                return repository.Routes.ToList();
            }
        }

        public IEnumerable<SelectListItem> SelectListRoutes
        {
            get
            {
                foreach (var item in _routes)
                {
                    yield return new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.ID.ToString(),
                        Selected = RouteID == item.ID
                    };
                }
            }
        }

        public int TransporteurID { get; set; }

        private List<Transporteur> _transporteurs
        {
            get
            {
                var repository = DependencyResolver.Current.GetService<IRepository>();
                return repository.Transporteurs.ToList();
            }
        }

        public IEnumerable<SelectListItem> SelectListTranspourteur
        {
            get
            {
                foreach (var item in _transporteurs)
                {
                    yield return new SelectListItem()
                    {
                        Text = string.Format("{0} {1} {2}", item.LastName, item.FirstName, item.Patronymic),
                        Value = item.ID.ToString(),
                        Selected = TransporteurID == item.ID
                    };
                }
            }
        }
    }
}