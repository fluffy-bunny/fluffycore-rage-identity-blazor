
using BlazorAccountManagement.Contracts;
using BlazorAccountManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net;
using System.Text.Json;

namespace BlazorAccountManagement.Services
{
    public class ProdRageApiService : IRageApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly string? _baseApiUrl;
        private readonly NavigationManager _navigationManager;
        private readonly AppSettings _appSettings;

        public ProdRageApiService(AppSettings appSettings, HttpClient httpClient, NavigationManager navigationManager, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _appSettings = appSettings;
            _navigationManager = navigationManager;
            _baseApiUrl = appSettings.BaseApiUrl;
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
                    if (wrappedResponse.StatusCode ==  HttpStatusCode.Unauthorized)
                    {
                        Console.WriteLine($"Unauthorized access. Redirecting to home page { _appSettings.UnauthorizedRedirectUrl }.");
                       _navigationManager.NavigateTo(_appSettings.UnauthorizedRedirectUrl);
                      //  return null;
                    }
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
                    if (wrappedResponse.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        Console.WriteLine($"Unauthorized access. Redirecting to home page {_appSettings.UnauthorizedRedirectUrl}.");
                        //			redirectUrl := fmt.Sprintf("%s?returnUrl=%s", wellknown_echo.LoginPath, path)

                        _navigationManager.NavigateTo(_appSettings.UnauthorizedRedirectUrl);
                     }
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
        

        public async Task<ResponseWrapper<EmptyResponse?>?> GetIsAuthorizedAsync()
        {
            return await GetAsync<EmptyResponse>("/api/is-authorized");
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
        public async Task<ResponseWrapper<VerifyCodeResponse?>?> VerifyCodeAsync(VerifyCodeRequest request)
        {
            return await PostAsync<VerifyCodeRequest, VerifyCodeResponse>("/api/verify-code", request);
        }
    }
}
