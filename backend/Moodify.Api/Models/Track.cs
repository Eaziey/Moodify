using System;
using System.Collections.Generic;

namespace Moodify.Api.Models
{
    public class Track
    {
        public Guid Id { get; set; }

        public string TrackName { get; set; } = null!;
        public string Artist { get; set; } = null!;
        public string SportifyTrackId { get; set; } = null!;

        public string AlbumArtUrl { get; set; } = null!;

        public ICollection<PlaylistTrack> PlaylistTracks { get; set; } = new List<PlaylistTrack>();
    }
}