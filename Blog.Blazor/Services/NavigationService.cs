using Microsoft.AspNetCore.Components;

namespace Blog.Blazor.Services
{
    public class NavigationService
    {
        private readonly NavigationManager _navigationManager;

        public NavigationService(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public void NavigateTo(string uri)
        {
            _navigationManager.NavigateTo(uri);
        }

        public void NavigateToWithReturn(string uri)
        {
            var currentUri = _navigationManager.Uri;
            var encodedCurrentUri = Uri.EscapeDataString(currentUri);
            _navigationManager.NavigateTo($"{uri}?tg={encodedCurrentUri}");
        }

        public string GetReturnUrl()
        {
            var uri = new Uri(_navigationManager.Uri);
            var queryParams = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(uri.Query);
            return queryParams.TryGetValue("tg", out var returnUrl) ? returnUrl.ToString() : string.Empty;
        }
    }
}
