using acciovac.Application.Common;
using MediatR;

namespace acciovac.Application.Behaviors.Messages.Commands.DeleteMessage
{
    public sealed record DeleteMessageCommand(Guid Id) : IRequest<Result<Guid>>;
}
