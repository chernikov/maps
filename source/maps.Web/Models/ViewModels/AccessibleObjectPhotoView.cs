using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace maps.Web.Models.ViewModels
{
    public class AccessibleObjectPhotoView
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public string Image { get; set; }

        public int State { get; set; }
    }
}