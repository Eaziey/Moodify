using Moodify.Api.Models;

namespace Moodify.Api.Services.IServices
{
    public interface IUserService
    {
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> AddUserAsync(User user);
    }
} 