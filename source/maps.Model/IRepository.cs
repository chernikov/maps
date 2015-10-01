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

        #region Brand

        IQueryable<Brand> Brands { get; }

        bool CreateBrand(Brand instance);

        bool UpdateBrand(Brand instance);

        bool RemoveBrand(int idBrand);

        #endregion 

        #region Bus

        IQueryable<Bus> Bus { get; }

        bool CreateBus(Bus instance);

        bool UpdateBus(Bus instance);

        bool RemoveBus(int idBus);

        #endregion 

        #region BusPhoto

        IQueryable<BusPhoto> BusPhotos { get; }

        bool CreateBusPhoto(BusPhoto instance);

        bool RemoveBusPhoto(int idBusPhoto);

        #endregion 

        #region Notify

        IQueryable<Notify> Notifies { get; }

        bool CreateNotify(Notify instance);

        bool UpdateNotify(Notify instance);

        bool RemoveNotify(int idNotify);

        #endregion 
        
        #region Report

        IQueryable<Report> Reports { get; }

        bool CreateReport(Report instance);

        bool UpdateReport(Report instance);

        bool RemoveReport(int idReport);

        #endregion 
        
        #region ReportAnswer

        IQueryable<ReportAnswer> ReportAnswers { get; }

        bool CreateReportAnswer(ReportAnswer instance);

        bool UpdateReportAnswer(ReportAnswer instance);

        bool RemoveReportAnswer(int idReportAnswer);

        #endregion 
        
        #region ReportComment

        IQueryable<ReportComment> ReportComments { get; }

        bool CreateReportComment(ReportComment instance);

        bool RemoveReportComment(int idReportComment);

        #endregion 
        
        #region ReportPhoto

        IQueryable<ReportPhoto> ReportPhotos { get; }

        bool CreateReportPhoto(ReportPhoto instance);

        bool UpdateReportPhoto(ReportPhoto instance);

        bool RemoveReportPhoto(int idReportPhoto);

        #endregion 
        
        #region Route

        IQueryable<Route> Routes { get; }

        bool CreateRoute(Route instance);

        bool UpdateRoute(Route instance);

        bool RemoveRoute(int idRoute);

        #endregion 

        #region RouteSection

        IQueryable<RouteSection> RouteSections { get; }

        bool CreateRouteSection(RouteSection instance);


        bool RemoveRouteSection(int idRouteSection);

        #endregion 


        #region FundamentalRule

        IQueryable<FundamentalRule> FundamentalRules { get; }

        bool CreateFundamentalRule(FundamentalRule instance);

        bool UpdateFundamentalRule(FundamentalRule instance);

        bool RemoveFundamentalRule(int idFundamentalRule);

        #endregion 
        
        
        #region Rule

        IQueryable<Rule> Rules { get; }

        bool CreateRule(Rule instance);

        bool UpdateRule(Rule instance);

        bool RemoveRule(int idRule);

        #endregion 
        
        #region RuleReport

        IQueryable<RuleReport> RuleReports { get; }

        bool CreateRuleReport(RuleReport instance);

        bool RemoveRuleReport(int idRuleReport);

        #endregion 

        #region Station

        IQueryable<Station> Stations { get; }

        bool CreateStation(Station instance);

        bool UpdateStation(Station instance);

        bool RemoveStation(int idStation);

        #endregion 

        #region Transporteur

        IQueryable<Transporteur> Transporteurs { get; }

        bool CreateTransporteur(Transporteur instance);

        bool UpdateTransporteur(Transporteur instance);

        bool RemoveTransporteur(int idTransporteur);

        #endregion 

        
        #region AccessibleDirection

        IQueryable<AccessibleDirection> AccessibleDirections { get; }

        bool CreateAccessibleDirection(AccessibleDirection instance);

        bool UpdateAccessibleDirection(AccessibleDirection instance);

        bool RemoveAccessibleDirection(int idAccessibleDirection);

        #endregion 

        
        #region AccessibleObject

        IQueryable<AccessibleObject> AccessibleObjects { get; }

        bool CreateAccessibleObject(AccessibleObject instance);

        bool UpdateAccessibleObject(AccessibleObject instance);

        bool VerifiedAccessibleObject(int idAccessibleObject, bool verified);

        bool RemoveAccessibleObject(int idAccessibleObject);

        #endregion 
        
        
        #region AccessibleObjectPhoto

        IQueryable<AccessibleObjectPhoto> AccessibleObjectPhotos { get; }

        bool CreateAccessibleObjectPhoto(AccessibleObjectPhoto instance);

        bool UpdateAccessibleObjectPhoto(AccessibleObjectPhoto instance);

        bool RemoveAccessibleObjectPhoto(int idAccessibleObjectPhoto);

        #endregion 
        
        #region AccessiblePlace
        
        IQueryable<AccessiblePlace> AccessiblePlaces { get; }

        bool CreateAccessiblePlace(AccessiblePlace instance);

        bool UpdateAccessiblePlace(AccessiblePlace instance);

        bool VerifiedAccessiblePlace(int idAccessiblePlace, bool verified);

        bool RemoveAccessiblePlace(int idAccessiblePlace);

        #endregion 
        
        
        #region AccessiblePlacePhoto

        IQueryable<AccessiblePlacePhoto> AccessiblePlacePhotos { get; }

        bool CreateAccessiblePlacePhoto(AccessiblePlacePhoto instance);

        bool UpdateAccessiblePlacePhoto(AccessiblePlacePhoto instance);

        bool RemoveAccessiblePlacePhoto(int idAccessiblePlacePhoto);

        #endregion 
    }

}
