namespace BlazorAccountManagement.Contracts
{
    public class AppSettings
    {
        public string ApplicationEnvironment { get; set; }
        public string BaseApiUrl { get; set; }
        public string AppName { get; set; }
        public string UnauthorizedRedirectUrl { get; set; }
    }
}
