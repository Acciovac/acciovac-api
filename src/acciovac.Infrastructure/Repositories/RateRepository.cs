using acciovac.Application.Abstractions;
using acciovac.Domain.Entities;
using acciovac.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace acciovac.Infrastructure.Repositories
{
    public class RateRepository : IRateRepository
    {
        private readonly AppDbContext _context;

        public RateRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Rate?> GetByIdAsync(Guid id)
        {
            return await _context.Rates
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Rate?> GetActiveRateAsync()
        {
            return await _context.Rates
                .OrderByDescending(r => r.CreatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Rate rate)
        {
            await _context.Rates.AddAsync(rate);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Rate rate)
        {
            _context.Rates.Update(rate);
            await _context.SaveChangesAsync();
        }
    }
}
