using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class VisualizationItem
    {
        public class DataValue
        {
            public string Key { get; set; }

            public string Value { get; set; }
        }
        public List<DataValue> DataItems
        {
            get
            {
                return JsonConvert.DeserializeObject<List<DataValue>>(Data);
            }
        }

       
    }
}
