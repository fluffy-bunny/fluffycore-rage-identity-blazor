using BlazorOIDCFlow.Contracts;
using BlazorOIDCFlow.Data;
using System.Net.Http.Json;
using Microsoft.JSInterop;

namespace BlazorOIDCFlow.Services
{
    public class ProdRageApiService : IRageApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public ProdRageApiService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;

        }
        public async Task<Manifest?> GetManifestAsync()
        {
            var csrfToken = await GetCSRFAsync();
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/manifest");
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("X-Csrf-Token", csrfToken);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Manifest?>();

        }
        private async Task<string> GetCSRFAsync()
        {
            return await _jsRuntime.InvokeAsync<string>("getCSRF");
        }
    }
}
