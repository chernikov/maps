using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using maps.Model;


namespace maps.Web.Models.ViewModels
{ 
	public class ShopView
    {
        public int Id { get; set; }

		public int UserID {get; set; }

		public string Position {get; set; }

    }
}