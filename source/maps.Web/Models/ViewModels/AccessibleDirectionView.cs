using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using maps.Model;


namespace maps.Web.Models.ViewModels
{ 
	public class AccessibleDirectionView
    {

        public int ID { get; set; }

		public int UserID {get; set; }

		public string Waypoints {get; set; }

		public string PolyLine {get; set; }

		public double Length { get; set; }
    }
}