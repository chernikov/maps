using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Dozor.Entity
{
    [JsonObject]
    public class BusRecord
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "managerId")]
        public int ManagerId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "imei")]
        public string IMEI { get; set; }

        [JsonProperty(PropertyName = "sim")]
        public string Sim { get; set; }

        [JsonProperty(PropertyName = "deviceProtocol")]
        public DeviceProtocolRecord DeviceProtocol { get; set; }

        [JsonProperty(PropertyName = "firmware")]
        public string Firmware { get; set; }

        [JsonProperty(PropertyName = "enable")]
        public bool Enable { get; set; }

        [JsonProperty(PropertyName = "connect")]
        public bool Connect { get; set; }

        [JsonProperty(PropertyName = "setting")]
        public List<string> Setting { get; set; }

        [JsonProperty(PropertyName = "regionalFlag")]
        public bool RegionalFlag { get; set; }

        [JsonProperty(PropertyName = "regionalType")]
        public int RegionalType { get; set; }

        [JsonProperty(PropertyName = "addDate")]
        public int AddDate { get; set; }

        [JsonProperty(PropertyName = "firstConnectDate")]
        public int FirstConnectDate { get; set; }

        [JsonProperty(PropertyName = "sizeStoreMonths")]
        public int SizeStoreMonths { get; set; }

    }
}
