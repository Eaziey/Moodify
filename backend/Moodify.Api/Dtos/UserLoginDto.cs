

namespace Moodify.Api.Dtos
{
    public class UserLoginDto
    {
        public string? Email { get; set; } = null;

        public string? SpotifyEmail { get; set; } = null;

        public string? Password { get; set; } = null;

        public bool UseSpotify { get; set; } = false;
    }
}