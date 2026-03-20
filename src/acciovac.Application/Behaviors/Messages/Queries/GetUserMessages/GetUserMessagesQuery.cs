using acciovac.Application.Common;
using MediatR;

namespace acciovac.Application.Behaviors.Messages.Queries.GetUserMessages
{
    public sealed record GetUserMessagesQuery(Guid UserId) : IRequest<Result<IReadOnlyList<MessageDto>>>;
}
