using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using maps.Model;
using System.ComponentModel.DataAnnotations;


namespace maps.Web.Models.ViewModels
{ 
	public class GoalView
    {
        public int ID { get; set; }

		public bool IsPublic {get; set; }

		public string Url {get; set; }

        [Required(ErrorMessage="Додайте опис")]
        [MaxLength(140, ErrorMessage = "Опис має буди не довше 140 символів")]
		public string Text {get; set; }


    }
}