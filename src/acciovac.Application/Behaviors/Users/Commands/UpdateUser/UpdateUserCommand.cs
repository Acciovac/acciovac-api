using acciovac.Application.Common;
using MediatR;
using System;

namespace acciovac.Application.Behaviors.Users.Commands.UpdateUser
{
    public sealed record UpdateUserCommand(
        Guid Id,
        string Email,
        string? ModifiedBy
    ) : IRequest<Result<Guid>>;
}
