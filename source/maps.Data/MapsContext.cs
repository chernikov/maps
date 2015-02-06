namespace maps.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using maps.Data.Entities;
    using maps.Data.Configuration;

    public partial class MapsContext : DbContext,  IDbContext, IMapsContext
    {
        public MapsContext()
            : base("name=MapsContext")
        {
        }

        public IDbSet<BicycleDirectionLine> BicycleDirectionLines { get; set; }

        public IDbSet<BicycleLine> BicycleLines { get; set; }

        public IDbSet<BicycleParking> BicycleParkings { get; set; }

        public IDbSet<BicycleParkingVote> BicycleParkingVotes { get; set; }

        public IDbSet<BicycleDirection> BycicleDirections { get; set; }

        public IDbSet<City> Cities { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<InstagramPhoto> InstagramPhotoes { get; set; }

        public IDbSet<Role> Roles { get; set; }

        public IDbSet<Social> Socials { get; set; }

        public IDbSet<User> Users { get; set; }

        public IDbSet<UserRole> UserRoles { get; set; }

        public IDbSet<UtilityDepartment> UtilityDepartments { get; set; }

        public IDbSet<UtilityIssue> UtilityIssues { get; set; }

        public IDbSet<UtilityIssueComment> UtilityIssueComments { get; set; }

        public IDbSet<UtilityIssueHistory> UtilityIssueHistories { get; set; }

        public IDbSet<UtilityIssueTag> UtilityIssueTags { get; set; }

        public IDbSet<UtilityPhoto> UtilityPhotoes { get; set; }

        public IDbSet<UtilityTag> UtilityTags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BycicleDirectionConfiguration());

            modelBuilder.Configurations.Add(new CityConfiguration());

            modelBuilder.Configurations.Add(new CommentConfiguration());

            modelBuilder.Configurations.Add(new UserConfiguration());

            modelBuilder.Configurations.Add(new UtilityIssueConfiguration());

            modelBuilder.Configurations.Add(new UtilityIssueHistoryConfiguration());
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
    }
}
