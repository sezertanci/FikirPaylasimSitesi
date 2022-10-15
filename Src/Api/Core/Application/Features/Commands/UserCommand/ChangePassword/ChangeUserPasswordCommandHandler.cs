using Application.Interfaces.Repositories;
using Common.Events.UserEvent;
using Common.Infrastructure;
using Common.Infrastructure.Exceptions;
using MediatR;

namespace Application.Features.Commands.UserCommand.ChangePassword
{
    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, bool>
    {
        private readonly IUserRepository userRepository;

        public ChangeUserPasswordCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.UserId);

            if(user is null)
                throw new DatabaseValidationException("User not found!");

            if(!PasswordEncryptor.VerifyPassword(request.OldPassword, user.Password, user.PasswordSalt))
                throw new DatabaseValidationException("Old password wrong!");

            PasswordEncryptor.Encrypt(request.NewPassword, out byte[] hash, out byte[] salt);

            user.Password = hash;
            user.PasswordSalt = salt;

            await userRepository.UpdateAsync(user);

            return true;
        }
    }
}
