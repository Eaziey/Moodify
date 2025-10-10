using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Moodify.Api.Services.IServices;
using Moodify.Api.Dtos;
using System.Text;
using System.Net.Http.Headers;

namespace Moodify.Api.Services {

    public class SpotifyAuthService : ISpotifyAuthService
    {

        private readonly IConfiguration _config;

        public SpotifyAuthService(IConfiguration config){
            _config = config;
        }

        public string GetSpotifyUrl(string state)
        {
            var clientID = _config["Spotify:ClientId"];
            //var clientSecret = _config["Spotify:ClientSecret"];
            var redirectUri = _config["Spotify:RedirectUri"];
            var scopes = "playlist-read-private playlist-read-collaborative user-read-email";

            var spotifyUrl = $"https://accounts.spotify.com/authorize?response_type=code&client_id={clientID}&scope={Uri.EscapeDataString(scopes)}&redirect_uri={Uri.EscapeDataString(redirectUri)}&show_dialog=true&state={state}";

            return spotifyUrl;
            
        }

        public string SpotifyLogin()
        {
            var clientID = _config["Spotify:ClientId"];
            //var clientSecret = _config["Spotify:ClientSecret"];
            var redirectUri = _config["Spotify:RedirectUri"];
            var scopes = "playlist-read-private playlist-read-collaborative user-read-email";

            var spotifyUrl = $"https://accounts.spotify.com/authorize?response_type=code&client_id={clientID}&scope={Uri.EscapeDataString(scopes)}&redirect_uri={Uri.EscapeDataString(redirectUri)}&show_dialog=true";

            return spotifyUrl;

        }

        public async Task<SpotifyLoginResultDTO> SpotifyCallBack(string code)
        {
            var clientId = _config["Spotify:ClientId"];
            var clientSecret = _config["Spotify:ClientSecret"];
            var redirectUri = _config["Spotify:RedirectUri"];

            if (string.IsNullOrEmpty(clientId))
            {
                throw new InvalidOperationException("Spotify client ID is missing.");

            }

            if (string.IsNullOrEmpty(clientSecret))
            {
                throw new InvalidOperationException("Spotify client Secret is missing.");

            }

            if (string.IsNullOrEmpty(redirectUri))
            {
                throw new InvalidOperationException("Spotify redirectURI is missing.");

            }

            var httpClient = new HttpClient();
            
            var requestBody = new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "code", code },
                { "redirect_uri", redirectUri }
            };

            var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeader);

            var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token")
            {
                Content = new FormUrlEncodedContent(requestBody)
            };

            var response = await httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            

            if (!response.IsSuccessStatusCode)
            {
    
                return new SpotifyLoginResultDTO { Success = false, Message = "Spotify login failed", Token = null };
            }

            var tokenData = JsonSerializer.Deserialize<SpotifyTokenResponseDTO>(content) ?? throw new Exception("Spotify login failed. TokenData is null");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenData.AccessToken);

            var userResponse = await httpClient.GetAsync("https://api.spotify.com/v1/me");
            var userContent = await userResponse.Content.ReadAsStringAsync();

            Console.WriteLine($"User Content: {userContent}");

            var userData = JsonSerializer.Deserialize<SpotifyUserDto>(userContent);

            return new SpotifyLoginResultDTO { Success = true, Message = "Spotify login successful", Token = tokenData, SpotifyUserData = userData };
        
        }
    }
}