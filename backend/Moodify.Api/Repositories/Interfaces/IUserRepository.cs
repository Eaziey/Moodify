using Moodify.Api.Models;

namespace Moodify.Api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task<bool> SaveChangesAsync();
    }
} 