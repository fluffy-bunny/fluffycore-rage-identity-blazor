using Microsoft.AspNetCore.Components;

namespace BlazorAccountManagement.Services
{
    public class RedirectService
    {
        private readonly NavigationManager _navigationManager;

        public RedirectService(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public void RedirectToUrl(string url)
        {
            _navigationManager.NavigateTo(url);
        }
    }
}
