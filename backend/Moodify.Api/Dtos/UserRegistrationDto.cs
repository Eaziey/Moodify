
namespace Moodify.Api.Dtos
{
    public class UserRegistrationDto
    {
        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool Integrate { get; set; } = true;
    }
}