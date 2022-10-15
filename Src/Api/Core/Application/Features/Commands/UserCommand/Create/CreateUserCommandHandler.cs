using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Constants;
using Common.Events.UserEvent;
using Common.Infrastructure;
using Common.Infrastructure.Exceptions;
using Common.Models.RequestModels.UserRequestModels.CreateRequestModels;
using Domain.Models;
using MediatR;

namespace Application.Features.Commands.UserCommand.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existsUser = await userRepository.GetSingleAsync(x => x.EmailAddress == request.EmailAddress);

            if(existsUser is not null)
                throw new DatabaseValidationException("User already exists!");

            var user = mapper.Map<User>(request);

            PasswordEncryptor.Encrypt(request.PasswordStr, out byte[] hash, out byte[] salt);

            user.Password = hash;
            user.PasswordSalt = salt;

            var rows = await userRepository.AddAsync(user);

            if(rows > 0)
            {
                var @event = new UserEmailChangedEvent
                {
                    NewEmailAddress = user.EmailAddress,
                    OldEmailAddress = null
                };

                QueryFactory.SendMessageToExchange(exchangeName: CommonConstants.UserExchangeName,
                                                   queueName: CommonConstants.UserEmailChangedQueueName,
                                                   exchangeType: CommonConstants.DefaultExchangeType,
                                                   @event);
            }

            return user.Id;
        }
    }
}
