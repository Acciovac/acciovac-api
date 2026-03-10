using System;
using acciovac.Domain.Entities;

namespace acciovac.Application.Abstractions;

public interface ILocalExpirences
{
    Task<LocalExpirences?> GetByIdAsync(Guid id);
    Task<LocalExpirences?> GetActiveLocalExpirencesAsync();
    Task AddAsync(LocalExpirences localExpirences);
    Task UpdateAsync(LocalExpirences localExpirences);
}
