using System;
using Moodify.Api.Models;

namespace Moodify.Api.Dtos
{
    public class PlaylistTrackDto
    {
        public Guid TrackId { get; set; }
        public string TrackName { get; set; } = null!;
        public string Artist { get; set; } = null!;
        public string SpotifyTrackId { get; set; } = null!;
        public string AlbumArtUrl { get; set; } = null!;
    }


}