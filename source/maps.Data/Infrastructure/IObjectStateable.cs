using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Data.Infrastructure
{
    public interface IObjectStateable
    {
        ObjectState ObjState { get; set; }
    }
}
