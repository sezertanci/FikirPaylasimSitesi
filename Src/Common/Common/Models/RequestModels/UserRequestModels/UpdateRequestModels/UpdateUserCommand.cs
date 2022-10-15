using MediatR;

namespace Common.Models.RequestModels.UserRequestModels.UpdateRequestModels
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }

        public UpdateUserCommand()
        {

        }

        public UpdateUserCommand(Guid id, string firstName, string lastName, string userName, string emailAddress)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            EmailAddress = emailAddress;
        }
    }
}
