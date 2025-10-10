using Newtonsoft.Json;

namespace Moodify.Api.Models
{
    
    public class SpotifyPlaylist
    {
        [JsonProperty("id")]
        public string Id { get; set; } = null!;

        [JsonProperty("name")]
        public string Name { get; set; } = null!;

        [JsonProperty("snapshot_id")]
        public string? SnapshotId { get; set; } = null;

        //[JsonProperty("owner")]
        //public SpotifyUser Owner { get; set; }
    }

}