using System;
using System.Collections.Generic;

namespace Moodify.Api.Dtos
{
    public class PlaylistSummaryDto
    {
        public Guid Id { get; set; }
        public string Mood { get; set; } = null!;
        public string PlaylistName { get; set; } = null!;
        public string Description { get; set; } = null!;

    }
}