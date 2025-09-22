using System;
using System.Collections.Generic;

namespace Moodify.Api.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;

        public string NormalisedUsername { get; set; } = null!;
        
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Salt { get; set;  } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime LastModifiedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Playlist> Playlists { get; set; } = [];

    }
}
