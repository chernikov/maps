using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class SqlRepository
    {
        

        public IQueryable<ReportPhoto> ReportPhotos
        {
            get
            {
                return Db.ReportPhotos;
            }
        }

        public bool CreateReportPhoto(ReportPhoto instance)
        {
            if (instance.ID == 0)
            {
                Db.ReportPhotos.InsertOnSubmit(instance);
                Db.ReportPhotos.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateReportPhoto(ReportPhoto instance)
        {
            var cache = Db.ReportPhotos.Where(p => p.ID == instance.ID).FirstOrDefault();
            if (cache != null)
            {
                cache.ReportID = instance.ReportID;
                Db.ReportPhotos.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool RemoveReportPhoto(int idReportPhoto)
        {
            ReportPhoto instance = Db.ReportPhotos.Where(p => p.ID == idReportPhoto).FirstOrDefault();
            if (instance != null)
            {
                Db.ReportPhotos.DeleteOnSubmit(instance);
                Db.ReportPhotos.Context.SubmitChanges();
                return true;
            }

            return false;
        }
        
    }
}
