using maps.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Models.ViewModels
{
    public class RuleView
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int? FundamentalRuleID { get; set; }

        private List<FundamentalRule> _fundamentalRules
        {
            get
            {
                var repository = DependencyResolver.Current.GetService<IRepository>();
                return repository.FundamentalRules.ToList();
            }
        }

        public IEnumerable<SelectListItem> SelectListFundamentalRule
        {
            get
            {
                yield return new SelectListItem()
                {
                    Text = "",
                    Value = "",
                    Selected = !FundamentalRuleID.HasValue
                };

                foreach (var item in _fundamentalRules)
                {
                    yield return new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.ID.ToString(),
                        Selected = FundamentalRuleID == item.ID
                    };
                }
            }
        }

        public int ТуреOfRule { get; set; }

        public IEnumerable<SelectListItem> SelectListTypes
        {
            get
            {
                yield return new SelectListItem()
                {
                    Text = "Порушення ПДР",
                    Value = ((int)Rule.TypeEnum.RulesOfTheRoad).ToString(),
                    Selected = ТуреOfRule == (int)Rule.TypeEnum.RulesOfTheRoad
                };

                yield return new SelectListItem()
                {
                    Text = "Порушення умов перевезення",
                    Value = ((int)Rule.TypeEnum.BreachOfContract).ToString(),
                    Selected = ТуреOfRule == (int)Rule.TypeEnum.BreachOfContract
                };
            }
        }

        public bool IsRouteScope { get; set; }

        public string Description { get; set; }

        public string UrlToLaw { get; set; }
    }
}