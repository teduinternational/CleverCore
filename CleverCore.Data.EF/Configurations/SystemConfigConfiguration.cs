using CleverCore.Data.EF.Extensions;
using CleverCore.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleverCore.Data.EF.Configurations
{
    class SystemConfigConfiguration : DbEntityConfiguration<SystemConfig>
    {
        public override void Configure(EntityTypeBuilder<SystemConfig> entity)
        {
            entity.Property(c => c.Id).HasMaxLength(255).IsRequired();
            // etc.
        }
    }
}
