using System;
using System.Collections.Generic;

namespace acciovac.Application.Behaviors.Users.Queries.GetAllUsers
{
    public sealed record UserDto(
        Guid Id,
        string FirebaseUid,
        string Email,
        bool IsActive,
        DateTime CreatedAt,
        IReadOnlyList<string> Roles);
}
