using maps.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Data.Configuration
{
    internal class UtilityIssueHistoryConfiguration : EntityTypeConfiguration<UtilityIssueHistory>
    {
        public UtilityIssueHistoryConfiguration()
        {
            HasMany(e => e.UtilityIssueHistorys)
                .WithOptional(e => e.MainUtilityIssueHistory)
                .HasForeignKey(e => e.MainUtilityIssueID);
        }
    }
}
