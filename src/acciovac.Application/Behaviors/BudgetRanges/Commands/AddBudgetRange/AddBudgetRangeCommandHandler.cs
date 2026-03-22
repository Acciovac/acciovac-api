using acciovac.Application.Abstractions;
using acciovac.Application.Common;
using acciovac.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace acciovac.Application.Behaviors.BudgetRanges.Commands.AddBudgetRange
{
    public class AddBudgetRangeCommandHandler : IRequestHandler<AddBudgetRangeCommand, Result<int>>
    {
        private readonly IAppDbContext _context;

        public AddBudgetRangeCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<int>> Handle(AddBudgetRangeCommand request, CancellationToken cancellationToken)
        {
            var entity = new BudgetRange(
                request.Type,
                request.FromBudget,
                request.ToBudget,
                request.IsActive,
                "Admin"
            );

            _context.BudgetRanges.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(entity.Id);
        }
    }
}
