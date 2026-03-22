using FluentValidation;

namespace acciovac.Application.Behaviors.BudgetRanges.Commands.UpdateBudgetRange
{
    public class UpdateBudgetRangeCommandValidator : AbstractValidator<UpdateBudgetRangeCommand>
    {
        public UpdateBudgetRangeCommandValidator()
        {
            RuleFor(v => v.Id).GreaterThan(0);
            RuleFor(v => v.FromBudget)
                .LessThanOrEqualTo(v => v.ToBudget).WithMessage("FromBudget must be less than or equal to ToBudget.");
        }
    }
}
