using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool;

namespace maps.Model
{
    public partial class SqlRepository
    {
        public IQueryable<User> Users
        {
            get
            {
                return Db.Users;
            }
        }

        public bool CreateUser(User instance)
        {
            if (instance.ID == 0)
            {
                instance.AddedDate = DateTime.Now;
                instance.LastVisitDate = DateTime.Now;
                Db.Users.InsertOnSubmit(instance);
                Db.Users.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public User GetUser(string login)
        {
            return Db.Users.FirstOrDefault(p => string.Compare(p.Login, login, true) == 0);
        }

        public User Login(string login, string password)
        {
            return Db.Users.FirstOrDefault(p => string.Compare(p.Login, login, true) == 0 && p.Password == password);
        }

        public bool UpdateUser(User instance)
        {
            var cache = Db.Users.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
                cache.Email = instance.Email;
                cache.AvatarPath = instance.AvatarPath;
                cache.FirstName = instance.FirstName;
                cache.LastName = instance.LastName;
                Db.Users.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}
