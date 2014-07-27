using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Models.ViewModels
{ 
	public class BicycleParkingView
    {
        public int ID { get; set; }

		public string Position {get; set; }

		public string PhotoUrl {get; set; }

        public string FullPhotoUrl
        {
            get
            {
                return string.IsNullOrWhiteSpace(PhotoUrl) ? "/Content/images/no_camera_sign.png" : PhotoUrl;
            }
        }

		public string Description {get; set; }

    }
}