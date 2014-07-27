using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace maps.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<BicycleParking> BicycleParkings
        {
            get
            {
                return Db.BicycleParkings;
            }
        }

        public bool CreateBicycleParking(BicycleParking instance)
        {
            if (instance.ID == 0)
            {
                Db.BicycleParkings.InsertOnSubmit(instance);
                Db.BicycleParkings.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateBicycleParking(BicycleParking instance)
        {
            var cache = Db.BicycleParkings.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.UserID = instance.UserID;
				cache.Position = instance.Position;
				cache.PhotoUrl = instance.PhotoUrl;
				cache.Type = instance.Type;
				cache.Quality = instance.Quality;
				cache.VotesCount = instance.VotesCount;
				cache.Description = instance.Description;
                Db.BicycleParkings.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveBicycleParking(int idBicycleParking)
        {
            BicycleParking instance = Db.BicycleParkings.FirstOrDefault(p => p.ID == idBicycleParking);
            if (instance != null)
            {
                Db.BicycleParkings.DeleteOnSubmit(instance);
                Db.BicycleParkings.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}