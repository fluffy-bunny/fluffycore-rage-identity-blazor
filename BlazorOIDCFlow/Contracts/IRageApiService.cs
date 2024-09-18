using BlazorOIDCFlow.Data;

namespace BlazorOIDCFlow.Contracts
{
    public interface IRageApiService
    {
        public Task<Manifest?> GetManifestAsync();
        public Task<VerifyUsernameResponse?> VerifyUsernameAsync(VerifyUsernameRequest request);
        public Task<VerifyPasswordStringResponse?> VerifyPasswordStrengthAsync(VerifyPasswordStrengthRequest request);
        public Task<LoginPhaseOneResponse?> LoginPhaseOneAsync(LoginPhaseOneRequest request);
        public Task<StartExternalIDPLoginResponse?> StartExternalIDPLoginAsync(StartExternalIDPLoginRequest request);
        public Task<LoginPasswordResponse?> LoginPasswordAsync(LoginPasswordRequest request);
        public Task<VerifyCodeResponse?> VerifyCodeAsync(VerifyCodeRequest request);

    }
}
