using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace maps.Web.Models.Info
{
    public class InstagramPhotoInfo
    {
        public string GlobalId { get; set; }


        public double Lat { get; set; }

        public double Lng { get; set; }

        public DateTime CreatedTime { get; set; }

        public string PhotoUrl { get; set; }

        public string Caption { get; set; }

        public string Tags { get; set; }

        public string UserName { get; set; }

        public long UserGlobalId { get; set; }

        public string UserFullName { get; set; }

        public string Link { get; set; }


        public static DateTime UnixTimeStampToDateTime(string unixTimeStamp)
        {
            var seconds = long.Parse(unixTimeStamp);
            // Unix timestamp is seconds past epoch
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(seconds).ToLocalTime();
            return dtDateTime;
        }
    }
}