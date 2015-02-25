using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class SqlRepository
    {
        

        public IQueryable<ReportAnswer> ReportAnswers
        {
            get
            {
                return Db.ReportAnswers;
            }
        }

        public bool CreateReportAnswer(ReportAnswer instance)
        {
            if (instance.ID == 0)
            {
                instance.AddedDate = DateTime.Now;
                Db.ReportAnswers.InsertOnSubmit(instance);
                Db.ReportAnswers.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateReportAnswer(ReportAnswer instance)
        {
            ReportAnswer cache = Db.ReportAnswers.Where(p => p.ID == instance.ID).FirstOrDefault();
            if (cache != null)
            {
                cache.Answer = instance.Answer;
                Db.ReportAnswers.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveReportAnswer(int idReportAnswer)
        {
            ReportAnswer instance = Db.ReportAnswers.Where(p => p.ID == idReportAnswer).FirstOrDefault();
            if (instance != null)
            {
                Db.ReportAnswers.DeleteOnSubmit(instance);
                Db.ReportAnswers.Context.SubmitChanges();
                return true;
            }

            return false;
        }
          

      
        
    }
}
