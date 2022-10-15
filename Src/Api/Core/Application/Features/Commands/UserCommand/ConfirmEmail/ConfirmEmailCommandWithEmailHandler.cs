using Application.Interfaces.Repositories;
using Common.Infrastructure.Exceptions;
using Common.Models.RequestModels.UserRequestModels.ConfirmEmailRequestModel;
using MediatR;

namespace Application.Features.Commands.UserCommand.ConfirmEmail
{
    public class ConfirmEmailCommandWithEmailHandler : IRequestHandler<ConfirmEmailCommandWithEmail, bool>
    {
        private readonly IUserRepository userRepository;
        private readonly IEmailConfirmationRepository emailConfirmationRepository;

        public ConfirmEmailCommandWithEmailHandler(IUserRepository userRepository, IEmailConfirmationRepository emailConfirmationRepository)
        {
            this.userRepository = userRepository;
            this.emailConfirmationRepository = emailConfirmationRepository;
        }

        public async Task<bool> Handle(ConfirmEmailCommandWithEmail request, CancellationToken cancellationToken)
        {
            var emailConfirmation = await emailConfirmationRepository.GetFirstOrDefaultAsync(x => x.NewEmailAddress == request.NewEmailAddress);

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
