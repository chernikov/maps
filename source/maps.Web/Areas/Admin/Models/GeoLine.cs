using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace maps.Web.Areas.Admin.Models
{
    public class GeoLine
    {
        public GeoPoint Start { get; set; }

        public GeoPoint End { get; set; }

        public int Quantity { get; set; }

        public override string ToString()
        {
            return string.Format("Start:{0}, End:{1} QNT:{2} ", Start, End, Quantity);
        }


        public List<int> DirectionsIds { get; set; }


    }
}