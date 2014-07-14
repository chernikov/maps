using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace maps.Web.Models.ViewModels.User
{
    public class UserView : BaseUserView
    {
        public int CurrentLang { get; set; }

        public bool IsCorrectLang { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AvatarPath { get; set; }

        public string FullAvatarPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(AvatarPath))
                {
                    return "/Content/images/default_avatar.jpg?w=260&h=200&mode=crop";
                }
                return AvatarPath + "?w=260&h=200&mode=crop";
            }
        }
    }
}