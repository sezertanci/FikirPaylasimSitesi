using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Constants;
using Common.Events.UserEvent;
using Common.Infrastructure;
using Common.Infrastructure.Exceptions;
using Common.Models.RequestModels.UserRequestModels.UpdateRequestModels;
using MediatR;

namespace Application.Features.Commands.UserCommand.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.Id);

            if(user is null)
                throw new DatabaseValidationException("User not found!");

            var dbEmailAddress = user.EmailAddress;

            mapper.Map(request, user);

            var result = await userRepository.UpdateAsync(user);

            var emailChanged = string.CompareOrdinal(dbEmailAddress, request.EmailAddress) != 0;

            if(emailChanged && result > 0)
            {
                var userEmailChangedEvent = new UserEmailChangedEvent
                {
                    NewEmailAddress = user.EmailAddress,
                    OldEmailAddress = dbEmailAddress
                };

                QueryFactory.SendMessageToExchange(exchangeName: CommonConstants.UserExchangeName,
                                                   queueName: CommonConstants.UserEmailChangedQueueName,
                                                   exchangeType: CommonConstants.DefaultExchangeType,
                                                   userEmailChangedEvent);

                user.EmailConfirmed = false;

                await userRepository.UpdateAsync(user);
            }

            return result != 0;
        }
    }
}
