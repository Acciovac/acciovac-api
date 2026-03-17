using acciovac.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace acciovac.Application.Abstractions
{
    public interface IUserRepository
    {
        Task<IReadOnlyList<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByFirebaseUidAsync(string firebaseUid);
        Task AddAsync(User user, string role, string createdBy);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}
