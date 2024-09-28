using BlazorOIDCFlow.Data;

namespace BlazorOIDCFlow.Contracts
{
    public interface IRageApiService
    {
        public Task<ResponseWrapper<Manifest?>?> GetManifestAsync();
        public Task<ResponseWrapper<Manifest?>?> StartOverAsync();

        public Task<ResponseWrapper<VerifyCodeBeginResponse?>?> GetVerifyCodeBeginAsync();

        public Task<ResponseWrapper<LoginPhaseOneResponse?>?> LoginPhaseOneAsync(LoginPhaseOneRequest request);

        public Task<ResponseWrapper<LoginPasswordResponse?>?> LoginPasswordAsync(LoginPasswordRequest request);


        public Task<ResponseWrapper<PasswordResetStartResponse?>?> PasswordResetStartAsync(PasswordResetStartRequest request);
        public Task<ResponseWrapper<PasswordResetFinishResponse?>?> PasswordResetFinishAsync(PasswordResetFinishRequest request);


        public Task<ResponseWrapper<StartExternalLoginResponse?>?> StartExternalLoginAsync(StartExternalLoginRequest request);

        public Task<ResponseWrapper<SignupResponse?>?> SignupRequestAsync(SignupRequest request);


        public Task<ResponseWrapper<VerifyCodeResponse?>?> VerifyCodeAsync(VerifyCodeRequest request);
        public Task<ResponseWrapper<VerifyPasswordStringResponse?>?> VerifyPasswordStrengthAsync(VerifyPasswordStrengthRequest request);

        public Task<ResponseWrapper<VerifyUsernameResponse?>?> VerifyUsernameAsync(VerifyUsernameRequest request);

    }
}
