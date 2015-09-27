using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace maps.Web.Models.ViewModels
{
    public class SportAvatarView
    {
        public enum AvatarTypeEnum 
        {
            guy = 0x01, 
            lady = 0x02, 
            couple = 0x03
        }
        public string PhotoUrl { get; set; }

        public AvatarTypeEnum AvatarType { get; set; }

    }
}