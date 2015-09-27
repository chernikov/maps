using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Dozor.Entity
{
    [JsonObject]
    public class RecordResponce<T>
    {
        [JsonProperty("hash")]
        public int Hash { get; set; }

        [JsonProperty("values")]
        public List<T> Values { get; set; }
    }
}
