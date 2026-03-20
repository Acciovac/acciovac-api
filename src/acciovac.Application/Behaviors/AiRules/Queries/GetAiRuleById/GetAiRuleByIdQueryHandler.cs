using acciovac.Application.Abstractions;
using acciovac.Application.Common;
using acciovac.Domain.Entities;
using MediatR;

namespace acciovac.Application.Behaviors.AiRules.Queries.GetAiRuleById
{
    public class GetAiRuleByIdQueryHandler : IRequestHandler<GetAiRuleByIdQuery, Result<AiRule>>
    {
        private readonly IAiRuleRepository _aiRuleRepository;

        public GetAiRuleByIdQueryHandler(IAiRuleRepository aiRuleRepository)
        {
            _aiRuleRepository = aiRuleRepository;
        }

        public async Task<Result<AiRule>> Handle(GetAiRuleByIdQuery request, CancellationToken cancellationToken)
        {
            var rule = await _aiRuleRepository.GetByIdAsync(request.Id);

            if (rule is null)
            {
                return Result<AiRule>.Failure("AI rule not found");
            }

            return Result<AiRule>.Success(rule);
        }
    }
}
