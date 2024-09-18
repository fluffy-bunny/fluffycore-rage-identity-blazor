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

        public async Task<LoginPasswordResponse?> LoginPasswordAsync(LoginPasswordRequest request)
        {
            if (request.Email == "ghstahl@gmail.com")
            {
                // Add your logic here
                return await _httpClient.GetFromJsonAsync<LoginPasswordResponse?>("sample-data/login-password-response.json");
            }
            return null;
        }

        public async Task<LoginPhaseOneResponse?> LoginPhaseOneAsync(LoginPhaseOneRequest request)
        {
            if (request.Email == "ghstahl@gmail.com")
            {
                // Add your logic here
                return await _httpClient.GetFromJsonAsync<LoginPhaseOneResponse?>("sample-data/login-phase-one-response.json");
            }
            if (request.Email.Contains("@mapped.com"))
            {
                // Add your logic here
                return await _httpClient.GetFromJsonAsync<LoginPhaseOneResponse?>("sample-data/login-phase-one-mapped-response.json");
            }
            return null;
        }

        public async Task<StartExternalIDPLoginResponse?> StartExternalIDPLoginAsync(StartExternalIDPLoginRequest request)
        {
            return await _httpClient.GetFromJsonAsync<StartExternalIDPLoginResponse?>("sample-data/start-external-login-response.json");
        
        }

        public async Task<VerifyCodeResponse?> VerifyCodeAsync(VerifyCodeRequest request)
        {
            if (request.Code == "1234")
            {
                return await _httpClient.GetFromJsonAsync<VerifyCodeResponse?>("sample-data/verify-code-response.json");

            }
            return null;
        }

        public async Task<VerifyPasswordStringResponse?> VerifyPasswordStrengthAsync(VerifyPasswordStrengthRequest request)
        {
            return await _httpClient.GetFromJsonAsync<VerifyPasswordStringResponse?>("sample-data/verify-password-strength-response.json");
        }

        public async Task<VerifyUsernameResponse?> VerifyUsernameAsync(VerifyUsernameRequest request)
        {
            if (request.UserName == "ghstahl@gmail.com")
            {
                // Add your logic here
                return await _httpClient.GetFromJsonAsync<VerifyUsernameResponse?>("sample-data/verify-username-response.json");

            }
            // return 404
            return null;
        }
    }
}
