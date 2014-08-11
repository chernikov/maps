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
                instance.AddedDate = DateTime.Now;
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
                if (!cache.Exist && instance.Exist)
                {
                    cache.CreatedDate = DateTime.Now;
                } 
                else if (cache.Exist && !instance.Exist)
                {
                    cache.CreatedDate = null;
                }
                cache.Position = instance.Position;
                cache.PhotoUrl = instance.PhotoUrl;
                cache.Exist = instance.Exist;
                cache.Type = instance.Type;
                cache.Camera = instance.Camera;
                cache.Lock = instance.Lock;
                cache.Rent = instance.Rent;
                cache.Capacity = instance.Capacity;
                cache.Quality = instance.Quality;
                cache.Description = instance.Description;
                cache.Address = instance.Address;
                cache.CenterDistance = instance.CenterDistance;
                Db.BicycleParkings.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool VerifiedBicycleParking(int idBicycleParking, bool verified)
        {
            var cache = Db.BicycleParkings.FirstOrDefault(p => p.ID == idBicycleParking);
            if (cache != null)
            {
                if (verified)
                {
                    cache.VerifiedDate = DateTime.Now;
                }
                else
                {
                    cache.VerifiedDate = null;
                }
                Db.BicycleParkings.Context.SubmitChanges();
                return true;
            }
            return false;
        }



        public bool UpdateBicycleParkingVote(BicycleParking instance)
        {
            var cache = Db.BicycleParkings.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
                cache.VotesCount = instance.VotesCount;
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

        private void RecalculateBicycleParkingVotes(int idBicycleParking)
        {
            var instance = Db.BicycleParkings.FirstOrDefault(p => p.ID == idBicycleParking);
            if (instance != null)
            {
                var total = instance.BicycleParkingVotes.Count();
                instance.VotesCount = total;
                Db.BicycleParkings.Context.SubmitChanges();
            }
        }
    }
}