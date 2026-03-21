using acciovac.Application.Abstractions;
using acciovac.Domain.Entities;
using acciovac.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace acciovac.Infrastructure.Repositories
{
    public class LocalExpirencesRepository : ILocalExpirences
    {
        private readonly AppDbContext _context;

        public LocalExpirencesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LocalExpirences?> GetByIdAsync(Guid id)
        {
            return await _context.LocalExpirences
                .Include(x => x.Photos)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<LocalExpirences>> GetAllAsync()
        {
            return await _context.LocalExpirences
                .Include(x => x.Photos)
                .OrderBy(x => x.expireancename)
                .ToListAsync();
        }

        public async Task AddAsync(LocalExpirences localExpirences)
        {
            await _context.LocalExpirences.AddAsync(localExpirences);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LocalExpirences localExpirences)
        {
            if (_context.Entry(localExpirences).State == EntityState.Detached)
            {
                _context.LocalExpirences.Update(localExpirences);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(LocalExpirences localExpirences)
        {
            _context.LocalExpirences.Remove(localExpirences);
            await _context.SaveChangesAsync();
        }

        public async Task AddPhotoAsync(Guid localExpirencesId, string photoUrl, int displayOrder)
        {
            var photo = new Photo(localExpirencesId, photoUrl, displayOrder);
            await _context.Photos.AddAsync(photo);
            await _context.SaveChangesAsync();
        }
    }
}
