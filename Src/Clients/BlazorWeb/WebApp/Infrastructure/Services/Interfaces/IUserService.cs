using Common.Models.Queries;
using Common.Models.RequestModels.UserRequestModels.UpdateRequestModels;

namespace WebApp.Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> ChangeUserPassword(string oldPassword, string newPassword);
        Task<UserDetailViewModel> GetUserDetail(Guid id);
        Task<UserDetailViewModel> GetUserDetail(string userName);
        Task<bool> UpdateUser(UpdateUserCommand updateUserCommand);
        Task<GetMyStatisticsViewModel> GetMyStatistics();
    }
}