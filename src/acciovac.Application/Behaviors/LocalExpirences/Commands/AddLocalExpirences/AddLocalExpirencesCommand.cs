using acciovac.Application.Common;
using MediatR;

namespace acciovac.Application.Behaviors.LocalExpirences.Commands.AddLocalExpirences
{
    public sealed record AddLocalExpirencesCommand(
        string LocationName,
        string Description
    ) : IRequest<Result<Guid>>;
}
