using System;

namespace Moodify.Api.Dtos
{
    
    public class TrackDto
    {
        public Guid Id { get; set; }
        public string TrackName { get; set; } = null!;
        public string Artist { get; set; } = null!;
        public string SpotifyTrackId { get; set; } = null!;
        public string AlbumArtUrl { get; set; } = null!;
    }

}