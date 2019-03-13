using CleverCore.Data.EF.Extensions;
using CleverCore.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleverCore.Data.EF.Configurations
{
    public class TagConfiguration : DbEntityConfiguration<Tag>
    {
        public override void Configure(EntityTypeBuilder<Tag> entity)
        {
            entity.Property(c => c.Id).HasMaxLength(50)
                .IsRequired().IsUnicode(false).HasMaxLength(50);
        }
    }
}
