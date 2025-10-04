namespace Moodify.Api.Dtos
{
    
    public class SpotifyLoginResultDTO
    {
        public bool Success { get; set; }
        public required string Message { get; set; }
        public SpotifyTokenResponseDTO? Token { get; set; }
    }

}