using acciovac.Application.Common;
using acciovac.Domain.Entities;
using MediatR;

namespace acciovac.Application.Behaviors.Users.Queries.GetUsers
{
    public sealed record GetUsersQuery : IRequest<Result<IEnumerable<User>>>;
}
