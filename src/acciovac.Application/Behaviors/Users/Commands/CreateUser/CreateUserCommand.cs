using acciovac.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace acciovac.Application.Behaviors.Users.Commands.CreateUser
{
    public sealed record CreateUserCommand(
        string FirebaseUid,
        string Email,
        string? DisplayName,
        string? PhotoUrl,
        string? PhoneNumber
    ) : IRequest<Result<Guid>>;
}
