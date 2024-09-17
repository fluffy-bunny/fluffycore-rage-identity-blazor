using BlazorOIDCFlow.Contracts;
using BlazorOIDCFlow.Data;
using System.Net.Http.Json;

namespace BlazorOIDCFlow.Services
{
    public class LocalRageApiService : IRageApiService
    {
        private readonly HttpClient _httpClient;

        public LocalRageApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Manifest?> GetManifestAsync()
        {
            return await _httpClient.GetFromJsonAsync<Manifest?>("sample-data/manifest.json");
        }

        public async Task<StartExternalLoginResponse?> StartExternalLoginAsync(StartExternalLoginRequest request)
        {
            return await _httpClient.GetFromJsonAsync<StartExternalLoginResponse?>("sample-data/start-external-login-response.json");
        }
    }
}
