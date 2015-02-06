using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Model
{
    public interface IOldRepository
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

        bool ChangeUserCity(User instance);


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
        
        /*bool UpdateBicycleParkingVote(BicycleParkingVote instance);
        
        bool RemoveBicycleParkingVote(int idBicycleParkingVote);*/
        
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

        #region Goal
        
        IQueryable<Goal> Goals { get; }
        
        bool CreateGoal(Goal instance);
        
        bool UpdateGoal(Goal instance);
        
        bool RemoveGoal(int idGoal);
        
        #endregion 

        #region GoalCell
        
        IQueryable<GoalCell> GoalCells { get; }
        
        bool UpdateGoalCell(GoalCell instance);

        void ClearAllCells(int idGoal);
        
        #endregion 

        #region City
        
        IQueryable<City> Cities { get; }
        
        bool CreateCity(City instance);
        
        bool UpdateCity(City instance);
        
        bool RemoveCity(int idCity);
        
        #endregion 

        #region Comment
        
        IQueryable<Comment> Comments { get; }
        
        bool CreateComment(Comment instance);
        
        bool UpdateComment(Comment instance);
        
        bool RemoveComment(int idComment);
        
        #endregion 

        #region UtilityDepartment
        
        IQueryable<UtilityDepartment> UtilityDepartments { get; }
        
        bool CreateUtilityDepartment(UtilityDepartment instance);
        
        bool UpdateUtilityDepartment(UtilityDepartment instance);
        
        bool RemoveUtilityDepartment(int idUtilityDepartment);
        
        #endregion 

        #region UtilityIssue
        
        IQueryable<UtilityIssue> UtilityIssues { get; }
        
        bool CreateUtilityIssue(UtilityIssue instance);

        bool UpdateUtilityIssue(UtilityIssue instance, int userID);

        bool AcceptUtilityIssue(UtilityIssue instance, int userID);

        bool AcceptBackUtilityIssue(UtilityIssue instance, int userID);

        bool ResolveUtilityIssue(UtilityIssue instance, int userID);

        bool CloseUtilityIssue(UtilityIssue instance, int userID);

        bool RemoveUtilityIssue(int idUtilityIssue, int userID);

        bool ClearAllUtilityIssueTags(int idUtilityIssue);

        bool ClearAllUtilityIssuePhotos(int idUtilityIssue);

        #endregion 

        #region UtilityIssueComment
        
        IQueryable<UtilityIssueComment> UtilityIssueComments { get; }
        
        bool CreateUtilityIssueComment(UtilityIssueComment instance);
        
        bool UpdateUtilityIssueComment(UtilityIssueComment instance);
        
        bool RemoveUtilityIssueComment(int idUtilityIssueComment);
        
        #endregion 

        #region UtilityTag
        
        IQueryable<UtilityTag> UtilityTags { get; }
        
        bool CreateUtilityTag(UtilityTag instance);
        
        bool UpdateUtilityTag(UtilityTag instance);
        
        bool RemoveUtilityTag(int idUtilityTag);
        
        #endregion 

        #region UtilityIssueTag
        
        IQueryable<UtilityIssueTag> UtilityIssueTags { get; }
        
        bool CreateUtilityIssueTag(UtilityIssueTag instance);
        
        bool UpdateUtilityIssueTag(UtilityIssueTag instance);
        
        bool RemoveUtilityIssueTag(int idUtilityIssueTag);
        
        #endregion 

        #region UtilityPhoto

        IQueryable<UtilityPhoto> UtilityPhotos { get; }
        
        bool CreateUtilityPhoto(UtilityPhoto instance);
        
        bool UpdateUtilityPhoto(UtilityPhoto instance);
        
        bool RemoveUtilityPhoto(int idUtilityPhoto);
        
        #endregion 

        #region InstagramPhoto
        
        IQueryable<InstagramPhoto> InstagramPhotoes { get; }
        

        bool CreateInstagramPhoto(InstagramPhoto instance);
        
        bool UpdateInstagramPhoto(InstagramPhoto instance);
        
        bool RemoveInstagramPhoto(int idInstagramPhoto);
        
        #endregion 
    }
}
