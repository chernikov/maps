using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using maps.Web.Models.ViewModels;
using maps.Model;


namespace maps.Web.Areas.Bus.Controllers
{
    public class RuleController : BaseBusController
    {
        public ActionResult Index()
        {
            var list = Repository.Rules.ToList();
            return View(list);
        }

        public ActionResult Create()
        {
            return View("Edit", new RuleView());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var rule = Repository.Rules.FirstOrDefault(p => p.ID == id);

            if (rule != null)
            {
                var ruleView = (RuleView)ModelMapper.Map(rule, typeof(Rule), typeof(RuleView));
                return View(ruleView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        public ActionResult Edit(RuleView ruleView)
        {
            if (ModelState.IsValid)
            {
                var rule = (Rule)ModelMapper.Map(ruleView, typeof(RuleView), typeof(Rule));
                if (rule.ID == 0)
                {
                    Repository.CreateRule(rule);
                }
                else
                {
                    Repository.UpdateRule(rule);
                }
                return RedirectToAction("Index");
            }
            return View(ruleView);
        }

        public ActionResult Delete(int id)
        {
            var rule = Repository.Rules.FirstOrDefault(p => p.ID == id);
            if (rule != null)
            {
                Repository.RemoveRule(rule.ID);
            }
            return RedirectToAction("Index");
        }
    }
}