using acciovac.Application.Common;
using MediatR;

namespace acciovac.Application.Behaviors.AiRules.Commands.DeactivateAiRule
{
    public sealed record DeactivateAiRuleCommand(Guid Id) : IRequest<Result<Guid>>;
}
