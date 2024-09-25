using BlazorOIDCFlow.Contracts;
using BlazorOIDCFlow.Data;
using System.Net.Http.Json;

namespace BlazorOIDCFlow.Services
{
    public class LocalRageApiService : IRageApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string? _baseApiUrl;
        public LocalRageApiService(IConfiguration configuration, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _baseApiUrl = _configuration.GetValue<string>("BaseAPIUrl");
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
                return new LoginPhaseOneResponse
                {
                    Directive = "directiveDisplayPasswordPage",
                    DirectiveDisplayPasswordPage = new DirectiveDisplayPasswordPage
                    {
                        Email = request.Email,
                        HasPasskey = false
                    }
                };
                
            }
            if (request.Email.Contains("@mapped.com"))
            {
                // Add your logic here
                return await _httpClient.GetFromJsonAsync<LoginPhaseOneResponse?>("sample-data/login-phase-one-mapped-response.json");
            }
            return null;
        }

        public async Task<PasswordResetFinishResponse?> PasswordResetFinishAsync(PasswordResetFinishRequest request)
        {
            if (request.Password == request.PasswordConfirm)
            {
                return new PasswordResetFinishResponse
                {
                    ErrorReason = PasswordResetErrorReason.PasswordResetErrorReason_NoError
                };
            }
            return new PasswordResetFinishResponse
            {
                ErrorReason = PasswordResetErrorReason.PasswordResetErrorReason_InvalidPassword
            };
        }

        public async Task<PasswordResetStartResponse?> PasswordResetStartAsync(PasswordResetStartRequest request)
        {
            var response = new PasswordResetStartResponse
            {
                Email = request.Email,
                Directive = "directiveEmailCodeChallenge",
                DirectiveEmailCodeChallenge = new DirectiveEmailCodeChallenge
                {
                    Code = "1234"
                }
            };
            return response;
        }

        public async Task<SignupResponse?> SignupRequestAsync(SignupRequest request)
        {
            var response = new SignupResponse
            {
                Email = request.Email,
            };

            if (request.Email == "ghstahl@gmail.com")
            {
                response.Directive = "directiveEmailCodeChallenge";
                response.DirectiveEmailCodeChallenge = new DirectiveEmailCodeChallenge
                {
                    Code = "1234"
                };

            }
            else if (request.Email.Contains("@mapped.com"))
            {
                response.Directive = "directiveRedirect";
                response.DirectiveRedirect = new DirectiveRedirect
                {
                    RedirectURI = "https://www.google.com"
                };
            }
            else
            {
                response.ErrorReason = SignupErrorReason.SignupErrorReason_UserAlreadyExists;
                response.Message = "User already exists";
            }
           
            return response;
        }

        public async Task<StartExternalLoginResponse?> StartExternalLoginAsync(StartExternalLoginRequest request)
        {
            return await _httpClient.GetFromJsonAsync<StartExternalLoginResponse?>("sample-data/start-external-login-response.json");
        
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
