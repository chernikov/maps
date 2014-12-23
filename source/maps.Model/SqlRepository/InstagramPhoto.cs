using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace maps.Model
{

    public partial class SqlRepository
    {
        public IQueryable<InstagramPhoto> InstagramPhotoes
        {
            get
            {
                return Db.InstagramPhotos;
            }
        }

        public bool CreateInstagramPhoto(InstagramPhoto instance)
        {
            if (instance.ID == 0)
            {
                var exist = Db.InstagramPhotos.Any(p => p.GlobalId == instance.GlobalId);
                if (!exist)
                {
                    instance.AddedDate = DateTime.Now;
                    Db.InstagramPhotos.InsertOnSubmit(instance);
                    Db.InstagramPhotos.Context.SubmitChanges();
                    return true;
                }
            }

            return false;
        }

        public bool UpdateInstagramPhoto(InstagramPhoto instance)
        {
            var cache = Db.InstagramPhotos.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
                cache.GlobalId = instance.GlobalId;
                cache.Lat = instance.Lat;
                cache.Lng = instance.Lng;
                cache.CreatedTime = instance.CreatedTime;
                cache.PhotoUrl = instance.PhotoUrl;
                cache.Caption = instance.Caption;
                cache.Tags = instance.Tags;
                cache.UserGlobalId = instance.UserGlobalId;
                cache.UserName = instance.UserName;
                cache.UserFullName = instance.UserFullName;
                cache.AddedDate = instance.AddedDate;
                cache.Link = instance.Link;
                Db.InstagramPhotos.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveInstagramPhoto(int idInstagramPhoto)
        {
            InstagramPhoto instance = Db.InstagramPhotos.FirstOrDefault(p => p.ID == idInstagramPhoto);
            if (instance != null)
            {
                Db.InstagramPhotos.DeleteOnSubmit(instance);
                Db.InstagramPhotos.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}