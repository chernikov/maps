using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Connector
{
    /// <summary>
    /// Get или post запрос
    /// </summary>
    public enum RequestMethodType : int
    {
        Get = 0x01,
        Post = 0x02
    }
}
