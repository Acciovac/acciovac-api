using System;
using System.Collections.Generic;

namespace acciovac.Application.Behaviors.Users.Queries.GetUserById
{
    public sealed record UserByIdDto(
        Guid Id,
        string FirebaseUid,
        string Email,
        bool IsActive,
        DateTime CreatedAt,
        IReadOnlyList<string> Roles);
}
