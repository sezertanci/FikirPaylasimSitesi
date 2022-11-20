using Blazored.LocalStorage;
using Common.Infrastructure.Exceptions;
using Common.Infrastructure.Results;
using Common.Models.Queries;
using Common.Models.RequestModels.UserRequestModels.ConfirmEmailRequestModel;
using Common.Models.RequestModels.UserRequestModels.CreateRequestModels;
using Common.Models.RequestModels.UserRequestModels.LoginRequestModels;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using WebApp.Infrastructure.Auth;
using WebApp.Infrastructure.Extensions;
using WebApp.Infrastructure.Services.Interfaces;

namespace WebApp.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient client;
        private readonly ISyncLocalStorageService syncLocalStorageService;
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private readonly IConfiguration configuration;

        public IdentityService(HttpClient client, ISyncLocalStorageService syncLocalStorageService, AuthenticationStateProvider authenticationStateProvider, IConfiguration configuration)
        {
            this.client = client;
            this.syncLocalStorageService = syncLocalStorageService;
            this.authenticationStateProvider = authenticationStateProvider;
            this.configuration = configuration;
        }

        public bool IsLoggedIn => !string.IsNullOrEmpty(GetUserToken());

        public string GetUserToken()
        {
            return syncLocalStorageService.GetToken();
        }

        public string GetUserName()
        {
            return syncLocalStorageService.GetUserName();
        }

        public Guid GetUserId()
        {
            return syncLocalStorageService.GetUserId();
        }

        public async Task<bool> Login(LoginUserCommand loginUserCommand)
        {
            string responseStr;
            var httpResponse = await client.PostAsJsonAsync("User/Login", loginUserCommand);

            if(httpResponse != null && !httpResponse.IsSuccessStatusCode)
            {
                if(httpResponse.StatusCode == HttpStatusCode.BadRequest)
                {
                    responseStr = await httpResponse.Content.ReadAsStringAsync();
                    var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseStr);
                    responseStr = validation.FlattenErrors;
                    throw new DatabaseValidationException(responseStr);
                }

                return false;
            }

            responseStr = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<LoginUserViewModel>(responseStr);

            if(!string.IsNullOrEmpty(response.Token))
            {
                syncLocalStorageService.SetToken(response.Token);
                syncLocalStorageService.SetUserName(response.UserName);
                syncLocalStorageService.SetUserId(response.Id);
                syncLocalStorageService.SetName(response.FirstName + " " + response.LastName);

                ((AuthStateProvider)authenticationStateProvider).NotifyUserLogin(response.UserName, response.Id);

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", response.Token);

                return true;
            }

            return false;
        }

        public void Logout()
        {
            syncLocalStorageService.RemoveItem(LocalStorageExtension.TokenName);
            syncLocalStorageService.RemoveItem(LocalStorageExtension.UserId);
            syncLocalStorageService.RemoveItem(LocalStorageExtension.UserName);
            syncLocalStorageService.RemoveItem(LocalStorageExtension.Name);

            ((AuthStateProvider)authenticationStateProvider).NotifyUserLogout();

            client.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<bool> Register(CreateUserCommand createUserCommand)
        {
            var response = await client.PostAsJsonAsync("User/Create", createUserCommand);

            if(response != null && !response.IsSuccessStatusCode)
            {
                if(response.StatusCode == HttpStatusCode.BadRequest)
                {
                    string responseStr = await response.Content.ReadAsStringAsync();
                    var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseStr);
                    responseStr = validation.FlattenErrors;
                    throw new DatabaseValidationException(responseStr);
                }

                return false;
            }

            return true;
        }

        public async Task<bool> Confirm(ConfirmEmailCommandWithEmail confirmEmailCommandWithEmail)
        {
            var response = await client.PostAsJsonAsync($"User/ConfirmEmail", confirmEmailCommandWithEmail);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CheckToken()
        {
            var response = await client.GetAsync($"User/CheckToken");

            return response.IsSuccessStatusCode;
        }

        public string GetAdminPassword()
        {
            return configuration["AdminPassword"].ToString();
        }

        public bool ShowAdministratorButton()
        {
            return Convert.ToBoolean(configuration["ShowAdministratorButton"]);
        }

        public string GetName()
        {
            return syncLocalStorageService.GetName();
        }
    }
}
