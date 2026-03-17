using acciovac.Application.Common;
using MediatR;
using System.Collections.Generic;

namespace acciovac.Application.Behaviors.Users.Queries.GetAllUsers
{
    public sealed record GetAllUsersQuery() : IRequest<Result<IReadOnlyList<UserDto>>>;
}
