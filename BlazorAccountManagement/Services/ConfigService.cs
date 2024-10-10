using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Text.Json;

namespace BlazorAccountManagement.Services
{

    public class ConfigService
    {
        private readonly HttpClient _httpClient;

        public ConfigService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAppSettingsAsync()
        {
            try
            {
                // First, try to get the settings from the API
                var apiResponse = await _httpClient.GetAsync("/api/appsettings");

                if (apiResponse.IsSuccessStatusCode)
                {
                    var content = await apiResponse.Content.ReadAsStringAsync();

                    // Check if the content is valid JSON
                    if (IsValidJson(content))
                    {
                        return content;
                    }
                    else
                    {
                        Console.WriteLine("API returned non-JSON content. Falling back to local file.");
                    }
                }
                else
                {
                    Console.WriteLine($"API request failed with status code: {apiResponse.StatusCode}. Falling back to local file.");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Failed to fetch settings from API. Error: {ex.Message}. Falling back to local file.");
            }

            // If API request failed or returned invalid content, try the local file
            try
            {
                var localResponse = await _httpClient.GetAsync("appsettings.json");
                localResponse.EnsureSuccessStatusCode();
                var content = await localResponse.Content.ReadAsStringAsync();

                if (IsValidJson(content))
                {
                    return content;
                }
                else
                {
                    throw new InvalidOperationException("Local appsettings.json is not valid JSON.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to fetch settings from local file. Error: {ex.Message}");
                throw;
            }
        }

        private bool IsValidJson(string content)
        {
            try
            {
                using (JsonDocument.Parse(content))
                {
                    return true;
                }
            }
            catch (JsonException)
            {
                return false;
            }
        }
    }
}
