using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using maps.Model;


namespace maps.Web.Models.ViewModels
{ 
	public class AccessiblePlaceView
    {
        public int ID { get; set; }

        public bool Exist { get; set; }

        public int Type { get; set; }

        public IEnumerable<SelectListItem> SelectListType
        {
            get
            {
                yield return new SelectListItem()
                {
                    Value = ((int)AccessiblePlace.TypeEnum.Rampant).ToString(),
                    Text = "Пандус",
                    Selected = Type == (int)AccessiblePlace.TypeEnum.Rampant
                };
            }
        }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public double CenterDistance { get; set; }

        public Dictionary<string, AccessiblePlacePhotoView> Photos { get; set; }

        public AccessiblePlaceView()
        {
            Type = (int)AccessiblePlace.TypeEnum.Rampant;
        }
    }
}