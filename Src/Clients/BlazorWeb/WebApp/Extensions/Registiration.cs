using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using WebApp.Infrastructure.Auth;
using WebApp.Infrastructure.Services;
using WebApp.Infrastructure.Services.Interfaces;

namespace Persistence.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            var webApiAddress = configuration["WebApiHost"];

            services.AddHttpClient("WebApiClient", client =>
            {
                client.BaseAddress = new Uri(webApiAddress);
            }).AddHttpMessageHandler<AuthTokenHandler>();

            services.AddScoped(sp =>
            {
                var clientFactory = sp.GetRequiredService<IHttpClientFactory>();

                return clientFactory.CreateClient("WebApiClient");
            });

            services.AddScoped<AuthTokenHandler>();
            services.AddTransient<ITagService, TagService>();
            services.AddTransient<IVoteService, VoteService>();
            services.AddTransient<IEntryService, EntryService>();
            services.AddTransient<IEntryCommentService, EntryCommentService>();
            services.AddTransient<IFavoriteService, FavoriteService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            services.AddBlazoredLocalStorage();
            services.AddAuthorizationCore();

            return services;
        }
    }
}
