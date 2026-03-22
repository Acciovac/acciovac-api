using acciovac.Application.Abstractions;
using acciovac.Application.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace acciovac.Application.Behaviors.BudgetRanges.Commands.UpdateBudgetRange
{
    public class UpdateBudgetRangeCommandHandler : IRequestHandler<UpdateBudgetRangeCommand, Result<int>>
    {
        private readonly IAppDbContext _context;

        public UpdateBudgetRangeCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<int>> Handle(UpdateBudgetRangeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.BudgetRanges.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                return Result<int>.Failure("Budget range not found");
            }

            entity.Update(
                request.Type,
                request.FromBudget,
                request.ToBudget,
                request.IsActive,
                "Admin"
            );

            await _context.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(entity.Id);
        }
    }
}
