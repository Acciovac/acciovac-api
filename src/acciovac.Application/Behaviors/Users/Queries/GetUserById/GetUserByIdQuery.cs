using acciovac.Application.Common;
using MediatR;
using System;

namespace acciovac.Application.Behaviors.Users.Queries.GetUserById
{
    public sealed record GetUserByIdQuery(Guid Id) : IRequest<Result<UserByIdDto>>;
}
