using acciovac.Application.Abstractions;
using acciovac.Application.Common;
using acciovac.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace acciovac.Application.Behaviors.BudgetRanges.Queries.GetBudgetRanges
{
    public class GetBudgetRangesQueryHandler : IRequestHandler<GetBudgetRangesQuery, Result<IReadOnlyList<BudgetRange>>>
    {
        private readonly IAppDbContext _context;

        public GetBudgetRangesQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IReadOnlyList<BudgetRange>>> Handle(GetBudgetRangesQuery request, CancellationToken cancellationToken)
        {
            var ranges = await _context.BudgetRanges.ToListAsync(cancellationToken);
            return Result<IReadOnlyList<BudgetRange>>.Success(ranges);
        }
    }
}
