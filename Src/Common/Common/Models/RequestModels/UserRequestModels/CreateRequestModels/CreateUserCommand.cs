using MediatR;

namespace Common.Models.RequestModels.UserRequestModels.CreateRequestModels
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string PasswordStr { get; set; }

        public CreateUserCommand()
        {

        }

        public CreateUserCommand(string firstName, string lastName, string userName, string emailAddress, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            EmailAddress = emailAddress;
            PasswordStr = password;
        }
    }
}
