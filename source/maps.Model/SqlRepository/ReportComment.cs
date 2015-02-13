using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class SqlRepository
    {


        public IQueryable<ReportComment> ReportComments
        {
            get
            {
                return Db.ReportComments;
            }
        }

        public bool CreateReportComment(ReportComment instance)
        {
            if (instance.ID == 0)
            {
                Db.ReportComments.InsertOnSubmit(instance);
                Db.ReportComments.Context.SubmitChanges();
                return true;
            }

            return false;
        }

  

        public bool RemoveReportComment(int idReportComment)
        {
            ReportComment instance = Db.ReportComments.Where(p => p.ID == idReportComment).FirstOrDefault();
            if (instance != null)
            {
                Db.ReportComments.DeleteOnSubmit(instance);
                Db.ReportComments.Context.SubmitChanges();
                return true;
            }

            return false;
        }
        
    }
}
