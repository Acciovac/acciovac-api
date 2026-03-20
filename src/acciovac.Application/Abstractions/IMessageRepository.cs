using acciovac.Domain.Entities;

namespace acciovac.Application.Abstractions
{
    public interface IMessageRepository
    {
        Task<IReadOnlyList<Message>> GetByUserIdAsync(Guid userId);
        Task<Message?> GetByIdAsync(Guid id);
        Task AddAsync(Message message);
        Task UpdateAsync(Message message);
        Task DeleteAsync(Message message);
    }
}
