using acciovac.Application.Common;
using MediatR;

namespace acciovac.Application.Behaviors.Messages.Commands.CreateMessage
{
    public sealed record CreateMessageCommand(
        Guid UserId,
        string Subject,
        string Body,
        string? CreatedBy
    ) : IRequest<Result<Guid>>;
}