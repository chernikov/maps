using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using maps.Model;
using System.ComponentModel.DataAnnotations;


namespace maps.Web.Models.ViewModels
{ 
	public class CityView
    {

        public int ID { get; set; }

        [Required(ErrorMessage="¬вед≥ть назву м≥ста")]
		public string Name {get; set; }

		public double CenterLat {get; set; }

		public double CenterLng {get; set; }

		public int Zoom {get; set; }

    }
}