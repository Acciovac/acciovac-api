using acciovac.Application.Common;
using MediatR;

namespace acciovac.Application.Behaviors.LocalExpirences.Commands.DeleteLocalExpirences
{
    public sealed record DeleteLocalExpirencesCommand(Guid Id) : IRequest<Result<Guid>>;
}
