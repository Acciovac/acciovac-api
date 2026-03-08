using System;
using acciovac.Domain.Entities;

namespace acciovac.Application.Abstractions;

public interface IReviewRepository
{
    Task<Review?> GetByIdAsync(Guid id);
    Task<Review?> GetActivateReviewAsync();
    Task UpdateAsync(Review review);
}
