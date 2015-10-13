using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class AccessibleObject
    {
        public enum TypeEnum
        {
            ramp_plus = 0x01,
            ramp_minus = 0x02,
            parking_plus = 0x03,
            parking_minus = 0x04,
            medicine_plus = 0x05,
            medicine_minus = 0x06, 
            road_plus = 0x07,
            road_minus = 0x08,
            toilet_plus = 0x09,
            toilet_minus = 0x0A,
            sidewalk_plus = 0x0B,
            sidewalk_minus = 0x0C,
        }
        public AccessibleObjectPhoto FirstImage
        {
            get
            {
                return AccessibleObjectPhotos.FirstOrDefault();
            }
        }


        public IEnumerable<AccessibleObjectPhoto> SubPhotos
        {
            get
            {
                return AccessibleObjectPhotos.AsEnumerable();
            }
        }

        public string TypeStr
        {
            get
            {
                return ((TypeEnum)Type).ToString();
            }
        }
    }
}
