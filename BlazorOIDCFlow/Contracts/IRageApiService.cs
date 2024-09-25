using BlazorOIDCFlow.Data;

namespace BlazorOIDCFlow.Contracts
{
    public interface IRageApiService
    {
        public Task<Manifest?> GetManifestAsync();

        public Task<LoginPasswordResponse?> LoginPasswordAsync(LoginPasswordRequest request);

        public Task<ResponseWrapper<LoginPhaseOneResponse?>?> LoginPhaseOneAsync(LoginPhaseOneRequest request);

        public Task<PasswordResetStartResponse?> PasswordResetStartAsync(PasswordResetStartRequest request);
        public Task<PasswordResetFinishResponse?> PasswordResetFinishAsync(PasswordResetFinishRequest request);


        public Task<StartExternalLoginResponse?> StartExternalLoginAsync(StartExternalLoginRequest request);

        public Task<SignupResponse?> SignupRequestAsync(SignupRequest request);


        public Task<VerifyCodeResponse?> VerifyCodeAsync(VerifyCodeRequest request);
        public Task<VerifyPasswordStringResponse?> VerifyPasswordStrengthAsync(VerifyPasswordStrengthRequest request);

        public Task<VerifyUsernameResponse?> VerifyUsernameAsync(VerifyUsernameRequest request);

    }
}
