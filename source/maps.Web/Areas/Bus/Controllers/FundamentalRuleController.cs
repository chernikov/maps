using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using maps.Web.Models.ViewModels;
using maps.Model;


namespace maps.Web.Areas.Bus.Controllers
{
    public class FundamentalRuleController : BaseBusController
    {
        public ActionResult Index()
        {
            var list = Repository.FundamentalRules.ToList();
            return View(list);
        }

        public ActionResult Create()
        {
            return View("Edit", new FundamentalRuleView());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var fundamentalRule = Repository.FundamentalRules.FirstOrDefault(p => p.ID == id);

            if (fundamentalRule != null)
            {
                var fundamentalRuleView = (FundamentalRuleView)ModelMapper.Map(fundamentalRule, typeof(FundamentalRule), typeof(FundamentalRuleView));
                return View(fundamentalRuleView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        public ActionResult Edit(FundamentalRuleView fundamentalRuleView)
        {
            if (ModelState.IsValid)
            {
                var fundamentalRule = (FundamentalRule)ModelMapper.Map(fundamentalRuleView, typeof(FundamentalRuleView), typeof(FundamentalRule));
                if (fundamentalRule.ID == 0)
                {
                    Repository.CreateFundamentalRule(fundamentalRule);
                }
                else
                {
                    Repository.UpdateFundamentalRule(fundamentalRule);
                }
                return RedirectToAction("Index");
            }
            return View(fundamentalRuleView);
        }

        public ActionResult Delete(int id)
        {
            var fundamentalRule = Repository.FundamentalRules.FirstOrDefault(p => p.ID == id);
            if (fundamentalRule != null)
            {
                Repository.RemoveFundamentalRule(fundamentalRule.ID);
            }
            return RedirectToAction("Index");
        }
    }
}