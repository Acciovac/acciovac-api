using FluentValidation;

namespace acciovac.Application.Behaviors.AiRules.Commands.DeactivateAiRule
{
    public class DeactivateAiRuleCommandValidator : AbstractValidator<DeactivateAiRuleCommand>
    {
        public DeactivateAiRuleCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("AI rule ID is required");
        }
    }
}
