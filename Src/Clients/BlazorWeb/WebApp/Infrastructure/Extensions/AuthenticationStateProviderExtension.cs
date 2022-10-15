using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace WebApp.Infrastructure.Extensions
{
    public static class AuthenticationStateProviderExtension
    {
        public static async Task<Guid> GetUserId(this AuthenticationStateProvider provider)
        {
            var state = await provider.GetAuthenticationStateAsync();

            var userId = state.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return string.IsNullOrEmpty(userId) ? Guid.Empty : new Guid(userId);
        }
    }
}
