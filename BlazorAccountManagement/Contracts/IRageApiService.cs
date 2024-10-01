

using BlazorAccountManagement.Models;

namespace BlazorAccountManagement.Contracts
{
    public interface IRageApiService
    {
        public Task<ResponseWrapper<LogoutResponse?>?> PostLogoutAsync(LogoutRequest request);
        public Task<ResponseWrapper<UserProfile?>?> GetUserProfileAsync();
        public Task<ResponseWrapper<UserIdentityInfo?>?> GetUserIdentityInfoAsync();

        public Task<ResponseWrapper<UserProfile?>?> PostUserProfileAsync(UserProfile request);

        public Task<ResponseWrapper<PasswordResetStartResponse?>?> PasswordResetStartAsync(PasswordResetStartRequest request);
        public Task<ResponseWrapper<PasswordResetFinishResponse?>?> PasswordResetFinishAsync(PasswordResetFinishRequest request);
        public Task<ResponseWrapper<VerifyCodeResponse?>?> VerifyCodeAsync(VerifyCodeRequest request);
    }
}
