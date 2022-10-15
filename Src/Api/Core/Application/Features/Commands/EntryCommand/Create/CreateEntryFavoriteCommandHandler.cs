using AutoMapper;
using Common.Constants;
using Common.Events.EntryFavoriteEvent;
using Common.Infrastructure;
using Common.Models.RequestModels.EntryRequestModels.CreateRequestModels;
using MediatR;

namespace Application.Features.Commands.EntryCommand.Create
{
    public class CreateEntryFavoriteCommandHandler : IRequestHandler<CreateEntryFavoriteCommand, bool>
    {
        private readonly IMapper mapper;

        public CreateEntryFavoriteCommandHandler(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<bool> Handle(CreateEntryFavoriteCommand request, CancellationToken cancellationToken)
        {
            var createEntryFavoriteEvent = mapper.Map<CreateEntryFavoriteEvent>(request);

            QueryFactory.SendMessageToExchange(exchangeName: CommonConstants.EntryFavoriteExchangeName,
                                               queueName: CommonConstants.CreateEntryFavoriteQueueName,
                                               exchangeType: CommonConstants.DefaultExchangeType,
                                               obj: createEntryFavoriteEvent);

            return await Task.FromResult(true);
        }
    }
}
