using System;
using System.Collections.Generic;

namespace Moodify.Api.Dtos
{
    public class PlaylistDto
    {
        public Guid Id { get; set; }
        public string Mood { get; set; } = null!;
        public string PlaylistName { get; set; } = null!;

        public string SportifyPlaylistId { get; set; } = null!;

        public Guid UserId { get; set; }

        public string Description { get; set; } = null!;

        public ICollection<PlaylistTrackDto> PlaylistTracks { get; set; } = new List<PlaylistTrackDto>();

    }
}