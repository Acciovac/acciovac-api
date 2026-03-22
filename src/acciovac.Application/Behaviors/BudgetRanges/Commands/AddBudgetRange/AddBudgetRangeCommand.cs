using acciovac.Application.Common;
using MediatR;
using System;

namespace acciovac.Application.Behaviors.BudgetRanges.Commands.AddBudgetRange
{
    public sealed record AddBudgetRangeCommand(
        byte Type,
        decimal FromBudget,
        decimal ToBudget,
        bool IsActive = true
    ) : IRequest<Result<int>>;
}
