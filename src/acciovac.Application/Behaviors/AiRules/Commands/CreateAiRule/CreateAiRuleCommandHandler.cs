using acciovac.Application.Abstractions;
using acciovac.Application.Common;
using acciovac.Domain.Entities;
using MediatR;
using System.Linq;

namespace acciovac.Application.Behaviors.AiRules.Commands.CreateAiRule
{
    public class CreateAiRuleCommandHandler : IRequestHandler<CreateAiRuleCommand, Result<Guid>>
    {
        private readonly IAiRuleRepository _aiRuleRepository;

        public CreateAiRuleCommandHandler(IAiRuleRepository aiRuleRepository)
        {
            _aiRuleRepository = aiRuleRepository;
        }

        public async Task<Result<Guid>> Handle(CreateAiRuleCommand request, CancellationToken cancellationToken)
        {
            var activeRules = await _aiRuleRepository.GetActiveRulesAsync();

            if (activeRules.Any(rule => rule.Code == request.Code))
            {
                return Result<Guid>.Failure("Rule with same code already exists");
            }

            var rule = new AiRule(
                request.Code,
                request.RuleText,
                request.Priority,
                request.CreatedBy
            );

            await _aiRuleRepository.AddAsync(rule);

            return Result<Guid>.Success(rule.Id);
        }
    }
}
