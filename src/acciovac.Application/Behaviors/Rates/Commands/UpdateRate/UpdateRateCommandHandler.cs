using acciovac.Application.Abstractions;
using acciovac.Application.Common;
using MediatR;

namespace acciovac.Application.Behaviors.Rates.Commands.UpdateRate
{
    public class UpdateRateCommandHandler : IRequestHandler<UpdateRateCommand, Result<Guid>>
    {
        private readonly IRateRepository _rateRepository;

        public UpdateRateCommandHandler(IRateRepository rateRepository)
        {
            _rateRepository = rateRepository;
        }

        public async Task<Result<Guid>> Handle(UpdateRateCommand request, CancellationToken cancellationToken)
        {
            var rate = await _rateRepository.GetByIdAsync(request.RateId);

            if (rate is null)
            {
                return Result<Guid>.Failure("Rate not found");
            }

            rate.UpdateRates(
                request.BaseFare,
                request.PerKm,
                // request.NightMultiplier ?? 1.0m,
                // request.PeakMultiplier ?? 1.0m,
                "system"
            );

            await _rateRepository.UpdateAsync(rate);

            return Result<Guid>.Success(rate.Id);
        }
    }
}
