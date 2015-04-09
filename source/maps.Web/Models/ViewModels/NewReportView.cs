using maps.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Models.ViewModels
{
    public class NewReportView : CreateReportView
    {
        public int ID { get; set; }

        [Required(ErrorMessage="Вкажіть прізвище")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Вкажіть ім'я")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Вкажіть мобільний телефон")]
        [Phone(ErrorMessage="Вкажіть номер телефону у форматі 095-777-66-55")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Вкажіть код, який прийшов на мобільний телефон")]
        public string Code { get; set; }
    }

}
