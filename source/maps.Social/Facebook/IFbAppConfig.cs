using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Social.Facebook
{
    public interface IFbAppConfig
    {
        string AppId { get; }

        string AppSecret { get; }
    }
}
