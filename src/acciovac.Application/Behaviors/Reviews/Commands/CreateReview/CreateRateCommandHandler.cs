using System;
using acciovac.Application.Abstractions;

namespace acciovac.Application.Behaviors.Reviews.Commands.CreateReview;

public class UpdateRateCommandHandler : IRequestHandler<CreateReviewCommand, Result<Guid>>
    {
        private readonly IReviewRepository _reviewRepository;

        public UpdateRateCommandHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<Result<Guid>> Handle(CreateReviewCommand review, CancellationToken cancellationToken)
        {
            var rate = await _reviewRepository.GetByIdAsync(review.UserId);

            if (rate is null)
            {
                return Result<Guid>.Failure("Review not found");
            }

            rate.UpdateRates(
                request.BaseFare,
                request.PerKm,
                request.NightMultiplier ?? 1.0m,
                request.PeakMultiplier ?? 1.0m,
                "system"
            );

            await _rateRepository.UpdateAsync(rate);

            return Result<Guid>.Success(rate.Id);
        }
    }