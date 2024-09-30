
using BlazorAccountManagement.Contracts;
using BlazorAccountManagement.Models;
using Microsoft.JSInterop;
using System.Net;
using System.Text.Json;

namespace BlazorAccountManagement.Services
{
    public class ProdRageApiService : IRageApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly IConfiguration _configuration;
        private readonly string? _baseApiUrl;

        public ProdRageApiService(IConfiguration configuration, HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _configuration = configuration;
            _baseApiUrl = _configuration.GetValue<string>("BaseAPIUrl");
            if (!string.IsNullOrEmpty(_baseApiUrl))
            {
                _httpClient.BaseAddress = new Uri(_baseApiUrl);
            }
        }
        
        public async Task<ResponseWrapper<TResponse?>?> GetAsync<TResponse>(string path)   where TResponse : class
        {
            try
            {
                var url = _baseApiUrl + path;
                var wrappedResponse = await _jsRuntime.InvokeAsync<ResponseWrapper<TResponse?>?>("sendRequestWithCookies", url, "GET", null);

                if (wrappedResponse != null)
                {
                    Console.WriteLine(JsonSerializer.Serialize(wrappedResponse));
                    return wrappedResponse;
                }
                else
                {
                    Console.Error.WriteLine("Error: Response is null");

                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
        public async Task<ResponseWrapper<TResponse?>?> PostAsync<TRequest, TResponse>(string path, TRequest request) where TRequest : class where TResponse : class
        {
            try
            {
                var url = _baseApiUrl + path;
                var wrappedResponse = await _jsRuntime.InvokeAsync<ResponseWrapper<TResponse?>?>("sendRequestWithCookies", url, "POST", request);

                if (wrappedResponse != null)
                {
                    Console.WriteLine(JsonSerializer.Serialize(wrappedResponse));
                    return wrappedResponse;
                }
                else
                {
                    Console.Error.WriteLine("Error: Response is null");

                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
        public async Task<ResponseWrapper<UserProfile?>?> GetUserProfileAsync()
        {
            return await GetAsync<UserProfile>("/api/user-profile");
        }
        public async Task<ResponseWrapper<UserProfile?>?> PostUserProfileAsync(UserProfile request)
        {
            return await PostAsync<UserProfile, UserProfile>("/api/user-profile", request);
        }
        public async Task<ResponseWrapper<LogoutResponse?>?> PostLogoutAsync(LogoutRequest request)
        {
            return await PostAsync<LogoutRequest, LogoutResponse>("/api/logout", request);
        }

        public async Task<ResponseWrapper<PasswordResetFinishResponse?>?> PasswordResetFinishAsync(PasswordResetFinishRequest request)
        {
            return await PostAsync<PasswordResetFinishRequest, PasswordResetFinishResponse>("/api/password-reset-finish", request);
        }
        public async Task<ResponseWrapper<PasswordResetStartResponse?>?> PasswordResetStartAsync(PasswordResetStartRequest request)
        {
            return await PostAsync<PasswordResetStartRequest, PasswordResetStartResponse>("/api/password-reset-start", request);
        }
        public async Task<ResponseWrapper<UserIdentityInfo?>?> GetUserIdentityInfoAsync()
        {
            return await GetAsync<UserIdentityInfo>("/api/user-identity-info");

        }

    }
}
