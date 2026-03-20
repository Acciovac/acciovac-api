using acciovac.Application.Abstractions;
using acciovac.Domain.Entities;
using acciovac.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace acciovac.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _context;

        public MessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Message>> GetAllAsync()
        {
            return await _context.Messages
                .OrderByDescending(x => x.CreatedAt)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Message>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Messages
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Message?> GetByIdAsync(Guid id)
        {
            return await _context.Messages
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Message message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Message message)
        {
            _context.Messages.Update(message);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Message message)
        {
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
        }
    }
}
