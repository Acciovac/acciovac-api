using acciovac.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace acciovac.Application.Abstractions
{
    public interface IAiRuleRepository
    {
        Task AddAsync(AiRule aiRule);
        Task<List<AiRule>> GetActiveRulesAsync();
        Task<AiRule?> GetByIdAsync(Guid id);
        Task UpdateAsync(AiRule aiRule);
    }
}
