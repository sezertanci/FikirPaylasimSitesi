using AutoMapper;
using Common.Constants;
using Common.Events.EntryCommentFavoriteEvent;
using Common.Infrastructure;
using Common.Models.RequestModels.EntryCommentRequestModels.DeleteRequestModel;
using MediatR;

namespace Application.Features.Commands.EntryCommentCommand.Delete
{
    public class DeleteEntryCommentFavoriteCommandHandler : IRequestHandler<DeleteEntryCommentFavoriteCommand, bool>
    {
        private readonly IMapper mapper;

        public DeleteEntryCommentFavoriteCommandHandler(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<bool> Handle(DeleteEntryCommentFavoriteCommand request, CancellationToken cancellationToken)
        {
            var deleteEntryCommentFavoriteEvent = mapper.Map<DeleteEntryCommentFavoriteEvent>(request);

            QueryFactory.SendMessageToExchange(exchangeName: CommonConstants.EntryCommentFavoriteExchangeName,
                                               queueName: CommonConstants.DeleteEntryCommentFavoriteQueueName,
                                               exchangeType: CommonConstants.DefaultExchangeType,
                                               obj: deleteEntryCommentFavoriteEvent);

            return await Task.FromResult(true);
        }
    }
}
