using CleverCore.Data.EF.Extensions;
using CleverCore.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleverCore.Data.EF.Configurations
{
    public class BlogTagConfiguration : DbEntityConfiguration<BlogTag>
    {
        public override void Configure(EntityTypeBuilder<BlogTag> entity)
        {
            entity.Property(c => c.TagId).HasMaxLength(50).IsRequired()
            .IsUnicode(false).HasMaxLength(50);
            // etc.
        }
    }
}