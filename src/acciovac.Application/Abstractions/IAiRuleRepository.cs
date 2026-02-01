using acciovac.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace acciovac.Application.Abstractions
{
    public interface IAiRuleRepository
    {
        Task<List<AiRule>> GetActiveRulesAsync();
    }
}
