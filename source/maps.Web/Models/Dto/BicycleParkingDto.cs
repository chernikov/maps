using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace maps.Web.Models.Dto
{
    public class BicycleParkingDto
    {
        public int ID { get; set; }

        public int Type { get; set; }

        public string Position { get; set; }

        private double? _lat;

        private double? _lng;

        public double Lat
        {
            get
            {
                if (!_lat.HasValue)
                {
                    ParsePosition();
                }
                return _lat ?? 0;
            }
        }

        public double Lng
        {
            get
            {
                if (!_lng.HasValue)
                {
                    ParsePosition();
                }
                return _lng ?? 0;
            }
        }


        
        private string _photoUrl;

        public string PhotoUrl
        {
            set { _photoUrl = value; }
            
        }

        public string FullPhotoUrl
        {
            get
            {
                return string.IsNullOrWhiteSpace(_photoUrl) ? "/Content/images/no_camera_sign.png" : _photoUrl;
            }
        }

        public string Description { get; set; }

        public string Address { get; set; }

        public int? Capacity { get; set; }

        private void ParsePosition()
        {
            var str = Position.Split(new[] {"(", ",", " ", ")"}, StringSplitOptions.RemoveEmptyEntries);

            _lat = double.Parse(str[0], CultureInfo.InvariantCulture);
            _lng = double.Parse(str[1], CultureInfo.InvariantCulture);
        }
    }
}