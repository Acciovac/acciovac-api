using acciovac.Application.Abstractions;
using acciovac.Application.Common;
using MediatR;

namespace acciovac.Application.Behaviors.Messages.Commands.MarkMessageAsResolved
{
    public class MarkMessageAsResolvedCommandHandler : IRequestHandler<MarkMessageAsResolvedCommand, Result<Guid>>
    {
        private readonly IMessageRepository _messageRepository;

        public MarkMessageAsResolvedCommandHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<Result<Guid>> Handle(MarkMessageAsResolvedCommand request, CancellationToken cancellationToken)
        {
            var message = await _messageRepository.GetByIdAsync(request.Id);

            if (message is null)
            {
                return Result<Guid>.Failure("Message not found.");
            }

            var modifiedBy = string.IsNullOrWhiteSpace(request.ModifiedBy) ? "system" : request.ModifiedBy;
            message.MarkAsResolved(modifiedBy!);

            await _messageRepository.UpdateAsync(message);

            return Result<Guid>.Success(message.Id);
        }
    }
}
