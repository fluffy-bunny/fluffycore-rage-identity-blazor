using BlazorOIDCFlow.Data;

namespace BlazorOIDCFlow.Contracts
{
    public interface IRageApiService
    {
        public Task<Manifest?> GetManifestAsync();
        public Task<StartExternalLoginResponse?> StartExternalLoginAsync(StartExternalLoginRequest request);

    }
}
