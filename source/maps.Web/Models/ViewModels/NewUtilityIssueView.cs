using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using maps.Model;


namespace maps.Web.Models.ViewModels
{ 
	public class NewUtilityIssueView
    {

		public int UserID {get; set; }

		public double Lat {get; set; }

		public double Lng {get; set; }

		public string Address {get; set; }

		public string Description {get; set; }

        private List<UtilityTag> _utilityTags
        {
            get
            {
                var repository = DependencyResolver.Current.GetService<IRepository>();
                return repository.UtilityTags.ToList();
            }
        }

        public List<int> UtilityTagList { get; set; }

        public IEnumerable<SelectListItem> SelectListUtilityTags
        {
            get
            {
                foreach (var item in _utilityTags)
                {
                    yield return new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.ID.ToString(),
                        Selected = UtilityTagList.Contains(item.ID)
                    };
                }
            }
        }

        public Dictionary<string, UtilityPhotoView> Photos { get; set; }

        public NewUtilityIssueView()
        {
            UtilityTagList = new List<int>();
        }

    }
}