using AutoMapper;
using Common.Constants;
using Common.Events.EntryCommentVoteEvent;
using Common.Infrastructure;
using Common.Models.RequestModels.EntryCommentRequestModels.DeleteRequestModel;
using MediatR;

namespace Application.Features.Commands.EntryCommentCommand.Delete
{
    public class DeleteEntryCommentVoteCommandHandler : IRequestHandler<DeleteEntryCommentVoteCommand, bool>
    {
        private readonly IMapper mapper;

        public DeleteEntryCommentVoteCommandHandler(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<bool> Handle(DeleteEntryCommentVoteCommand request, CancellationToken cancellationToken)
        {
            var deleteEntryCommentVoteEvent = mapper.Map<DeleteEntryCommentVoteEvent>(request);

            QueryFactory.SendMessageToExchange(exchangeName: CommonConstants.EntryCommentVoteExchangeName,
                                               queueName: CommonConstants.DeleteEntryCommentVoteQueueName,
                                               exchangeType: CommonConstants.DefaultExchangeType,
                                               obj: deleteEntryCommentVoteEvent);

            return await Task.FromResult(true);
        }
    }
}
