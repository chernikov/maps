using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public interface IRepository
    {
        IQueryable<T> GetTable<T>() where T : class;

        #region Role

        IQueryable<Role> Roles { get; }

        bool CreateRole(Role instance);

        bool RemoveRole(int idRole);

        #endregion

        #region User

        IQueryable<User> Users { get; }

        bool CreateUser(User instance);

        bool UpdateUser(User instance);

        User GetUser(string email);

        User Login(string email, string password);

        bool ActivateUser(User instance);

        bool ChangePassword(User instance);

        #endregion

        #region UserRole

        IQueryable<UserRole> UserRoles { get; }

        bool CreateUserRole(UserRole instance);

        bool RemoveUserRole(int idUserRole);

        #endregion

        #region BycicleDirection
        
        IQueryable<BycicleDirection> BycicleDirections { get; }
        
        bool CreateBycicleDirection(BycicleDirection instance);
        
        bool UpdateBycicleDirection(BycicleDirection instance);
        
        bool RemoveBycicleDirection(int idBycicleDirection);
        
        #endregion 
    }
}
