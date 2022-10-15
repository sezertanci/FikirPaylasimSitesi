using MediatR;

namespace Common.Models.RequestModels.UserRequestModels.DeleteRequestModels
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public DeleteUserCommand()
        {

        }

        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }
    }
}
