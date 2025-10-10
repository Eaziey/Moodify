using Microsoft.AspNetCore.Mvc;
using Moodify.Api.Dtos;

namespace Moodify.Api.Services.IServices{
    public interface ISpotifyAuthService
    {
        string SpotifyLogin();
        string GetSpotifyUrl(string state);

        Task<SpotifyLoginResultDTO> SpotifyCallBack(string code);
        
    }
}