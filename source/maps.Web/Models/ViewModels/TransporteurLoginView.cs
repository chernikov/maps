using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Models.ViewModels
{
    public class TransporteurLoginView
    {
        public int ID { get; set; }

        public int _ID { get; set; }

        public string Mobile { get; set; }

        public List<string> Mobiles { get; set; }

        public IEnumerable<SelectListItem> SelectListMobiles
        {
            get
            {
                if (Mobiles != null && Mobiles.Count > 0) 
                {
                    foreach (var mobile in Mobiles)
                    {
                        yield return new SelectListItem()
                        {
                            Text = mobile,
                            Value = mobile,
                            Selected = Mobile == mobile
                        };
                    };
                };
            }
        }
        public string Password { get; set; }

        public bool UserIsExist { get; set; }
    }
}