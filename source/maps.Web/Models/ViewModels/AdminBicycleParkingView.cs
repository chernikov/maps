using maps.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Models.ViewModels
{
    public class AdminBicycleParkingView : BicycleParkingView
    {
        [Required(ErrorMessage = "¬ибер≥ть м≥сто")]
        public int? CityID { get; set; }

        public double CityCenterLat { get; set; }

        public double CityCenterLng { get; set; }

        public int CityZoom { get; set; }

        private List<City> Cities
        {
            get
            {
                var repository = DependencyResolver.Current.GetService<IRepository>();
                return repository.Cities.ToList();
            }
        }

        public IEnumerable<SelectListItem> SelectListCity
        {
            get
            {
                foreach (var city in Cities)
                {
                    yield return new SelectListItem()
                    {
                        Value = city.ID.ToString(),
                        Text = city.Name,
                        Selected = CityID == city.ID
                    };
                }
            }
        }

    }
}