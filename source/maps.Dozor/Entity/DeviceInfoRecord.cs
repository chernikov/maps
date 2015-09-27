using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Dozor.Entity
{
    [JsonObject]
    public class DeviceInfoRecord
    {
        [JsonProperty(PropertyName = "autoBrand")]
        public string AutoBrand { get; set; }

        [JsonProperty(PropertyName = "autoYear")]
        public string AutoYear { get; set; }

        [JsonProperty(PropertyName = "autoGovNumber")]
        public string AutoGovNumber { get; set; }

        [JsonProperty(PropertyName = "autoBodyGovNumber")]
        public string AutoBodyGovNumber { get; set; }

        [JsonProperty(PropertyName = "autoColorName")]
        public string AutoColorName { get; set; }

        [JsonProperty(PropertyName = "autoColorRGB")]
        public string AutoColorRGB { get; set; }

        [JsonProperty(PropertyName = "clientFIO")]
        public string ClientFIO { get; set; }

        [JsonProperty(PropertyName = "clientPhone1")]
        public string ClientPhone1 { get; set; }

        [JsonProperty(PropertyName = "clientPhone2")]
        public string ClientPhone2 { get; set; }

        [JsonProperty(PropertyName = "clientPhone3")]
        public string ClientPhone3 { get; set; }

        [JsonProperty(PropertyName = "clientPhone4")]
        public string ClientPhone4 { get; set; }

        [JsonProperty(PropertyName = "clientPhone5")]
        public string ClientPhone5 { get; set; }

        [JsonProperty(PropertyName = "clientPIN")]
        public string ClientPIN { get; set; }

        [JsonProperty(PropertyName = "clientQuestion")]
        public string ClientQuestion { get; set; }

        [JsonProperty(PropertyName = "clientAnswer")]
        public string ClientAnswer { get; set; }
    }
}
