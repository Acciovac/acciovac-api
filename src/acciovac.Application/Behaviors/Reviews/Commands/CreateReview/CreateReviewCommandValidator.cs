using System;
using FluentValidation;

namespace acciovac.Application.Behaviors.Reviews.Commands.CreateReview;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required");

        RuleFor(x => x.Rating)
            .NotEmpty().WithMessage("Rating is required");

        RuleFor(x => x.Comment)
            .NotEmpty().WithMessage("Comment ID is required");
    }
}
