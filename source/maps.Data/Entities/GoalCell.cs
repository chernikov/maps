namespace maps.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GoalCell")]
    public partial class GoalCell
    {
        public int ID { get; set; }

        public int GoalID { get; set; }

        public int Number { get; set; }

        public int State { get; set; }

        public DateTime? AddedDate { get; set; }

        public virtual Goal Goal { get; set; }
    }
}
