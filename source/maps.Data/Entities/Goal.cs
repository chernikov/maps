namespace maps.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Goal")]
    public partial class Goal
    {
        public Goal()
        {
            GoalCells = new HashSet<GoalCell>();
        }

        public int ID { get; set; }

        public int UserID { get; set; }

        public bool IsPublic { get; set; }

        [Required]
        [StringLength(140)]
        public string Url { get; set; }

        [Required]
        [StringLength(140)]
        public string Text { get; set; }

        public int Count { get; set; }

        public int Progress { get; set; }

        public DateTime AddedDate { get; set; }

        public bool IsReady { get; set; }

        public int ColumnsCount { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<GoalCell> GoalCells { get; set; }
    }
}
