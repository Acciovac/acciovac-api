using acciovac.Application.Common;
using MediatR;
using System;

namespace acciovac.Application.Behaviors.BudgetRanges.Commands.UpdateBudgetRange
{
    public sealed record UpdateBudgetRangeCommand(
        int Id,
        byte Type,
        decimal FromBudget,
        decimal ToBudget,
        bool IsActive
    ) : IRequest<Result<int>>;
}
