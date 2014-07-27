using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace maps.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<BicycleParkingVote> BicycleParkingVotes
        {
            get
            {
                return Db.BicycleParkingVotes;
            }
        }

        public bool CreateBicycleParkingVote(BicycleParkingVote instance)
        {
            if (instance.ID == 0)
            {
                Db.BicycleParkingVotes.InsertOnSubmit(instance);
                Db.BicycleParkingVotes.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateBicycleParkingVote(BicycleParkingVote instance)
        {
            var cache = Db.BicycleParkingVotes.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.UserID = instance.UserID;
				cache.BicycleParkingID = instance.BicycleParkingID;
				cache.Mark = instance.Mark;
                Db.BicycleParkingVotes.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveBicycleParkingVote(int idBicycleParkingVote)
        {
            BicycleParkingVote instance = Db.BicycleParkingVotes.FirstOrDefault(p => p.ID == idBicycleParkingVote);
            if (instance != null)
            {
                Db.BicycleParkingVotes.DeleteOnSubmit(instance);
                Db.BicycleParkingVotes.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}