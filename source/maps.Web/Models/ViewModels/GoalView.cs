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

        [Required(ErrorMessage="������� ����")]
        [MaxLength(140, ErrorMessage = "���� �� ���� �� ����� 140 �������")]
		public string Text {get; set; }


    }
}