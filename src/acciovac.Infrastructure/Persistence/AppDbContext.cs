using AccioVac.Domain.Entities;
using AccioVac.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AccioVac.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) 
    : DbContext(options), IAppDbContext
{
    public DbSet<Trip> Trips { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // 1. Configure JSONB for Postgres
        builder.Entity<Trip>()
            .Property(t => t.AiItineraryJson)
            .HasColumnType("jsonb"); // <--- CRITICAL FOR POSTGRES

        // 2. Seed the Dev User (so you can work without Mobile app)
        builder.Entity<User>().HasData(
            new User 
            { 
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), 
                Email = "dev@acciovac.com",
                FullName = "Dev User",
                ExternalIdentityId = "dev_mode"
            }
        );
    }
}