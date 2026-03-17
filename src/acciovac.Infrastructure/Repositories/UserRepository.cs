using acciovac.Application.Abstractions;
using acciovac.Domain.Entities;
using acciovac.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace acciovac.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
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
    }
}
