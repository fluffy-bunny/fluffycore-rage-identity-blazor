using BlazorOIDCFlow.Data;

namespace BlazorOIDCFlow.Contracts
{
    public interface IRageApiService
    {
        public Task<Manifest?> GetManifestAsync();
        public Task<StartExternalLoginResponse?> StartExternalLoginAsync(StartExternalLoginRequest request);
        public Task<VerifyUsernameResponse?> VerifyUsernameAsync(VerifyUsernameRequest request);
        public Task<VerifyPasswordStringResponse?> VerifyPasswordStrengthAsync(VerifyPasswordStrengthRequest request);

    }
}
