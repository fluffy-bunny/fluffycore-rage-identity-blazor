using BlazorOIDCFlow.Contracts;
using BlazorOIDCFlow.Data;
using Microsoft.JSInterop;
using System.Text.Json;

namespace BlazorOIDCFlow.Services
{
    public class ProdRageApiService : IRageApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly string? _baseApiUrl;
        private readonly AppSettings _appSettings;

        public ProdRageApiService( AppSettings appSettings,HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _appSettings = appSettings;

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

        public async Task StoreLoginRecord(LoginRecord request)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("storeLoginRecord", request);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
        public async Task<List<LoginRecord>> FetchLoginRecordsAsync()
        {
            var records = await _jsRuntime.InvokeAsync<List<LoginRecord>>("fetchLoginRecords");
            return records ?? new List<LoginRecord>();
        }

        public async Task<ResponseWrapper<Manifest?>?> GetManifestAsync()
        {
            return await GetAsync<Manifest>("/api/manifest");
        }
        public async Task<ResponseWrapper<LoginPasswordResponse?>?> LoginPasswordAsync(LoginPasswordRequest request)
        {
            return await PostAsync<LoginPasswordRequest, LoginPasswordResponse>("/api/login-password", request);
        }

        public async Task<ResponseWrapper<LoginPhaseOneResponse?>?> LoginPhaseOneAsync(LoginPhaseOneRequest request)
        {
            return await PostAsync<LoginPhaseOneRequest, LoginPhaseOneResponse>("/api/login-phase-one", request);
        }
        public async Task<ResponseWrapper<PasswordResetFinishResponse?>?> PasswordResetFinishAsync(PasswordResetFinishRequest request)
        {
            return await PostAsync<PasswordResetFinishRequest, PasswordResetFinishResponse>("/api/password-reset-finish", request);
        }
        public async Task<ResponseWrapper<PasswordResetStartResponse?>?> PasswordResetStartAsync(PasswordResetStartRequest request)
        {
            return await PostAsync<PasswordResetStartRequest, PasswordResetStartResponse>("/api/password-reset-start", request);
        }
        public async Task<ResponseWrapper<SignupResponse?>?> SignupRequestAsync(SignupRequest request)
        {
            return await PostAsync<SignupRequest, SignupResponse>("/api/signup", request);
        }
        public async Task<ResponseWrapper<StartExternalLoginResponse?>?> StartExternalLoginAsync(StartExternalLoginRequest request)
        {
            return await PostAsync<StartExternalLoginRequest, StartExternalLoginResponse>("/api/start-external-login", request);
        }
        public async Task<ResponseWrapper<VerifyCodeResponse?>?> VerifyCodeAsync(VerifyCodeRequest request)
        {
            return await PostAsync<VerifyCodeRequest, VerifyCodeResponse>("/api/verify-code", request);
        }
        public async Task<ResponseWrapper<VerifyPasswordStringResponse?>?> VerifyPasswordStrengthAsync(VerifyPasswordStrengthRequest request)
        {
            return await PostAsync<VerifyPasswordStrengthRequest, VerifyPasswordStringResponse>("/api/verify-password-strength", request);
        }
        public async Task<ResponseWrapper<VerifyUsernameResponse?>?> VerifyUsernameAsync(VerifyUsernameRequest request)
        {
            return await PostAsync<VerifyUsernameRequest, VerifyUsernameResponse>("/api/verify-username", request);
        }

        private async Task<string> GetCSRFAsync()
        {
            return await _jsRuntime.InvokeAsync<string>("getCSRF");
        }

        public async Task<ResponseWrapper<VerifyCodeBeginResponse?>?> GetVerifyCodeBeginAsync()
        {
            return await GetAsync<VerifyCodeBeginResponse>("/api/verify-code-begin");
        }

        public async Task<ResponseWrapper<Manifest?>?> StartOverAsync()
        {
            return await GetAsync<Manifest>("/api/start-over");
        }
        public async Task<ResponseWrapper<UserProfile?>?> GetUserProfileAsync()
        {
            return await GetAsync<UserProfile>("/api/user-profile");
        }

        public async Task<ResponseWrapper<LoginCurrentUserResponse>> GetLoginCurrentUserAsync()
        {
            return await GetAsync<LoginCurrentUserResponse>("/api/login-current-user");
        }

        public async Task<ResponseWrapper<ValidOIDCSessionResponse>> GetIsValidOIDCSessionAsync()
        {
            return await GetAsync<ValidOIDCSessionResponse>("/api/is-valid-oidc-session");
        }
    }
}


