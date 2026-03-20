using acciovac.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace acciovac.Application.Abstractions
{
    public interface IMessageRepository
    {
        Task<Message?> GetByIdAsync(Guid id);
        Task<IEnumerable<Message>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<Message>> GetAllAsync();
        Task AddAsync(Message message);
        Task UpdateAsync(Message message);
        Task DeleteAsync(Guid id);
    }
}
