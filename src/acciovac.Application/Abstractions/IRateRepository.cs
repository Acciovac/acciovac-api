using acciovac.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace acciovac.Application.Abstractions
{
    public interface IRateRepository
    {
        Task<Rate?> GetByIdAsync(Guid id);
        Task<Rate?> GetActiveRateAsync();
        Task AddAsync(Rate rate);
        Task UpdateAsync(Rate rate);
    }
}
