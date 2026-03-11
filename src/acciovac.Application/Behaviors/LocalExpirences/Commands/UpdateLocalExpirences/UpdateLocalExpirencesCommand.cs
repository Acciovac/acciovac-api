using acciovac.Application.Common;
using MediatR;

namespace acciovac.Application.Behaviors.LocalExpirences.Commands.UpdateLocalExpirences
{
    public sealed record UpdateLocalExpirencesCommand(
        Guid Id,
        string LocationName,
        string Description
    ) : IRequest<Result<Guid>>;
}
