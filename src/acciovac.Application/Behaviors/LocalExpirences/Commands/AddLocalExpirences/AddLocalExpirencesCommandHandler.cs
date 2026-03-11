using acciovac.Application.Abstractions;
using acciovac.Application.Common;
using MediatR;
using LocalExpirencesEntity = acciovac.Domain.Entities.LocalExpirences;

namespace acciovac.Application.Behaviors.LocalExpirences.Commands.AddLocalExpirences
{
    public class AddLocalExpirencesCommandHandler : IRequestHandler<AddLocalExpirencesCommand, Result<Guid>>
    {
        private readonly ILocalExpirences _repository;

        public AddLocalExpirencesCommandHandler(ILocalExpirences repository)
        {
            _repository = repository;
        }

        public async Task<Result<Guid>> Handle(AddLocalExpirencesCommand request, CancellationToken cancellationToken)
        {
            var localExperience = new LocalExpirencesEntity(
                request.LocationName,
                request.Description,
                "system"
            );

            await _repository.AddAsync(localExperience);

            return Result<Guid>.Success(localExperience.Id);
        }
    }
}
