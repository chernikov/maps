using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class VisualizationColumn
    {
        public enum VisualizationType : int
        {
            Text = 0x00,
            List = 0x01,
            Date = 0x02,
            Time = 0x03
        }
    }
}
