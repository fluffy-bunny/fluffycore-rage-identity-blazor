using System.Text.Json.Serialization;

namespace BlazorOIDCFlow.Data
{
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

}
