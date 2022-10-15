using AutoMapper;
using Common.Constants;
using Common.Events.EntryFavoriteEvent;
using Common.Infrastructure;
using Common.Models.RequestModels.EntryRequestModels.DeleteRequestModels;
using MediatR;

namespace Application.Features.Commands.EntryCommand.Delete
{
    public class DeleteEntryFavoriteCommandHandler : IRequestHandler<DeleteEntryFavoriteCommand, bool>
    {
        private readonly IMapper mapper;

        public DeleteEntryFavoriteCommandHandler(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<bool> Handle(DeleteEntryFavoriteCommand request, CancellationToken cancellationToken)
        {
            var deleteEntryFavoriteEvent = mapper.Map<DeleteEntryFavoriteEvent>(request);

            QueryFactory.SendMessageToExchange(exchangeName: CommonConstants.EntryFavoriteExchangeName,
                                               queueName: CommonConstants.DeleteEntryFavoriteQueueName,
                                               exchangeType: CommonConstants.DefaultExchangeType,
                                               obj: deleteEntryFavoriteEvent);

            return await Task.FromResult(true);
        }
    }
}
