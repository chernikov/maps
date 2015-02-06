using maps.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maps.Data.Configuration
{
    internal class CommentConfiguration : EntityTypeConfiguration<Comment>
    {

        public CommentConfiguration()
        {
            HasMany(e => e.Comments)
             .WithOptional(e => e.Parent)
             .HasForeignKey(e => e.ParentID);
        }
    }
}
