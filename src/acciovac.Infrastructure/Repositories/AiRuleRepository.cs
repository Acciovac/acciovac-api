using acciovac.Application.Abstractions;
using acciovac.Domain.Entities;
using acciovac.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace acciovac.Infrastructure.Repositories
{
    public class AiRuleRepository : IAiRuleRepository
    {
        private readonly AppDbContext _context;
        public AiRuleRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(AiRule aiRule)
        {
            await _context.AiRules.AddAsync(aiRule);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AiRule>> GetActiveRulesAsync()
        {
            return await _context.AiRules
                .Where(r => r.IsActive)
                .ToListAsync();
        }
    }
}
