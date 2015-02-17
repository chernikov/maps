using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class SqlRepository
    {
        

        public IQueryable<Report> Reports
        {
            get
            {
                return Db.Reports;
            }
        }

        public bool CreateReport(Report instance)
        {
            if (instance.ID == 0)
            {
                Db.Reports.InsertOnSubmit(instance);
                Db.Reports.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateReport(Report instance)
        {
            Report cache = Db.Reports.Where(p => p.ID == instance.ID).FirstOrDefault();
            if (cache != null)
            {
                cache.UserID = instance.UserID;
                cache.RouteID = instance.RouteID;
                cache.Type = instance.Type;
                cache.Status = instance.Status;
                cache.BusID = instance.BusID;
                cache.Status = instance.Status;
                cache.DeadlineDate = instance.DeadlineDate;
                cache.StationID = instance.StationID;
                cache.Description = instance.Description;
                cache.NotifyTransporteurID = instance.NotifyTransporteurID;
                cache.NotifyReporterID = instance.NotifyReporterID;
                cache.Link = instance.Link;
                cache.FacebookLink = instance.FacebookLink;
                Db.Reports.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveReport(int idReport)
        {
            Report instance = Db.Reports.Where(p => p.ID == idReport).FirstOrDefault();
            if (instance != null)
            {
                Db.Reports.DeleteOnSubmit(instance);
                Db.Reports.Context.SubmitChanges();
                return true;
            }

            return false;
        }
        
    }
}
