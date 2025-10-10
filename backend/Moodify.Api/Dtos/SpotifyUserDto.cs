using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Moodify.Api.Dtos
{
    public class SpotifyUserDto
    {
        [JsonPropertyName("display_name")]
        public string? DisplayName { get; set; } = null;

        [JsonPropertyName("email")]
        public string? Email { get; set; } = null;

        [JsonPropertyName("external_urls")]
        public Dictionary<string, string>? External_urls { get; set; } = null;

        [JsonPropertyName("href")]
        public string? Href { get; set; } = null;

        [JsonPropertyName("id")]
        public string? SpotifyId { get; set; } = null;

        /*[JsonPropertyName("images")]
        public Dictionary<string, string>? Images { get; set; } = null;*/
    }
}