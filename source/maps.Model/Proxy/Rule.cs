using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class Rule
    {

        public enum TypeEnum
        {
            RulesOfTheRoad = 0x01,
            BreachOfContract = 0x02
        }

        public string ТуреStr
        {
            get
            {
                switch (this.ТуреOfRule)
                {
                    case (int)TypeEnum.RulesOfTheRoad: return "ПДР";
                    case (int)TypeEnum.BreachOfContract: return "Умови договору";
                }
                return string.Empty;
            }
        }
    }
}
