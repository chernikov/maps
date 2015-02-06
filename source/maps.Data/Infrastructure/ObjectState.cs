using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Data.Infrastructure
{
    public enum ObjectState
    {
        /// <summary>
        /// The unchanged.
        /// </summary>
        Unchanged,

        /// <summary>
        /// The added.
        /// </summary>
        Added,

        /// <summary>
        /// The modified.
        /// </summary>
        Modified,

        /// <summary>
        /// The deleted.
        /// </summary>
        Deleted
    }
}
