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
    public class VerifyPasswordStrengthRequest
    {
        [JsonPropertyName("password")]

        public string Password { get; set; }
    }
    public class VerifyPasswordStringResponse
    {
        [JsonPropertyName("valid")]

        public bool Valid { get; set; }
    }

    public class LoginPhaseOneRequest  {
        [JsonPropertyName("email")]
        public string Email { get; set; }

	}
    public class LoginPhaseOneResponse
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("directive")]
        public string Directive { get; set; }

        [JsonPropertyName("directiveDisplayPasswordPage")]
        public DirectiveDisplayPasswordPage? DirectiveDisplayPasswordPage { get; set; }

        [JsonPropertyName("directiveEmailCodeChallenge")]
        public DirectiveEmailCodeChallenge? DirectiveEmailCodeChallenge { get; set; }

        [JsonPropertyName("directiveStartExternalLogin")]
        public DirectiveStartExternalLogin? DirectiveStartExternalLogin { get; set; }

    }
    public class DirectiveDisplayPasswordPage
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("hasPasskey")]
        public bool HasPasskey { get; set; }
    }
    public class DirectiveEmailCodeChallenge
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
    }
    public class DirectiveStartExternalLogin
    {
        [JsonPropertyName("slug")]
        public string Slug { get; set; }
    }

    public class StartExternalIDPLoginRequest  {
        [JsonPropertyName("slug")]
        public string Slug { get; set; }
        [JsonPropertyName("directive")]
        public string Directive { get; set; }

        
    }

    public class StartExternalIDPLoginResponse  {
        [JsonPropertyName("redirectUri")]
        public string RedirectUri { get; set; } = "";
    }

    public class LoginPasswordRequest  {
        [JsonPropertyName("email")]
        public string Email { get; set; } = "";

        [JsonPropertyName("password")]
        public string Password { get; set; } = "";
    }

    public class LoginPasswordResponse  {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("directive")]
        public string Directive { get; set; }

        [JsonPropertyName("directiveRedirect")]
        public DirectiveRedirect? DirectiveRedirect { get; set; }

        [JsonPropertyName("directiveEmailCodeChallenge")]
        public DirectiveEmailCodeChallenge? DirectiveEmailCodeChallenge { get; set; }
    }
    public class DirectiveRedirect
    {
        [JsonPropertyName("redirectUri")]
        public string RedirectURI { get; set; } = "";

    }

    public class VerifyCodeRequest  {
        [JsonPropertyName("code")]
        public string Code { get; set; } = "";
        
    }

    public class VerifyCodeResponse  {
        [JsonPropertyName("directive")]
        public string Directive { get; set; } = "";

        [JsonPropertyName("directiveRedirect")]
        public DirectiveRedirect? DirectiveRedirect { get; set; } 
    }
}
