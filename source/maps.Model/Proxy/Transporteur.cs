using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class Transporteur
    {
        public string FullName
        {
            get
            {
                return string.Format("{0} {1} {2}", LastName, FirstName, Patronymic);
            }
        }
    }
}
