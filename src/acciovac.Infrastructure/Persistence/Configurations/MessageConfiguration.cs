using acciovac.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace acciovac.Infrastructure.Persistence.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Subject).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Body).HasColumnType("text").IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.IsRead).IsRequired();
            builder.Property(x => x.IsResolved).IsRequired();

            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.CreatedAt);
        }
    }
}
