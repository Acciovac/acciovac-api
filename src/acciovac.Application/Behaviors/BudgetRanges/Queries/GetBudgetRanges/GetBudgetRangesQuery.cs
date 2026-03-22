using acciovac.Application.Common;
using acciovac.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace acciovac.Application.Behaviors.BudgetRanges.Queries.GetBudgetRanges
{
    public sealed record GetBudgetRangesQuery() : IRequest<Result<IReadOnlyList<BudgetRange>>>;
}
