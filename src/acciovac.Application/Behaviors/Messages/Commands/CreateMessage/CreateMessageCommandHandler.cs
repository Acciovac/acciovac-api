using acciovac.Application.Abstractions;
using acciovac.Application.Common;
using acciovac.Domain.Entities;
using MediatR;

namespace acciovac.Application.Behaviors.Messages.Commands.CreateMessage
{
    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, Result<Guid>>
    {
        private readonly IMessageRepository _messageRepository;

        public CreateMessageCommandHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<Result<Guid>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var createdBy = string.IsNullOrWhiteSpace(request.CreatedBy) ? "system" : request.CreatedBy.Trim();

            var message = new Message(
                request.UserId,
                request.Subject.Trim(),
                request.Body.Trim(),
                createdBy);

            await _messageRepository.AddAsync(message);

            return Result<Guid>.Success(message.Id);
        }
    }
}