using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace maps.Model
{ 
    public partial class BicycleParking
    {
        public enum TypeEnum : int
        {
            Wheel = 0x01,
            Frame = 0x02
        }

        public string TypeStr
        {
            get
            {
                switch ((TypeEnum)Type)
                {
                    case TypeEnum.Wheel :
                        return "колесо";
                    case TypeEnum.Frame:
                        return "рама";
                }
                return string.Empty;
            }

        }
	}
}