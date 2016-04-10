using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class Visualization
    {

        public int Identifier
        {
            get
            {
                return ID;
            }
            set
            {
                ID = value;
            }
        }

        public IEnumerable<VisualizationItem> SubItems
        {
            get
            {
                return VisualizationItems.ToList();
            }
        }
    }
}
