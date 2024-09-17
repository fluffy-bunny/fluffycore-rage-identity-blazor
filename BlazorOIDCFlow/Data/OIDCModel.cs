using System.Text.Json.Serialization;

namespace BlazorOIDCFlow.Data
{
    public enum OIDCPage
    {
        SigninUserName,
        SigninUserNamePassword,
        SignUpUserName,
        SignUpUserNamePassword
    }

    public class OIDCModel
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public Manifest? Manifest { get; set; }
    }

    public class Manifest
    {
        // json is social_idps
        [JsonPropertyName("social_idps")]
        public List<SocialIdp> SocialIdps { get; set; } = new List<SocialIdp>();
        [JsonPropertyName("passkey_enabled")]
        public bool PasskeyEnabled { get; set; } = false;
    }

    public class SocialIdp
    {
        [JsonPropertyName("slug")]
        public string Slug { get; set; } = "";
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
    }

    /*
     {"redirectUri":"https://accounts.google.com/o/oauth2/v2/auth?client_id=1096301616546-edbl612881t7rkpljp3qa3juminskulo.apps.googleusercontent.com&nonce=crkrs3chb636s43a8og0&redirect_uri=http%3A%2F%2Flocalhost%3A9044%2Foauth2%2Fcallback&response_type=code&scope=openid+email+profile&state=crkrs3chb636s43a8ofg"}
    */
    public class StartExternalLoginResponse
    {
        [JsonPropertyName("redirectUri")]
        public string RedirectUri { get; set; } = "";
    }
    public class StartExternalLoginRequest
    {
        public string Slug { get; set; } = "";
        public string Directive { get; set; }
    }
    public class VerifyUsernameRequest
    {
        [JsonPropertyName("userName")]

        public string UserName { get; set; }
    }
    public class VerifyUsernameResponse
    {
        [JsonPropertyName("userName")]

        public string UserName { get; set; }

        [JsonPropertyName("passkeyAvailable")]

        public bool PasskeyAvailable { get; set; }
    }
}
