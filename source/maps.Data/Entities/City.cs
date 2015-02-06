namespace maps.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("City")]
    public partial class City
    {
        public City()
        {
            BicycleLines = new HashSet<BicycleLine>();
            BicycleParkings = new HashSet<BicycleParking>();
            BycicleDirections = new HashSet<BycicleDirection>();
            InstagramPhotoes = new HashSet<InstagramPhoto>();
            Users = new HashSet<User>();
            UtilityDepartments = new HashSet<UtilityDepartment>();
            UtilityIssues = new HashSet<UtilityIssue>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public double CenterLat { get; set; }

        public double CenterLng { get; set; }

        public int Zoom { get; set; }

        public virtual ICollection<BicycleLine> BicycleLines { get; set; }

        public virtual ICollection<BicycleParking> BicycleParkings { get; set; }

        public virtual ICollection<BycicleDirection> BycicleDirections { get; set; }

        public virtual ICollection<InstagramPhoto> InstagramPhotoes { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<UtilityDepartment> UtilityDepartments { get; set; }

        public virtual ICollection<UtilityIssue> UtilityIssues { get; set; }
    }
}
