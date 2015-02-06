namespace maps.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public User()
        {
            BicycleParkings = new HashSet<BicycleParking>();
            BicycleParkingVotes = new HashSet<BicycleParkingVote>();
            BycicleDirections = new HashSet<BycicleDirection>();
            Comments = new HashSet<Comment>();
            Goals = new HashSet<Goal>();
            Socials = new HashSet<Social>();
            UserRoles = new HashSet<UserRole>();
            UtilityIssues = new HashSet<UtilityIssue>();
            UtilityIssueHistories = new HashSet<UtilityIssueHistory>();
            UtilityPhotoes = new HashSet<UtilityPhoto>();
        }

        public int ID { get; set; }

        public int CityID { get; set; }

        [Required]
        [StringLength(150)]
        public string Login { get; set; }

        [Required]
        [StringLength(150)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public DateTime AddedDate { get; set; }

        public DateTime LastVisitDate { get; set; }

        [StringLength(150)]
        public string AvatarPath { get; set; }

        [StringLength(500)]
        public string FirstName { get; set; }

        [StringLength(500)]
        public string LastName { get; set; }

        public virtual ICollection<BicycleParking> BicycleParkings { get; set; }

        public virtual ICollection<BicycleParkingVote> BicycleParkingVotes { get; set; }

        public virtual ICollection<BycicleDirection> BycicleDirections { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Goal> Goals { get; set; }

        public virtual ICollection<Social> Socials { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public virtual ICollection<UtilityIssue> UtilityIssues { get; set; }

        public virtual ICollection<UtilityIssueHistory> UtilityIssueHistories { get; set; }

        public virtual ICollection<UtilityPhoto> UtilityPhotoes { get; set; }
    }
}
