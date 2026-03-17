using acciovac.Application.Abstractions;
using acciovac.Application.Common;
using MediatR;

namespace acciovac.Application.Behaviors.LocalExpirences.Commands.DeleteLocalExpirences
{
    public class DeleteLocalExpirencesCommandHandler : IRequestHandler<DeleteLocalExpirencesCommand, Result<Guid>>
    {
        private readonly ILocalExpirences _repository;

        public DeleteLocalExpirencesCommandHandler(ILocalExpirences repository)
        {
            _repository = repository;
        }

        public async Task<Result<Guid>> Handle(DeleteLocalExpirencesCommand request, CancellationToken cancellationToken)
        {
            var localExperience = await _repository.GetByIdAsync(request.Id);

            if (localExperience is null)
            {
                return Result<Guid>.Failure("Local experience not found");
            }

            await _repository.DeleteAsync(localExperience);

            return Result<Guid>.Success(request.Id);
        }
    }
}
