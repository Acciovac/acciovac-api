using acciovac.Application.Abstractions;
using acciovac.Application.Common;
using MediatR;

namespace acciovac.Application.Behaviors.Messages.Queries.GetAllMessages
{
    public class GetAllMessagesQueryHandler : IRequestHandler<GetAllMessagesQuery, Result<IReadOnlyList<GetAllMessageDto>>>
    {
        private readonly IMessageRepository _messageRepository;

        public GetAllMessagesQueryHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<Result<IReadOnlyList<GetAllMessageDto>>> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = await _messageRepository.GetAllAsync();

            var dtos = messages
                .Select(x => new GetAllMessageDto(
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

            return Result<IReadOnlyList<GetAllMessageDto>>.Success(dtos);
        }
    }
}