using acciovac.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace acciovac.Infrastructure.Persistence.Configurations
{
    public class BudgetRangeConfiguration : IEntityTypeConfiguration<BudgetRange>
    {
        public void Configure(EntityTypeBuilder<BudgetRange> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type)
                .IsRequired();

            builder.Property(x => x.FromBudget)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.ToBudget)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        }
    }
}
