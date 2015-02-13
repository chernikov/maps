using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class SqlRepository
    {
        

        public IQueryable<Notify> Notifies
        {
            get
            {
                return Db.Notifies;
            }
        }

        public bool CreateNotify(Notify instance)
        {
            if (instance.ID == 0)
            {
                instance.AddedDate = DateTime.Now;
                Db.Notifies.InsertOnSubmit(instance);
                Db.Notifies.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateNotify(Notify instance)
        {
            Notify cache = Db.Notifies.Where(p => p.ID == instance.ID).FirstOrDefault();
            if (cache != null)
            {
                cache.ReportID = instance.ReportID;
                cache.Phone = instance.Phone;
                cache.Email = instance.Email;
                cache.Sender = instance.Sender;
                cache.Header = instance.Header;
                cache.Text = instance.Text;
                cache.IsSent = instance.IsSent;
                cache.Result = instance.Result;
                Db.Notifies.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveNotify(int idNotify)
        {
            Notify instance = Db.Notifies.Where(p => p.ID == idNotify).FirstOrDefault();
            if (instance != null)
            {
                Db.Notifies.DeleteOnSubmit(instance);
                Db.Notifies.Context.SubmitChanges();
                return true;
            }

            return false;
        }
        
    }
}
