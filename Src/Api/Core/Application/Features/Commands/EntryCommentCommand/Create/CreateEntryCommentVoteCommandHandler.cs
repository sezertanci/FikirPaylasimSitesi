using AutoMapper;
using Common.Constants;
using Common.Events.EntryCommentVoteEvent;
using Common.Infrastructure;
using Common.Models.RequestModels.EntryCommentRequestModels.CreateRequestModels;
using MediatR;

namespace Application.Features.Commands.EntryCommentCommand.Create
{
    public class CreateEntryCommentVoteCommandHandler : IRequestHandler<CreateEntryCommentVoteCommand, bool>
    {
        private readonly IMapper mapper;

        public CreateEntryCommentVoteCommandHandler(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<bool> Handle(CreateEntryCommentVoteCommand request, CancellationToken cancellationToken)
        {
            var createEntryCommentVoteEvent = mapper.Map<CreateEntryCommentVoteEvent>(request);

            QueryFactory.SendMessageToExchange(exchangeName: CommonConstants.EntryCommentVoteExchangeName,
                                            queueName: CommonConstants.CreateEntryCommentVoteQueueName,
                                            exchangeType: CommonConstants.DefaultExchangeType,
                                            obj: createEntryCommentVoteEvent);

            return await Task.FromResult(true);
        }
    }
}
