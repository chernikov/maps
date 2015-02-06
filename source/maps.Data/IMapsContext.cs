using maps.Data.Entities;
using System;
using System.Data.Entity;
namespace maps.Data
{
    public interface IMapsContext : IDbContext
    {
        IDbSet<BicycleDirectionLine> BicycleDirectionLines { get; set; }

        IDbSet<BicycleLine> BicycleLines { get; set; }

        IDbSet<BicycleParking> BicycleParkings { get; set; }

        IDbSet<BicycleParkingVote> BicycleParkingVotes { get; set; }

        IDbSet<BicycleDirection> BycicleDirections { get; set; }

        IDbSet<City> Cities { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<InstagramPhoto> InstagramPhotoes { get; set; }

        IDbSet<Role> Roles { get; set; }

        IDbSet<Social> Socials { get; set; }

        IDbSet<UserRole> UserRoles { get; set; }

        IDbSet<User> Users { get; set; }

        IDbSet<UtilityDepartment> UtilityDepartments { get; set; }

        IDbSet<UtilityIssueComment> UtilityIssueComments { get; set; }

        IDbSet<UtilityIssueHistory> UtilityIssueHistories { get; set; }

        IDbSet<UtilityIssue> UtilityIssues { get; set; }

        IDbSet<UtilityIssueTag> UtilityIssueTags { get; set; }

        IDbSet<UtilityPhoto> UtilityPhotoes { get; set; }

        IDbSet<UtilityTag> UtilityTags { get; set; }

    }
}
