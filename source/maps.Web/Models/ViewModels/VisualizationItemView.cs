using maps.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Web.Models.ViewModels
{
    public class VisualizationItemView 
    {
        public int ID { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public int Accuracy { get; set; }

        public List<VisualizationItem.DataValue> DataItems { get; set; }
    }
}
