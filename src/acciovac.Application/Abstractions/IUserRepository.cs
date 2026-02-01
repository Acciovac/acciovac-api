using acciovac.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace acciovac.Application.Abstractions
{
    public interface IUserRepository
    {
        Task<User?> GetByFirebaseUidAsync(string firebaseUid);
        Task AddAsync(User user);
    }
}
