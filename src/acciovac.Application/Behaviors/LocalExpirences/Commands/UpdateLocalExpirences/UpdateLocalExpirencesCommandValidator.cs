using FluentValidation;

namespace acciovac.Application.Behaviors.LocalExpirences.Commands.UpdateLocalExpirences
{
    public class UpdateLocalExpirencesCommandValidator : AbstractValidator<UpdateLocalExpirencesCommand>
    {
        public UpdateLocalExpirencesCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required");

            RuleFor(x => x.LocationName)
                .NotEmpty().WithMessage("Location name is required")
                .MaximumLength(200).WithMessage("Location name must not exceed 200 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters");
        }
    }
}
