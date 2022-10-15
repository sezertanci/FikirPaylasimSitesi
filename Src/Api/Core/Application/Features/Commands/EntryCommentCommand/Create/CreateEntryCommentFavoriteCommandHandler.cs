using AutoMapper;
using Common.Constants;
using Common.Events.EntryCommentFavoriteEvent;
using Common.Infrastructure;
using Common.Models.RequestModels.EntryCommentRequestModels.CreateRequestModels;
using MediatR;

namespace Application.Features.Commands.EntryCommentCommand.Create
{
    public class CreateEntryCommentFavoriteCommandHandler : IRequestHandler<CreateEntryCommentFavoriteCommand, bool>
    {
        private readonly IMapper mapper;

        public CreateEntryCommentFavoriteCommandHandler(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<bool> Handle(CreateEntryCommentFavoriteCommand request, CancellationToken cancellationToken)
        {
            var createEntryCommentFavoriteEvent = mapper.Map<CreateEntryCommentFavoriteEvent>(request);

            QueryFactory.SendMessageToExchange(exchangeName: CommonConstants.EntryCommentFavoriteExchangeName,
                                               queueName: CommonConstants.CreateEntryCommentFavoriteQueueName,
                                               exchangeType: CommonConstants.DefaultExchangeType,
                                               obj: createEntryCommentFavoriteEvent);

            return await Task.FromResult(true);
        }
    }
}
