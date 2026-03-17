using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace acciovac.Application.Behaviors.AiRules.Commands.CreateAiRule
{
    public class CreateAiRuleCommandValidator : AbstractValidator<CreateAiRuleCommand>
    {
        public CreateAiRuleCommandValidator() 
        {
            RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(50);

            RuleFor(x => x.RuleText)
                .NotEmpty();

            RuleFor(x => x.Priority)
                .GreaterThanOrEqualTo(0);
        }
    }
}
