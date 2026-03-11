using acciovac.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace acciovac.Infrastructure.Persistence.Configurations
{
    public class LocalExpirencesConfiguration : IEntityTypeConfiguration<LocalExpirences>
    {
        public void Configure(EntityTypeBuilder<LocalExpirences> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.LocationName)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(1000)
                .IsRequired();
        }
    }
}
