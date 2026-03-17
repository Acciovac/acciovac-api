using acciovac.Application.Common;
using MediatR;
using LocalExpirencesEntity = acciovac.Domain.Entities.LocalExpirences;

namespace acciovac.Application.Behaviors.LocalExpirences.Queries.GetAllLocalExpirences
{
    public sealed record GetAllLocalExpirencesQuery() : IRequest<Result<IEnumerable<LocalExpirencesEntity>>>;
}
