using Common.Events.UserEvent;
using Common.Infrastructure.Exceptions;
using Common.Infrastructure.Results;
using Common.Models.Queries;
using Common.Models.RequestModels.UserRequestModels.UpdateRequestModels;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using WebApp.Infrastructure.Services.Interfaces;

namespace WebApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient client;

        public UserService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<UserDetailViewModel> GetUserDetail(Guid id)
        {
            var response = await client.GetFromJsonAsync<UserDetailViewModel>($"User/{id}");

            return response;
        }

        public async Task<UserDetailViewModel> GetUserDetail(string userName)
        {
            var response = await client.GetFromJsonAsync<UserDetailViewModel>($"User/UserName/{userName}");

            return response;
        }

        public async Task<bool> UpdateUser(UpdateUserCommand updateUserCommand)
        {
            var response = await client.PostAsJsonAsync("User/Update", updateUserCommand);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ChangeUserPassword(string oldPassword, string newPassword)
        {
            var command = new ChangeUserPasswordCommand(Guid.Empty, oldPassword, newPassword);

            var response = await client.PostAsJsonAsync("User/ChangePassword", command);

            if(response != null && !response.IsSuccessStatusCode)
            {
                if(response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var responseStr = await response.Content.ReadAsStringAsync();
                    var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseStr);
                    responseStr = validation.FlattenErrors;
                    throw new DatabaseValidationException(responseStr);
                }

                return false;
            }

            return response.IsSuccessStatusCode;
        }

        public async Task<GetMyStatisticsViewModel> GetMyStatistics()
        {
            var response = await client.GetFromJsonAsync<GetMyStatisticsViewModel>($"User/Statistics");

            return response;
        }
    }
}
