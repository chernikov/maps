using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace maps.Web.Areas.Admin.Models
{
    [JsonObject]
    public class GeoPoint
    {
        [JsonProperty("k")]
        public double Lat { get; set; }

        [JsonProperty("B")]
        public double Lng { get; set; }

        public int Quantity { get; set; }

        public override string ToString()
        {
            return string.Format("Lat:{0}, Lng:{1} Q:{2}", Lat, Lng, Quantity);
        }

        public string Position
        {
            get
            {
                return string.Format("({0},{1})", Lat.ToString(CultureInfo.InvariantCulture), Lng.ToString(CultureInfo.InvariantCulture));
            }
        }
    }
}