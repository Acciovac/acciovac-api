using System;
using acciovac.Domain.Entities;

namespace acciovac.Application.Abstractions;

public interface ILocalExpirences
{
    Task<LocalExpirences?> GetByIdAsync(Guid id);
    Task<IEnumerable<LocalExpirences>> GetAllAsync();
    Task AddAsync(LocalExpirences localExpirences);
    Task UpdateAsync(LocalExpirences localExpirences);
    Task DeleteAsync(LocalExpirences localExpirences);
    Task AddPhotoAsync(Guid localExpirencesId, string photoUrl, int displayOrder);
}