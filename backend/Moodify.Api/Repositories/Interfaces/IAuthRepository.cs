using Moodify.Api.Models;

namespace Moodify.Api.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task VarifyAsync(User user);
        Task<bool> SaveChangesAsync();
    }
} 