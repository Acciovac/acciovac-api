using FluentValidation;

namespace acciovac.Application.Behaviors.AiRules.Queries.GetAiRuleById
{
    public class GetAiRuleByIdQueryValidator : AbstractValidator<GetAiRuleByIdQuery>
    {
        public GetAiRuleByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("AI rule ID is required");
        }
    }
}
