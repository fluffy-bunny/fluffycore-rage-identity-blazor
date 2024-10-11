
using BlazorAccountManagement.Contracts;
using BlazorAccountManagement.Models;
using System.Net;
using System.Net.Http.Json;

namespace BlazorAccountManagement.Services
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

        public async Task<ResponseWrapper<UserProfile?>?> GetUserProfileAsync()
        {
            return new ResponseWrapper<UserProfile?>
            {
                Response = new UserProfile
                {
                    FamilyName = "Van Halen",
                    GivenName = "Eddie",
                    PhoneNumber = "123-456-7890"
                },
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<ResponseWrapper<UserProfile?>?> PostUserProfileAsync(UserProfile request)
        {
            return new ResponseWrapper<UserProfile?>
            {
                Response = request,
                StatusCode = HttpStatusCode.OK
            };
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

        public async Task<ResponseWrapper<LogoutResponse?>?> PostLogoutAsync(LogoutRequest request)
        {
            return new ResponseWrapper<LogoutResponse?>
            {
                Response = new LogoutResponse
                {
                    Directive = "directiveRedirect",
                    RedirectURL = "/"
                },
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<ResponseWrapper<UserIdentityInfo?>?> GetUserIdentityInfoAsync()
        {
            return new ResponseWrapper<UserIdentityInfo?>
            {
                Response = new UserIdentityInfo
                {
                    Email = "test@test.com"
                 },
                StatusCode = HttpStatusCode.OK
            };
        }
        public async Task<ResponseWrapper<VerifyCodeResponse?>?> VerifyCodeAsync(VerifyCodeRequest request)
        {
            if (request.Code == "1234")
            {
                var response = new VerifyCodeResponse
                {

                };
                return new ResponseWrapper<VerifyCodeResponse?>
                {
                    Response = response,
                    StatusCode = HttpStatusCode.OK
                };
            }
            return null;
        }
    }
}
