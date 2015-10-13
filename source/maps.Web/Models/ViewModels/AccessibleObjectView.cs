using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using maps.Model;


namespace maps.Web.Models.ViewModels
{ 
	public class AccessibleObjectView
    {
        public int ID { get; set; }

        public int Type { get; set; }

        public IEnumerable<SelectListItem> SelectListType
        {
            get
            {
                yield return new SelectListItem()
                {
                    Value = ((int)AccessibleObject.TypeEnum.ramp_minus).ToString(),
                    Text = "Пандус",
                    Selected = Type == (int)AccessibleObject.TypeEnum.ramp_minus
                };
                yield return new SelectListItem()
                {
                    Value = ((int)AccessibleObject.TypeEnum.medicine_minus).ToString(),
                    Text = "Аптека",
                    Selected = Type == (int)AccessibleObject.TypeEnum.medicine_minus
                };
                yield return new SelectListItem()
                {
                    Value = ((int)AccessibleObject.TypeEnum.parking_minus).ToString(),
                    Text = "Парковка",
                    Selected = Type == (int)AccessibleObject.TypeEnum.parking_minus
                };
                yield return new SelectListItem()
                {
                    Value = ((int)AccessibleObject.TypeEnum.road_minus).ToString(),
                    Text = "Вузька дорога",
                    Selected = Type == (int)AccessibleObject.TypeEnum.road_minus
                };
                yield return new SelectListItem()
                {
                    Value = ((int)AccessibleObject.TypeEnum.sidewalk_minus).ToString(),
                    Text = "З'їзд/заїзд",
                    Selected = Type == (int)AccessibleObject.TypeEnum.sidewalk_minus
                };
                yield return new SelectListItem()
                {
                    Value = ((int)AccessibleObject.TypeEnum.toilet_minus).ToString(),
                    Text = "Туалет",
                    Selected = Type == (int)AccessibleObject.TypeEnum.toilet_minus
                };
            }
        }

        public bool IsAccessible { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public double CenterDistance { get; set; }

        public Dictionary<string, AccessibleObjectPhotoView> Photos { get; set; }

        public AccessibleObjectView()
        {
            Type = (int)AccessibleObject.TypeEnum.ramp_minus;
        }
    }
}