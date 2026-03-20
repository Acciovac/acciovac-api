using acciovac.Application.Common;
using acciovac.Domain.Entities;
using MediatR;

namespace acciovac.Application.Behaviors.AiRules.Queries.GetActiveAiRules
{
    public sealed record GetActiveAiRulesQuery : IRequest<Result<IEnumerable<AiRule>>>;
}
