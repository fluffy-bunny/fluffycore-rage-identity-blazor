using BlazorOIDCFlow.Contracts;
using BlazorOIDCFlow.Data;
using System.Net.Http.Json;
using Microsoft.JSInterop;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;
using System.Net.Http;

namespace BlazorOIDCFlow.Services
{
    public class ProdRageApiService : IRageApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly IConfiguration _configuration;
        private readonly string? _baseApiUrl;

        public ProdRageApiService(IConfiguration configuration,HttpClient httpClient, IJSRuntime jsRuntime)
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
        public async Task<Manifest?> GetManifestAsync()
        {
            var csrfToken = await GetCSRFAsync();
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/manifest");
            var allCookies = await _jsRuntime.InvokeAsync<string>("getAllCookies");
            httpRequestMessage.Headers.Add("Cookie", allCookies);

            httpRequestMessage.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            if (csrfToken != null)
            {
                httpRequestMessage.Headers.Add("X-Csrf-Token", csrfToken);
            }
            try
            {
                var response = await _httpClient.SendAsync(httpRequestMessage);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<Manifest?>();
            }

            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
                return null;
            }



        }

        public async Task<LoginPasswordResponse?> LoginPasswordAsync(LoginPasswordRequest request)
        {
            var csrfToken = await GetCSRFAsync();
            var requestBody = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/login-password")
            {
                Content = requestBody
            };
            var allCookies = await _jsRuntime.InvokeAsync<string>("getAllCookies");
            httpRequestMessage.Headers.Add("Cookie", allCookies);

            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpRequestMessage.Headers.Add("X-Csrf-Token", csrfToken);

            var response = await _httpClient.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseData);
                return JsonSerializer.Deserialize<LoginPasswordResponse>(responseData);
            }
            else
            {
                Console.Error.WriteLine($"Error: {response.ReasonPhrase}");
                return null;
            }
        }

        public async Task<LoginPhaseOneResponse?> LoginPhaseOneAsync(LoginPhaseOneRequest request)
        {
            try
            {
                var csrfToken = await GetCSRFAsync();
               
                var url = _baseApiUrl + "/api/login-phase-one";
                var response = await _jsRuntime.InvokeAsync<LoginPhaseOneResponse>("sendRequestWithCookies", url, "POST",  request);

                if (response != null)
                {
                    Console.WriteLine(JsonSerializer.Serialize(response));
                    return response;
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

        public async Task<PasswordResetFinishResponse?> PasswordResetFinishAsync(PasswordResetFinishRequest request)
        {
            var csrfToken = await GetCSRFAsync();
            var requestBody = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/password-reset-finish")
            {
                Content = requestBody
            };
            var allCookies = await _jsRuntime.InvokeAsync<string>("getAllCookies");
            httpRequestMessage.Headers.Add("Cookie", allCookies);

            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpRequestMessage.Headers.Add("X-Csrf-Token", csrfToken);

            var response = await _httpClient.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseData);
                return JsonSerializer.Deserialize<PasswordResetFinishResponse>(responseData);
            }
            else
            {
                Console.Error.WriteLine($"Error: {response.ReasonPhrase}");
                return null;
            }
        }

        public async Task<PasswordResetStartResponse?> PasswordResetStartAsync(PasswordResetStartRequest request)
        {
            var csrfToken = await GetCSRFAsync();
            var requestBody = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/password-reset-start")
            {
                Content = requestBody
            };
            var allCookies = await _jsRuntime.InvokeAsync<string>("getAllCookies");
            httpRequestMessage.Headers.Add("Cookie", allCookies);

            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpRequestMessage.Headers.Add("X-Csrf-Token", csrfToken);

            var response = await _httpClient.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseData);
                return JsonSerializer.Deserialize<PasswordResetStartResponse>(responseData);
            }
            else
            {
                Console.Error.WriteLine($"Error: {response.ReasonPhrase}");
                return null;
            }
        }

        public async Task<SignupResponse?> SignupRequestAsync(SignupRequest request)
        {
            var csrfToken = await GetCSRFAsync();
            var requestBody = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/signup")
            {
                Content = requestBody
            };
            var allCookies = await _jsRuntime.InvokeAsync<string>("getAllCookies");
            httpRequestMessage.Headers.Add("Cookie", allCookies);


            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpRequestMessage.Headers.Add("X-Csrf-Token", csrfToken);

            var response = await _httpClient.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseData);
                return JsonSerializer.Deserialize<SignupResponse>(responseData);
            }
            else
            {
                Console.Error.WriteLine($"Error: {response.ReasonPhrase}");
                return null;
            }
        }

        public async Task<StartExternalLoginResponse?> StartExternalLoginAsync(StartExternalLoginRequest request)
        {
            var csrfToken = await GetCSRFAsync();
            var requestBody = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/start-external-login")
            {
                Content = requestBody
            };
            var allCookies = await _jsRuntime.InvokeAsync<string>("getAllCookies");
            httpRequestMessage.Headers.Add("Cookie", allCookies);


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

        public async Task<VerifyCodeResponse?> VerifyCodeAsync(VerifyCodeRequest request)
        {
            var csrfToken = await GetCSRFAsync();
            var requestBody = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/verify-code")
            {
                Content = requestBody
            };
            var allCookies = await _jsRuntime.InvokeAsync<string>("getAllCookies");
            httpRequestMessage.Headers.Add("Cookie", allCookies);


            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpRequestMessage.Headers.Add("X-Csrf-Token", csrfToken);

            var response = await _httpClient.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseData);
                return JsonSerializer.Deserialize<VerifyCodeResponse>(responseData);
            }
            else
            {
                Console.Error.WriteLine($"Error: {response.ReasonPhrase}");
                return null;
            }
        }

        public async Task<VerifyPasswordStringResponse?> VerifyPasswordStrengthAsync(VerifyPasswordStrengthRequest request)
        {
            var csrfToken = await GetCSRFAsync();
            var requestBody = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/verify-password-strength")
            {
                Content = requestBody
            };
            var allCookies = await _jsRuntime.InvokeAsync<string>("getAllCookies");
            httpRequestMessage.Headers.Add("Cookie", allCookies);


            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpRequestMessage.Headers.Add("X-Csrf-Token", csrfToken);

            var response = await _httpClient.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseData);
                return JsonSerializer.Deserialize<VerifyPasswordStringResponse>(responseData);
            }
            else
            {
                Console.Error.WriteLine($"Error: {response.ReasonPhrase}");
                return null;
            }
        }

        public async Task<VerifyUsernameResponse?> VerifyUsernameAsync(VerifyUsernameRequest request)
        {
            var csrfToken = await GetCSRFAsync();
            var requestBody = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/verify-username")
            {
                Content = requestBody
            };
            var allCookies = await _jsRuntime.InvokeAsync<string>("getAllCookies");
            httpRequestMessage.Headers.Add("Cookie", allCookies);


            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpRequestMessage.Headers.Add("X-Csrf-Token", csrfToken);

            var response = await _httpClient.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseData);
                return JsonSerializer.Deserialize<VerifyUsernameResponse>(responseData);
            }
            else
            {
                Console.Error.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return null;
            }
        }

        private async Task<string> GetCSRFAsync()
        {
            return await _jsRuntime.InvokeAsync<string>("getCSRF");
        }
    }
}
