using Microsoft.AspNetCore.Mvc;
using Moodify.Api.Dtos;

namespace Moodify.Api.Services.IServices{
    public interface ISpotifyAuthService
    {
        String SpotifyLogin();

        Task<SpotifyLoginResultDTO> SpotifyCallBack(string code);
        
    }
}