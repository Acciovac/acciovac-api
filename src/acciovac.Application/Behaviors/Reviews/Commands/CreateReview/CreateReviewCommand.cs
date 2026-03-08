using acciovac.Application.Common;
using MediatR;

namespace acciovac.Application.Behaviors.Reviews.Commands.CreateReview;

public record class CreateReviewCommand
(
    Guid UserId,
    int Rating,
    string Comment
) : IRequest<Result<Guid>>;
