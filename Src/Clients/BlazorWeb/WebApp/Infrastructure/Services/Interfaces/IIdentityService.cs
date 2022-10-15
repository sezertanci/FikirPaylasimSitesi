using Common.Models.RequestModels.UserRequestModels.ConfirmEmailRequestModel;
using Common.Models.RequestModels.UserRequestModels.CreateRequestModels;
using Common.Models.RequestModels.UserRequestModels.LoginRequestModels;

namespace WebApp.Infrastructure.Services.Interfaces
{
    public interface IIdentityService
    {
        bool IsLoggedIn { get; }

        Guid GetUserId();
        string GetUserName();
        string GetUserToken();
        Task<bool> Login(LoginUserCommand loginUserCommand);
        Task<bool> Register(CreateUserCommand createUserCommand);
        void Logout();
        Task<bool> Confirm(ConfirmEmailCommandWithEmail confirmEmailCommandWithEmail);
        Task<bool> CheckToken();
        string GetAdminPassword();
        bool ShowAdministratorButton();
    }
}