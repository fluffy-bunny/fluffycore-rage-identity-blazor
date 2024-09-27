using BlazorOIDCFlow.Contracts;
using BlazorOIDCFlow.Data;
using System.Net;
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
        public async Task<ResponseWrapper<Manifest?>?> GetManifestAsync()
        {
            var respone = await _httpClient.GetFromJsonAsync<Manifest?>("sample-data/manifest.json");
            return new ResponseWrapper<Manifest?>
            {
                Response = respone,
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<ResponseWrapper<LoginPasswordResponse?>?> LoginPasswordAsync(LoginPasswordRequest request)
        {
            if (request.Email == "ghstahl@gmail.com")
            {
                // Add your logic here
                var response = await _httpClient.GetFromJsonAsync<LoginPasswordResponse?>("sample-data/login-password-response.json");
                return new ResponseWrapper<LoginPasswordResponse?>
                {
                    Response = response,
                    StatusCode = HttpStatusCode.OK
                };
            }
            return null;
        }

        public async Task<ResponseWrapper<LoginPhaseOneResponse?>?> LoginPhaseOneAsync(LoginPhaseOneRequest request)
        {
            if (request.Email == "ghstahl@gmail.com")
            {
                return new ResponseWrapper<LoginPhaseOneResponse?>
                {
                    Response = new LoginPhaseOneResponse
                    {
                        Directive = "directiveDisplayPasswordPage",
                        DirectiveDisplayPasswordPage = new DirectiveDisplayPasswordPage
                        {
                            Email = request.Email,
                            HasPasskey = true
                        }
                    },
                    StatusCode = HttpStatusCode.OK
                };


            }
            if (request.Email.Contains("@mapped.com"))
            {
                // Add your logic here
                var value = await _httpClient.GetFromJsonAsync<LoginPhaseOneResponse?>("sample-data/login-phase-one-mapped-response.json");
                return new ResponseWrapper<LoginPhaseOneResponse?>
                {
                    Response = value,
                    StatusCode = HttpStatusCode.OK
                };
            }
            return null;
        }

        public async Task<ResponseWrapper<PasswordResetFinishResponse?>?> PasswordResetFinishAsync(PasswordResetFinishRequest request)
        {
            if (request.Password == request.PasswordConfirm)
            {
                return new ResponseWrapper<PasswordResetFinishResponse?>
                {
                    Response = new PasswordResetFinishResponse
                    {
                        ErrorReason = PasswordResetErrorReason.PasswordResetErrorReason_NoError
                    },
                    StatusCode = HttpStatusCode.OK
                };
            }
            return new ResponseWrapper<PasswordResetFinishResponse?>
            {
                Response = new PasswordResetFinishResponse
                {
                    ErrorReason = PasswordResetErrorReason.PasswordResetErrorReason_InvalidPassword
                },
                StatusCode = HttpStatusCode.BadRequest
            };

        }

        public async Task<ResponseWrapper<PasswordResetStartResponse?>?> PasswordResetStartAsync(PasswordResetStartRequest request)
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
            return new ResponseWrapper<PasswordResetStartResponse?>
            {
                Response = response,
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<ResponseWrapper<SignupResponse?>?> SignupRequestAsync(SignupRequest request)
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

            return new ResponseWrapper<SignupResponse?>
            {
                Response = response,
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<ResponseWrapper<StartExternalLoginResponse?>?> StartExternalLoginAsync(StartExternalLoginRequest request)
        {
            var response = await _httpClient.GetFromJsonAsync<StartExternalLoginResponse?>("sample-data/start-external-login-response.json");
            return new ResponseWrapper<StartExternalLoginResponse?>
            {
                Response = response,
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<ResponseWrapper<VerifyCodeResponse?>?> VerifyCodeAsync(VerifyCodeRequest request)
        {
            if (request.Code == "1234")
            {
                var response = await _httpClient.GetFromJsonAsync<VerifyCodeResponse?>("sample-data/verify-code-response.json");
                return new ResponseWrapper<VerifyCodeResponse?>
                {
                    Response = response,
                    StatusCode = HttpStatusCode.OK
                };
            }
            return null;
        }

        public async Task<ResponseWrapper<VerifyPasswordStringResponse?>?> VerifyPasswordStrengthAsync(VerifyPasswordStrengthRequest request)
        {
            var response = await _httpClient.GetFromJsonAsync<VerifyPasswordStringResponse?>("sample-data/verify-password-strength-response.json");
            return new ResponseWrapper<VerifyPasswordStringResponse?>
            {
                Response = response,
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<ResponseWrapper<VerifyUsernameResponse?>?> VerifyUsernameAsync(VerifyUsernameRequest request)
        {
            if (request.UserName == "ghstahl@gmail.com")
            {
                // Add your logic here
                var response = await _httpClient.GetFromJsonAsync<VerifyUsernameResponse?>("sample-data/verify-username-response.json");
                return new ResponseWrapper<VerifyUsernameResponse?>
                {
                    Response = response,
                    StatusCode = HttpStatusCode.OK
                };

            }
            // return 404
            return null;
        }
    }
}
