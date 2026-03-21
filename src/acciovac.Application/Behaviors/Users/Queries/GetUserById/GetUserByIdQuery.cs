using acciovac.Application.Common;
using acciovac.Domain.Entities;
using MediatR;

namespace acciovac.Application.Behaviors.Users.Queries.GetUserById
{
    public sealed record GetUserByIdQuery(Guid Id) : IRequest<Result<User>>;
}
