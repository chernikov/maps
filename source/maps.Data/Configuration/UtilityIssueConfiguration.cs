using maps.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Data.Configuration
{
    internal class UtilityIssueConfiguration : EntityTypeConfiguration<UtilityIssue>
    {
        public UtilityIssueConfiguration()
        {
            HasMany(e => e.UtilityIssues)
                .WithOptional(e => e.MainUtilityIssue)
                .HasForeignKey(e => e.MainUtilityIssueID);
        }
    }
}
