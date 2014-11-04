using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using maps.Model;


namespace maps.Web.Models.ViewModels
{ 
	public class UtilityIssueView : NewUtilityIssueView
    {
		public int ID { get; set; }

        public int Status { get; set; }

        public string UtilityDepartmentName { get; set; }

        public string WorkFlow { get; set; }

        public CityView City { get; set; }

    }
}