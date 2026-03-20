using acciovac.Application.Common;
using MediatR;

namespace acciovac.Application.Behaviors.Messages.Commands.MarkMessageAsRead
{
    public sealed record MarkMessageAsReadCommand(Guid Id, string? ModifiedBy) : IRequest<Result<Guid>>;
}
