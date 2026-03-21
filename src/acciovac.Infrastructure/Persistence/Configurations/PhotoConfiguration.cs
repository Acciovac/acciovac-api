using acciovac.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace acciovac.Infrastructure.Persistence.Configurations
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.PhotoUrl)
                .IsRequired();

            builder.Property(x => x.DisplayOrder)
                .IsRequired();

            // Configure the relationship with LocalExpirences
            builder.HasOne<LocalExpirences>()
                .WithMany(le => le.Photos)
                .HasForeignKey(p => p.LocalExpirencesId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.LocalExpirencesId)
                .IsRequired();
        }
    }
}
