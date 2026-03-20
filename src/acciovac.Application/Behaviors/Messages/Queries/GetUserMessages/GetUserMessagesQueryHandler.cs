using acciovac.Application.Abstractions;
using acciovac.Application.Common;
using MediatR;

namespace acciovac.Application.Behaviors.Messages.Queries.GetUserMessages
{
    public class GetUserMessagesQueryHandler : IRequestHandler<GetUserMessagesQuery, Result<IReadOnlyList<MessageDto>>>
    {
        private readonly IMessageRepository _messageRepository;

        public GetUserMessagesQueryHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<Result<IReadOnlyList<MessageDto>>> Handle(GetUserMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = await _messageRepository.GetByUserIdAsync(request.UserId);

            var dtos = messages
                .Select(x => new MessageDto(
                    x.Id,
                    x.UserId,
                    x.Subject,
                    x.Body,
                    x.IsRead,
                    x.ReadAt,
                    x.IsResolved,
                    x.ResolvedAt,
                    x.CreatedAt))
                .ToList();

            return Result<IReadOnlyList<MessageDto>>.Success(dtos);
        }
    }
}
