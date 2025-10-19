using Moodify.Api.Dtos;
using Moodify.Api.Models;

namespace Moodify.Api.Services.IServices{

    public interface IAuthService
    {

        Task<UserLoginReturnDto> AddUserAsync(UserRegistrationDto user);
        
        Task<string> VarifyUserLoginDetailsAsync(UserLoginDto user);
        User NormaliseUserDetails(User user);
        string SaltAndHashPassword(string password, out byte[] salt);

        bool VarifyPassword(string password, string hash, byte[] salt);

        Task<bool> UpdateSpotifyDetailsAsync(Guid Id, string? SpotifyId = null, string? SpotifyEmail = null, string? SpotifyDisplayName = null);
        Task<string> GetUserTokenBySpotifyIdAsync(string SpotifyId);

    
    }
}