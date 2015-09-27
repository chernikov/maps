using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Dozor.Entity
{
    [JsonObject]
    public class PlaceRecord
    {
        [JsonProperty(PropertyName = "deviceId")]
        public int DeviceId { get; set; }

        [JsonProperty(PropertyName = "trackerTime")]
        public int TrackerTime { get; set; }

        [JsonProperty(PropertyName = "serverTime")]
        public int ServerTime { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public int Longitude { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public int Latitude { get; set; }

        [JsonProperty(PropertyName = "satellites")]
        public int Satellites { get; set; }

        [JsonProperty(PropertyName = "altitude")]
        public int Altitude { get; set; }

        [JsonProperty(PropertyName = "speed")]
        public int Speed { get; set; }

        [JsonProperty(PropertyName = "s")]
        public double S { get; set; }

        [JsonProperty(PropertyName = "place")]
        public string Place { get; set; }

        [JsonProperty(PropertyName = "systemMode")]
        public int SystemMode { get; set; }

        [JsonProperty(PropertyName = "systemState")]
        public int SystemState { get; set; }

        [JsonProperty(PropertyName = "systemControl")]
        public int SystemControl { get; set; }

        [JsonProperty(PropertyName = "systemVoltage")]
        public int SystemVoltage { get; set; }

        [JsonProperty(PropertyName = "netLAC")]
        public int NetLAC { get; set; }

        [JsonProperty(PropertyName = "netCellID")]
        public int NetCellID { get; set; }

        [JsonProperty(PropertyName = "netRSSI")]
        public int NetRSSI { get; set; }

        [JsonProperty(PropertyName = "netStatus")]
        public int NetStatus { get; set; }



    }
}
