using acciovac.Application.Abstractions;
using acciovac.Application.Common;
using acciovac.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace acciovac.Application.Behaviors.BudgetRanges.Queries.GetBudgetRangeById
{
    public class GetBudgetRangeByIdQueryHandler : IRequestHandler<GetBudgetRangeByIdQuery, Result<BudgetRange>>
    {
        private readonly IAppDbContext _context;

        public GetBudgetRangeByIdQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<BudgetRange>> Handle(GetBudgetRangeByIdQuery request, CancellationToken cancellationToken)
        {
            var range = await _context.BudgetRanges.FindAsync(new object[] { request.Id }, cancellationToken);
            
            if (range == null)
            {
                return Result<BudgetRange>.Failure("Budget range not found");
            }
            
            return Result<BudgetRange>.Success(range);
        }
    }
}
