using acciovac.Application.Abstractions;
using acciovac.Application.Common;
using MediatR;
using LocalExpirencesEntity = acciovac.Domain.Entities.LocalExpirences;

namespace acciovac.Application.Behaviors.LocalExpirences.Queries.GetAllLocalExpirences
{
    public class GetAllLocalExpirencesQueryHandler : IRequestHandler<GetAllLocalExpirencesQuery, Result<IEnumerable<LocalExpirencesEntity>>>
    {
        private readonly ILocalExpirences _repository;

        public GetAllLocalExpirencesQueryHandler(ILocalExpirences repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<LocalExpirencesEntity>>> Handle(GetAllLocalExpirencesQuery request, CancellationToken cancellationToken)
        {
            var experiences = await _repository.GetAllAsync();

            return Result<IEnumerable<LocalExpirencesEntity>>.Success(experiences);
        }
    }
}
