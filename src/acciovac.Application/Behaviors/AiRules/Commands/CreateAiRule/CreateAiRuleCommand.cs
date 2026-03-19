using acciovac.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace acciovac.Application.Behaviors.AiRules.Commands.CreateAiRule
{
    public sealed record CreateAiRuleCommand(
        string Code,
        string RuleText,
        int Priority
        ) : IRequest<Result<Guid>>;
}
