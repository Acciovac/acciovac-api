using FluentValidation;

namespace acciovac.Application.Behaviors.BudgetRanges.Commands.AddBudgetRange
{
    public class AddBudgetRangeCommandValidator : AbstractValidator<AddBudgetRangeCommand>
    {
        public AddBudgetRangeCommandValidator()
        {
            RuleFor(v => v.FromBudget)
                .LessThanOrEqualTo(v => v.ToBudget).WithMessage("FromBudget must be less than or equal to ToBudget.");
        }
    }
}
