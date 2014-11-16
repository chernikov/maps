using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace maps.Model
{ 
    public partial class InstagramPhoto
    {
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