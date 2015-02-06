using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using maps.Model;
using System.ComponentModel.DataAnnotations;


namespace maps.Web.Models.ViewModels
{ 
	public class PlaceView
    {

        public int ID { get; set; }

        [Required(ErrorMessage="¬вед≥ть назву")]
		public string Name {get; set; }

		public double Lat {get; set; }

		public double Lng {get; set; }

		public int Zoom {get; set; }

        public int Distance { get; set; }

    }
}