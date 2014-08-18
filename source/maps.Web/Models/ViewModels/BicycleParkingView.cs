using maps.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Models.ViewModels
{
    public class BicycleParkingView
    {
        public int ID { get; set; }

        public bool Exist { get; set; }

        public int Type { get; set; }

        public IEnumerable<SelectListItem> SelectListType
        {
            get
            {
                yield return new SelectListItem()
                {
                    Value = ((int)BicycleParking.TypeEnum.Wheel).ToString(),
                    Text = "Колесо",
                    Selected = Type == (int)BicycleParking.TypeEnum.Wheel
                };
                yield return new SelectListItem()
                {
                    Value = ((int)BicycleParking.TypeEnum.Frame).ToString(),
                    Text = "Рама",
                    Selected = Type == (int)BicycleParking.TypeEnum.Frame
                };
            }
        }

        public string Position { get; set; }

        public string PhotoUrl { get; set; }

        public string FullPhotoUrl
        {
            get
            {
                return string.IsNullOrWhiteSpace(PhotoUrl) ? "/Content/images/no_camera_sign.png" : PhotoUrl;
            }
        }

        public string Description { get; set; }

        public string Address { get; set; }

        public bool Lock { get; set; }

        public bool Camera { get; set; }

        public bool Rent { get; set; }

        public int? Capacity { get; set; }

        public double CenterDistance { get; set; }

        public BicycleParkingView()
        {
            Type = (int)BicycleParking.TypeEnum.Wheel;
        }

    }
}