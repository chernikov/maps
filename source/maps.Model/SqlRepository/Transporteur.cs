using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public partial class SqlRepository
    {
        

        public IQueryable<Transporteur> Transporteurs
        {
            get
            {
                return Db.Transporteurs;
            }
        }

        public bool CreateTransporteur(Transporteur instance)
        {
            if (instance.ID == 0)
            {
                Db.Transporteurs.InsertOnSubmit(instance);
                Db.Transporteurs.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateTransporteur(Transporteur instance)
        {
            Transporteur cache = Db.Transporteurs.Where(p => p.ID == instance.ID).FirstOrDefault();
            if (cache != null)
            {
                cache.FirstName = instance.FirstName;
                cache.LastName = instance.LastName;
                cache.Patronymic = instance.Patronymic;
                cache.Email = instance.Email;
                cache.PrimaryPhone = instance.PrimaryPhone;
                cache.AdditionalPhone = instance.AdditionalPhone;
                cache.UserID = instance.UserID;
                Db.Transporteurs.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveTransporteur(int idTransporteur)
        {
            Transporteur instance = Db.Transporteurs.Where(p => p.ID == idTransporteur).FirstOrDefault();
            if (instance != null)
            {
                Db.Transporteurs.DeleteOnSubmit(instance);
                Db.Transporteurs.Context.SubmitChanges();
                return true;
            }

            return false;
        }
        
    }
}
