using System;
using FluentValidation;

namespace acciovac.Application.Behaviors.Reviews.Commands.CreateReview;

public class UpdateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public UpdateReviewCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required");

        RuleFor(x => x.Rating)
            .NotEmpty().WithMessage("Rating is required");

        RuleFor(x => x.Comment)
            .NotEmpty().WithMessage("Comment ID is required");
    }
}
