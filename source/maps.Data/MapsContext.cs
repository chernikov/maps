namespace maps.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using maps.Data.Entities;
    using maps.Data.Configuration;

    public partial class MapsContext : DbContext
    {
        public MapsContext()
            : base("name=MapsContext")
        {
        }

        public virtual DbSet<BicycleDirectionLine> BicycleDirectionLines { get; set; }
        public virtual DbSet<BicycleLine> BicycleLines { get; set; }
        public virtual DbSet<BicycleParking> BicycleParkings { get; set; }
        public virtual DbSet<BicycleParkingVote> BicycleParkingVotes { get; set; }
        public virtual DbSet<BycicleDirection> BycicleDirections { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Goal> Goals { get; set; }
        public virtual DbSet<GoalCell> GoalCells { get; set; }
        public virtual DbSet<InstagramPhoto> InstagramPhotoes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Social> Socials { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UtilityDepartment> UtilityDepartments { get; set; }
        public virtual DbSet<UtilityIssue> UtilityIssues { get; set; }
        public virtual DbSet<UtilityIssueComment> UtilityIssueComments { get; set; }
        public virtual DbSet<UtilityIssueHistory> UtilityIssueHistories { get; set; }
        public virtual DbSet<UtilityIssueTag> UtilityIssueTags { get; set; }
        public virtual DbSet<UtilityPhoto> UtilityPhotoes { get; set; }
        public virtual DbSet<UtilityTag> UtilityTags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BycicleDirectionConfiguration());

            modelBuilder.Configurations.Add(new CityConfiguration());

            modelBuilder.Configurations.Add(new CommentConfiguration());

            modelBuilder.Configurations.Add(new UserConfiguration());

            modelBuilder.Configurations.Add(new UtilityIssueConfiguration());

            modelBuilder.Configurations.Add(new UtilityIssueHistoryConfiguration());
        }
    }
}
