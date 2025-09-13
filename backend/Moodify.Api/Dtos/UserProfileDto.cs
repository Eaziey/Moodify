using System;
using Moodify.Api.Models;

namespace Moodify.Api.Dtos
{
    public class UserProfileDto
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }

        public string Email { get; set; } = null!;

        public ICollection<PlaylistSummaryDto> Playlists { get; set; } = new List<PlaylistSummaryDto>();
    }
}