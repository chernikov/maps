using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class Building
    {

        public IEnumerable<BuildingElectricity> SubBuildingElectricities
        {
            get
            {
                return BuildingElectricities.OrderBy(p => p.Year).ToList();
            }
        }
        
    }
}
