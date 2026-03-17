using acciovac.Application.Common;
using MediatR;
using System;

namespace acciovac.Application.Behaviors.Users.Commands.DeleteUser
{
    public sealed record DeleteUserCommand(Guid Id, string? ModifiedBy) : IRequest<Result<Guid>>;
}
