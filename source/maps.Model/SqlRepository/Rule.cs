using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class SqlRepository
    {
        public IQueryable<Rule> Rules
        {
            get
            {
                return Db.Rules;
            }
        }

        public bool CreateRule(Rule instance)
        {
            if (instance.ID == 0)
            {
                instance.Description = instance.Description ?? string.Empty;
                Db.Rules.InsertOnSubmit(instance);
                Db.Rules.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool UpdateRule(Rule instance)
        {
            var cache = Db.Rules.Where(p => p.ID == instance.ID).FirstOrDefault();
            if (cache != null)
            {
                cache.FundamentalRuleID = instance.FundamentalRuleID;
                cache.ТуреOfRule = instance.ТуреOfRule;
                cache.Name = instance.Name;
                cache.Description = instance.Description ?? string.Empty;
                cache.IsRouteScope = instance.IsRouteScope;
                cache.UrlToLaw = instance.UrlToLaw;
                Db.Rules.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool RemoveRule(int idRule)
        {
            var instance = Db.Rules.Where(p => p.ID == idRule).FirstOrDefault();
            if (instance != null)
            {
                Db.Rules.DeleteOnSubmit(instance);
                Db.Rules.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}
