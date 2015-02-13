using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class SqlRepository
    {
        

        public IQueryable<RuleReport> RuleReports
        {
            get
            {
                return Db.RuleReports;
            }
        }

        public bool CreateRuleReport(RuleReport instance)
        {
            if (instance.ID == 0)
            {
                Db.RuleReports.InsertOnSubmit(instance);
                Db.RuleReports.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveRuleReport(int idRuleReport)
        {
            RuleReport instance = Db.RuleReports.Where(p => p.ID == idRuleReport).FirstOrDefault();
            if (instance != null)
            {
                Db.RuleReports.DeleteOnSubmit(instance);
                Db.RuleReports.Context.SubmitChanges();
                return true;
            }

            return false;
        }
        
    }
}
