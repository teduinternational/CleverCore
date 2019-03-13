using CleverCore.Data.EF.Extensions;
using CleverCore.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleverCore.Data.EF.Configurations
{
    public class PageConfiguration : DbEntityConfiguration<Page>
    {
        public override void Configure(EntityTypeBuilder<Page> entity)
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).HasMaxLength(255).IsRequired();
            // etc.
        }
    }
}
