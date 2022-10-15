using Application.Interfaces.Repositories;
using Common.Models.RequestModels.UserRequestModels.DeleteRequestModels;
using MediatR;

namespace Application.Features.Commands.UserCommand.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.Id);

            if(user is null)
                return false;

            user.Deleted = true;

            var result = await userRepository.UpdateAsync(user);

            return result != 0;
        }
    }
}
