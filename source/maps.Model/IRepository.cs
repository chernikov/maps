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

        User GetUser(string login);

        User Login(string login, string password);


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

        bool ProcessBycicleDirection(int idBycicleDirection);
        
        #endregion 

        #region Social
        
        IQueryable<Social> Socials { get; }
        
        bool CreateSocial(Social instance);
        
        bool UpdateSocial(Social instance);
        
        bool RemoveSocial(int idSocial);
        
        #endregion 

        #region BicycleParking
        
        IQueryable<BicycleParking> BicycleParkings { get; }
        
        bool CreateBicycleParking(BicycleParking instance);
        
        bool UpdateBicycleParking(BicycleParking instance);
        
        bool RemoveBicycleParking(int idBicycleParking);

        bool VerifiedBicycleParking(int idBicycleParking, bool verified);
        
        #endregion 

        #region BicycleParkingVote
        
        IQueryable<BicycleParkingVote> BicycleParkingVotes { get; }
        
        bool CreateBicycleParkingVote(BicycleParkingVote instance);
        
        bool UpdateBicycleParkingVote(BicycleParkingVote instance);
        
        bool RemoveBicycleParkingVote(int idBicycleParkingVote);
        
        #endregion 

        #region BicycleLine
        
        IQueryable<BicycleLine> BicycleLines { get; }
        
        bool CreateBicycleLine(BicycleLine instance);

        bool UpdateBicycleLineQuantity(int idBicycleLine);

        bool ClearAllBicycleLines();
        
        #endregion 

        #region BicycleDirectionLine
        
        IQueryable<BicycleDirectionLine> BicycleDirectionLines { get; }
        
        bool CreateBicycleDirectionLine(BicycleDirectionLine instance);
        
        #endregion 
    }
}
