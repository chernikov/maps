using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class AccessiblePlace
    {
        public AccessiblePlacePhoto FirstImage
        {
            get
            {
                return AccessiblePlacePhotos.FirstOrDefault();
            }
        }


        public IEnumerable<AccessiblePlacePhoto> SubPhotos
        {
            get
            {
                return AccessiblePlacePhotos.AsEnumerable();
            }
        }
    }
}
