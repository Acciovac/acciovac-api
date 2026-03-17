using acciovac.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace acciovac.Infrastructure.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public static readonly Guid AdminRoleId = new Guid("00000000-0000-0000-0000-000000000001");
        public static readonly Guid UserRoleId  = new Guid("00000000-0000-0000-0000-000000000002");

        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(256);

            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasData(
                new Role(AdminRoleId, "Admin",     "Administrator with full access"),
                new Role(UserRoleId,  "User",      "Standard user"));
        }
    }
}
