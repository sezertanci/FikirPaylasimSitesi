using AutoMapper;
using Common.Constants;
using Common.Events.EntryVoteEvent;
using Common.Infrastructure;
using Common.Models.RequestModels.EntryRequestModels.CreateRequestModels;
using MediatR;

namespace Application.Features.Commands.EntryCommand.Create
{
    public class CreateEntryVoteCommandHandler : IRequestHandler<CreateEntryVoteCommand, bool>
    {
        private readonly IMapper mapper;

        public CreateEntryVoteCommandHandler(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<bool> Handle(CreateEntryVoteCommand request, CancellationToken cancellationToken)
        {
            var createEntryVoteEvent = mapper.Map<CreateEntryVoteEvent>(request);

            QueryFactory.SendMessageToExchange(exchangeName: CommonConstants.EntryVoteExchangeName,
                                               queueName: CommonConstants.CreateEntryVoteQueueName,
                                               exchangeType: CommonConstants.DefaultExchangeType,
                                               obj: createEntryVoteEvent);

            return await Task.FromResult(true);
        }
    }
}
