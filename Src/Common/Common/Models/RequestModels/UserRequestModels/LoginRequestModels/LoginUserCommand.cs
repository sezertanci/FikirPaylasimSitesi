using Common.Models.Queries;
using MediatR;

namespace Common.Models.RequestModels.UserRequestModels.LoginRequestModels
{
    public class LoginUserCommand : IRequest<LoginUserViewModel>
    {

        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public LoginUserCommand(string emailAddress, string password)
        {
            EmailAddress = emailAddress;
            Password = password;
        }

        public LoginUserCommand()
        {

        }
    }
}
