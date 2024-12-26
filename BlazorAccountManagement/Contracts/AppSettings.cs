namespace BlazorAccountManagement.Contracts
{
    public class HomePageSettings
    {
        public string? Title { get; set; }
        public string? Body { get; set; }
        public string? GetSartedUrl { get; set; }
        public string? LearnMoreUrl { get; set; }
        public string? ImageUrl { get; set; }
    }
    public class AppSettings
    {
        public string ApplicationEnvironment { get; set; }
        public string BaseApiUrl { get; set; }
        public string AppName { get; set; }
        public string UnauthorizedRedirectUrl { get; set; }
        public string PrivacyPolicyUrl { get; set; }
        public string CookiePolicyUrl { get; set; }
        public HomePageSettings HomePageSettings { get; set; }


    }
}
