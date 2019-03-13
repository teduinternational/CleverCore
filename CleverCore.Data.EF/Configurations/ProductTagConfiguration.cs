using CleverCore.Data.EF.Extensions;
using CleverCore.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleverCore.Data.EF.Configurations
{
    public class ProductTagConfiguration : DbEntityConfiguration<ProductTag>
    {
        public override void Configure(EntityTypeBuilder<ProductTag> entity)
        {
            entity.Property(c => c.TagId).HasMaxLength(50).IsRequired()
            .HasMaxLength(50).IsUnicode(false);
            // etc.
        }
    }
}
