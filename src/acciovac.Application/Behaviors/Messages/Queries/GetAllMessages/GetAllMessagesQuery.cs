using acciovac.Application.Common;
using MediatR;

namespace acciovac.Application.Behaviors.Messages.Queries.GetAllMessages
{
    public sealed record GetAllMessagesQuery() : IRequest<Result<IReadOnlyList<GetAllMessageDto>>>;
}