using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.Json.Serialization;


namespace BlazorOIDCFlow.Data
{
    public enum OIDCPage
    {
        SigninUserName,
        SigninUserNamePassword,
        SignUpUserName,
        SignUpUserNamePassword,
        VerifyCode,
        ForgotPassword,
        ResetPassword,

    }
    public enum Directive
    {
        Directive_Unspecified,
        Directive_ResetPassword,
        Directive_Login
    }
    public class OIDCModel
    {
        public bool DevelopmentMode { get; set; }

        public string? Email { get; set; }


        public string? Password { get; set; }

        public Manifest? Manifest { get; set; }

        public string Code { get; set; } = "";

        public Directive Directive { get; set; } = Directive.Directive_Unspecified;

        public OIDCPage InitialPage { get; set; } = OIDCPage.SigninUserName;
    }
    public class Manifest
    {
        // json is social_idps
        [JsonPropertyName("social_idps")]
        public List<SocialIdp> SocialIdps { get; set; } = new List<SocialIdp>();
        [JsonPropertyName("passkey_enabled")]
        public bool PasskeyEnabled { get; set; } = false;

        [JsonPropertyName("development_mode")]
        public bool DevelopmentMode { get; set; } = false;

        [JsonPropertyName("landing_page")]
        public LandingPage? LandingPage { get; set; }
    }
    public class LandingPage
    {
        [JsonPropertyName("page")]
        public string Page { get; set; } = "";
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

        [JsonPropertyName("passkeyAvailabel")]

        public bool PasskeyAvailabel { get; set; }
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
    public class ResponseWrapper<T>
    {
        [JsonPropertyName("response")]
        public T Response { get; set; }

        [JsonPropertyName("statusCode")]
        public HttpStatusCode StatusCode { get; set; }

        [JsonPropertyName("error")]
        public string Error { get; set; }
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

    public class StartExternalLoginRequest  {
        [JsonPropertyName("slug")]
        public string Slug { get; set; }
        [JsonPropertyName("directive")]
        public string Directive { get; set; }

        
    }

    public class StartExternalLoginResponse  {
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
    public class LoginCurrentUserResponse
    {
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
    public class VerifyCodeBeginResponse
    {
        [JsonPropertyName("code")]
        public string Code { get; set; } = "";

        [JsonPropertyName("valid")]
        public bool Valid { get; set; }  

        [JsonPropertyName("email")]
        public string Email { get; set; } = "";

    }
 
    public class VerifyCodeResponse  {
        [JsonPropertyName("directive")]
        public string Directive { get; set; } = "";

        [JsonPropertyName("directiveRedirect")]
        public DirectiveRedirect? DirectiveRedirect { get; set; } 
    }
    public class SignupRequest  {
        [JsonPropertyName("email")]
        public string Email { get; set; } = "";

        [JsonPropertyName("password")]
        public string Password { get; set; } = "";
    }
    public enum SignupErrorReason
    {
        SignupErrorReason_NoError = 0,
        SignupErrorReason_InvalidPassword = 1,
        SignupErrorReason_UserAlreadyExists = 2,
    }
    public class SignupResponse  {
        [JsonPropertyName("email")]
        public string Email { get; set; } = "";

        [JsonPropertyName("directive")]
        public string Directive { get; set; } = "";

        [JsonPropertyName("directiveRedirect")]
        public DirectiveRedirect? DirectiveRedirect { get; set; }

        [JsonPropertyName("directiveEmailCodeChallenge")]
        public DirectiveEmailCodeChallenge? DirectiveEmailCodeChallenge { get; set; }

        [JsonPropertyName("directiveStartExternalLogin")]
        public DirectiveStartExternalLogin? DirectiveStartExternalLogin { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("errorReason")]
        public SignupErrorReason ErrorReason { get; set; } = SignupErrorReason.SignupErrorReason_NoError;

    }

    public class PasswordResetStartRequest  {
        [JsonPropertyName("email")]
        public string Email { get; set; } = "";
    }
    public class PasswordResetStartResponse
    {
        [JsonPropertyName("email")]
        public string Email { get; set; } = "";

        [JsonPropertyName("directive")]
        public string Directive { get; set; } = "";


        [JsonPropertyName("directiveEmailCodeChallenge")]
        public DirectiveEmailCodeChallenge? DirectiveEmailCodeChallenge { get; set; }

    }

    public class PasswordResetFinishRequest  {
        [JsonPropertyName("password")]
        public string Password { get; set; } = "";

        [JsonPropertyName("passwordConfirm")]
        public string PasswordConfirm { get; set; } = "";
    }
    public enum PasswordResetErrorReason
    {
        PasswordResetErrorReason_NoError = 0,
        PasswordResetErrorReason_InvalidPassword = 1,
        PasswordResetErrorReason_PasswordsDoNotMatch = 2,
    }
    public class PasswordResetFinishResponse  {
        [JsonPropertyName("directive")]
        public string Directive { get; set; } = "";

        [JsonPropertyName("errorReason")]
        public PasswordResetErrorReason ErrorReason { get; set; } = PasswordResetErrorReason.PasswordResetErrorReason_NoError;
    }
    public class UserProfile
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [Required]
        [JsonPropertyName("givenName")]
        public string GivenName { get; set; }

        [Required]
        [JsonPropertyName("familyName")]
        public string FamilyName { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }
    }
    public class ValidOIDCSessionResponse
    {
        [JsonPropertyName("valid")]
        public bool Valid { get; set; }
    }
}
