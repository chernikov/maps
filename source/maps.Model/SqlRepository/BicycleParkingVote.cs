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
                RecalculateBicycleParkingVotes(instance.BicycleParkingID);
                return true;
            }

            return false;
        }

    }
}