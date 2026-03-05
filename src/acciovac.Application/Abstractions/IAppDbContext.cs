using acciovac.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace acciovac.Application.Abstractions
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; }
        DbSet<AiRule> AiRules { get; }
        DbSet<Rate> Rates { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
