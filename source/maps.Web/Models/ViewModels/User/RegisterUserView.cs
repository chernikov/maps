using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using maps.Web.Validation;

namespace maps.Web.Models.ViewModels.User
{
    [PropertiesMustMatch("Password", "ConfirmPassword", ErrorMessage = "Password doesn't match")]
    public class RegisterUserView : BaseUserView
    {
        [Required(ErrorMessage = "Enter Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password error")]
        [Compare("Password", ErrorMessage = "Password doesn't match")]
        public string ConfirmPassword { get; set; }

        public string Captcha { get; set; }
    }
}