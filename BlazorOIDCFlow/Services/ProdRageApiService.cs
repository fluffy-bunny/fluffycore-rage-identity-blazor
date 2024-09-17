using BlazorOIDCFlow.Contracts;
using BlazorOIDCFlow.Data;
using System.Net.Http.Json;
using Microsoft.JSInterop;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;

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

        public async Task<StartExternalLoginResponse?> StartExternalLoginAsync(StartExternalLoginRequest request)
        {
            var csrfToken = await GetCSRFAsync();
            var requestBody = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/start-external-login")
            {
                Content = requestBody
            };

            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpRequestMessage.Headers.Add("X-Csrf-Token", csrfToken);

            var response = await _httpClient.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseData);
                return JsonSerializer.Deserialize<StartExternalLoginResponse>(responseData);
            }
            else
            {
                Console.Error.WriteLine($"Error: {response.ReasonPhrase}");
                return null;
            }
        }

        private async Task<string> GetCSRFAsync()
        {
            return await _jsRuntime.InvokeAsync<string>("getCSRF");
        }
    }
}
