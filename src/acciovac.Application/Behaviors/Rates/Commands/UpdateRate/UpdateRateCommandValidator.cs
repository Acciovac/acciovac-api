using FluentValidation;

namespace acciovac.Application.Behaviors.Rates.Commands.UpdateRate
{
    public class UpdateRateCommandValidator : AbstractValidator<UpdateRateCommand>
    {
        public UpdateRateCommandValidator()
        {
            RuleFor(x => x.RateId)
                .NotEmpty().WithMessage("Rate ID is required");

            RuleFor(x => x.BaseFare)
                .GreaterThan(0).WithMessage("Base fare must be greater than 0");

            RuleFor(x => x.PerKm)
                .GreaterThan(0).WithMessage("Per km rate must be greater than 0");

            // RuleFor(x => x.NightMultiplier)
            //     .GreaterThan(0).WithMessage("Night multiplier must be greater than 0")
            //     .When(x => x.NightMultiplier.HasValue);

            // RuleFor(x => x.PeakMultiplier)
            //     .GreaterThan(0).WithMessage("Peak multiplier must be greater than 0")
            //     .When(x => x.PeakMultiplier.HasValue);
        }
    }
}
