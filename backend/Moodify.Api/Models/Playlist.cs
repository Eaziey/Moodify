using System;
using System.Collections.Generic;

namespace Moodify.Api.Models
{
    public class Playlist
    {
        public Guid Id { get; set; }
        public string Mood { get; set; } = null!;
        public string PlaylistName { get; set; } = null!;

        public string SportifyPlaylistId { get; set; } = null!;

        public Guid? UserId { get; set; }
        public User? User{ get; set; } 

        public ICollection<Track> Tracks { get; set; } = new List<Track>();



    }
}