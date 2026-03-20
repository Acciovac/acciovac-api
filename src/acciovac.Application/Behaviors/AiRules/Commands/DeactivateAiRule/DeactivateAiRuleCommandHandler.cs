using acciovac.Application.Abstractions;
using acciovac.Application.Common;
using MediatR;

namespace acciovac.Application.Behaviors.AiRules.Commands.DeactivateAiRule
{
    public class DeactivateAiRuleCommandHandler : IRequestHandler<DeactivateAiRuleCommand, Result<Guid>>
    {
        private readonly IAiRuleRepository _aiRuleRepository;

        public DeactivateAiRuleCommandHandler(IAiRuleRepository aiRuleRepository)
        {
            _aiRuleRepository = aiRuleRepository;
        }

        public async Task<Result<Guid>> Handle(DeactivateAiRuleCommand request, CancellationToken cancellationToken)
        {
            var rule = await _aiRuleRepository.GetByIdAsync(request.Id);

            if (rule is null)
            {
                return Result<Guid>.Failure("AI rule not found");
            }

            if (!rule.IsActive)
            {
                return Result<Guid>.Failure("AI rule is already inactive");
            }

            rule.Deactivate("system");
            await _aiRuleRepository.UpdateAsync(rule);

            return Result<Guid>.Success(rule.Id);
        }
    }
}
