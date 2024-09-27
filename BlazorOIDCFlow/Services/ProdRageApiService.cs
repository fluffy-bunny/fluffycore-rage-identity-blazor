﻿using BlazorOIDCFlow.Contracts;
using BlazorOIDCFlow.Data;
using Microsoft.JSInterop;
using System.Text.Json;

namespace BlazorOIDCFlow.Services
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
        public async Task<ResponseWrapper<Manifest?>?> GetManifestAsync()
        {
            try
            {
                var url = _baseApiUrl + "/api/manifest";
                var wrappedResponse = await _jsRuntime.InvokeAsync<ResponseWrapper<Manifest?>?>("sendRequestWithCookies", url, "GET", null);

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
    }
}
