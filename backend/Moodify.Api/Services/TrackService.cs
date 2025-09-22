using Moodify.Api.Models;
using Moodify.Api.Services.IServices;
using Moodify.Api.Repositories.Interfaces;

namespace Moodify.Api.Services
{
    public class TrackService : ITrackService
    {
        private readonly ITrackRepository _repository;

        public TrackService(ITrackRepository repository){

            _repository = repository;
        
        }
        public async Task<IEnumerable<Track>> GetAllTracksAsync()
        {
            var tracks = await _repository.GetAllAsync();

            return tracks;
        }
        public async Task<Track?> GetTrackByIdAsync(Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                return null;
            }

            return await _repository.GetByIdAsync(id);
        }
        
    }
} 