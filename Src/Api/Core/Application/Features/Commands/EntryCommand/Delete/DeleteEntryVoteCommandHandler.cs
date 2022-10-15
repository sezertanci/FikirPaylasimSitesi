using AutoMapper;
using Common.Constants;
using Common.Events.EntryVoteEvent;
using Common.Infrastructure;
using Common.Models.RequestModels.EntryRequestModels.DeleteRequestModels;
using MediatR;

namespace Application.Features.Commands.EntryCommand.Delete
{
    public class DeleteEntryVoteCommandHandler : IRequestHandler<DeleteEntryVoteCommand, bool>
    {
        private readonly IMapper mapper;

        public DeleteEntryVoteCommandHandler(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<bool> Handle(DeleteEntryVoteCommand request, CancellationToken cancellationToken)
        {
            var deleteEntryVoteEvent = mapper.Map<DeleteEntryVoteEvent>(request);

            QueryFactory.SendMessageToExchange(exchangeName: CommonConstants.EntryVoteExchangeName,
                                               queueName: CommonConstants.DeleteEntryVoteQueueName,
                                               exchangeType: CommonConstants.DefaultExchangeType,
                                               obj: deleteEntryVoteEvent);

            return await Task.FromResult(true);
        }
    }
}
