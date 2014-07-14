using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using maps.Model;


namespace maps.Web.Models.ViewModels
{ 
	public class BycicleDirectionView
    {

        public int ID { get; set; }

		public int UserID {get; set; }

		public string Waypoints {get; set; }

		public string PolyLine {get; set; }

		public DateTime AddedDate {get; set; }

    }
}