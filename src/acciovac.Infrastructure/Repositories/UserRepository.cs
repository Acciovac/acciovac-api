using acciovac.Application.Abstractions;
using acciovac.Domain.Entities;
using acciovac.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace acciovac.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.Roles)
                    .ThenInclude(ur => ur.Role)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .Include(u => u.Roles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByFirebaseUidAsync(string firebaseUid)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.FirebaseUid == firebaseUid);
        }

        public async Task AddAsync(User user, string roleName, string createdBy)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName)
                ?? throw new InvalidOperationException($"Role '{roleName}' does not exist.");

            await _context.Users.AddAsync(user);
            await _context.UserRoles.AddAsync(new UserRole(user.Id, role.Id, createdBy));
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
