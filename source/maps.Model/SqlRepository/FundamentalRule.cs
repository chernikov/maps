using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class SqlRepository
    {
        public IQueryable<FundamentalRule> FundamentalRules
        {
            get
            {
                return Db.FundamentalRules;
            }
        }

        public bool CreateFundamentalRule(FundamentalRule instance)
        {
            if (instance.ID == 0)
            {
                Db.FundamentalRules.InsertOnSubmit(instance);
                Db.FundamentalRules.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateFundamentalRule(FundamentalRule instance)
        {
            var cache = Db.FundamentalRules.Where(p => p.ID == instance.ID).FirstOrDefault();
            if (cache != null)
            {
                cache.Name = instance.Name;
                Db.FundamentalRules.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool RemoveFundamentalRule(int idFundamentalRule)
        {
            var instance = Db.FundamentalRules.Where(p => p.ID == idFundamentalRule).FirstOrDefault();
            if (instance != null)
            {
                Db.FundamentalRules.DeleteOnSubmit(instance);
                Db.FundamentalRules.Context.SubmitChanges();
                return true;
            }

            return false;
        }

    }
}
