using acciovac.Application.Common;
using MediatR;

namespace acciovac.Application.Behaviors.Messages.Commands.MarkMessageAsResolved
{
    public sealed record MarkMessageAsResolvedCommand(Guid Id, string? ModifiedBy) : IRequest<Result<Guid>>;
}
