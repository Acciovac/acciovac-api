using acciovac.Application.Common;
using acciovac.Domain.Entities;
using MediatR;

namespace acciovac.Application.Behaviors.BudgetRanges.Queries.GetBudgetRangeById
{
    public sealed record GetBudgetRangeByIdQuery(int Id) : IRequest<Result<BudgetRange>>;
}
