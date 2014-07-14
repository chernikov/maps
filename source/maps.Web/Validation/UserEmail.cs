using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using maps.Web.Models.ViewModels.User;

namespace maps.Web.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class UserEmailValidationAttribute : UniqueValidationAttribute
    {
        protected override bool CheckProperty(object obj)
        {
            if (!(obj is BaseUserView))
                return true;
            var userView = obj as BaseUserView;
            return repository.Users.Count(p => string.Compare(p.Email, userView.Email, true) == 0 && p.ID != userView.ID) == 0;
        }
    }
}