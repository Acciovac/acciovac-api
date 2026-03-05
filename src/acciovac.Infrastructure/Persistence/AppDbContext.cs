using acciovac.Application.Abstractions;
using acciovac.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace acciovac.Infrastructure.Persistence
{
    public class AppDbContext : DbContext , IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
               : base(options) { }


        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<AiRule> AiRules => Set<AiRule>();
        public DbSet<Rate> Rates => Set<Rate>();
        public DbSet<LocalExpirences> LocalExpirences => Set<LocalExpirences>();
        public DbSet<Message> Messages => Set<Message>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
