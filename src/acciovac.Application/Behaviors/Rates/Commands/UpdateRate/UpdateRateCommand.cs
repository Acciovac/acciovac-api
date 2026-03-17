using acciovac.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace acciovac.Application.Behaviors.Rates.Commands.UpdateRate
{
    public sealed record UpdateRateCommand(
        Guid RateId,
        decimal BaseFare,
        decimal PerKm,
        decimal? NightMultiplier,
        decimal? PeakMultiplier
    ) : IRequest<Result<Guid>>;
}
