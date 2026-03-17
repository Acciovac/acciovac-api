using FluentValidation;

namespace acciovac.Application.Behaviors.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.FirebaseUid)
                .NotEmpty().WithMessage("Firebase UID is required");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Role)
                .MaximumLength(64).WithMessage("Role must not exceed 64 characters")
                .When(x => !string.IsNullOrWhiteSpace(x.Role));
        }
    }
}
