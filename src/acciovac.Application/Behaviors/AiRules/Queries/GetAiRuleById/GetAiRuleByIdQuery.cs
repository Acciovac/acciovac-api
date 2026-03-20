using acciovac.Application.Common;
using acciovac.Domain.Entities;
using MediatR;

namespace acciovac.Application.Behaviors.AiRules.Queries.GetAiRuleById
{
    public sealed record GetAiRuleByIdQuery(Guid Id) : IRequest<Result<AiRule>>;
}
