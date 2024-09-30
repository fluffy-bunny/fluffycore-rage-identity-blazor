using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorAccountManagement.Models
{
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
        public string PhoneNumber { get;set; }
    }
    public class LogoutRequest { }
    public class LogoutResponse {
        [JsonPropertyName("directive")]
        public string Directive { get; set; }

        [JsonPropertyName("redirectURL")]
        public string RedirectURL { get; set; }
    }
    public class LinkedIdentity
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Passkey
    {
        [JsonPropertyName("aaguid")]
        public string AAGUID { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class UserIdentityInfo
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("linkedIdentities")]
        public List<LinkedIdentity> LinkedIdentities { get; set; }

        [JsonPropertyName("passkeyEligible")]
        public bool PasskeyEligible { get; set; }

        [JsonPropertyName("passkeys")]
        public List<Passkey> Passkeys { get; set; }
    }
}
