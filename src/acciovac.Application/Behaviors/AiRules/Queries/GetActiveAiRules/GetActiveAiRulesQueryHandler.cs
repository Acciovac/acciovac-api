using acciovac.Application.Abstractions;
using acciovac.Application.Common;
using acciovac.Domain.Entities;
using MediatR;

namespace acciovac.Application.Behaviors.AiRules.Queries.GetActiveAiRules
{
    public class GetActiveAiRulesQueryHandler : IRequestHandler<GetActiveAiRulesQuery, Result<IEnumerable<AiRule>>>
    {
        private readonly IAiRuleRepository _aiRuleRepository;

        public GetActiveAiRulesQueryHandler(IAiRuleRepository aiRuleRepository)
        {
            _aiRuleRepository = aiRuleRepository;
        }

        public async Task<Result<IEnumerable<AiRule>>> Handle(GetActiveAiRulesQuery request, CancellationToken cancellationToken)
        {
            var rules = await _aiRuleRepository.GetActiveRulesAsync();
            return Result<IEnumerable<AiRule>>.Success(rules);
        }
    }
}
