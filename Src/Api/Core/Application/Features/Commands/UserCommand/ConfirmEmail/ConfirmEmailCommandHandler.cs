using Application.Interfaces.Repositories;
using Common.Infrastructure.Exceptions;
using Common.Models.RequestModels.UserRequestModels.ConfirmEmailRequestModel;
using MediatR;

namespace Application.Features.Commands.UserCommand.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, bool>
    {
        private readonly IUserRepository userRepository;
        private readonly IEmailConfirmationRepository emailConfirmationRepository;

        public ConfirmEmailCommandHandler(IUserRepository userRepository, IEmailConfirmationRepository emailConfirmationRepository)
        {
            this.userRepository = userRepository;
            this.emailConfirmationRepository = emailConfirmationRepository;
        }

        public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var emailConfirmation = await emailConfirmationRepository.GetByIdAsync(request.Id);

            if(emailConfirmation is null)
                throw new DatabaseValidationException("Confirmation not found!");

            var dbUser = await userRepository.GetSingleAsync(x => x.EmailAddress == emailConfirmation.NewEmailAddress);

            if(dbUser is null)
                throw new DatabaseValidationException("User not found with this email!");

            if(dbUser.EmailConfirmed)
                throw new DatabaseValidationException("Email address is already confirmed!");

            dbUser.EmailConfirmed = true;

            await userRepository.UpdateAsync(dbUser);

            return true;
        }
    }
}
