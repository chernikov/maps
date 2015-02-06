using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Data.Infrastructure
{
    public abstract class DatabaseEntity : IObjectStateable
    {
        [NotMapped]
        public ObjectState ObjState { get; set; }
    }
}
