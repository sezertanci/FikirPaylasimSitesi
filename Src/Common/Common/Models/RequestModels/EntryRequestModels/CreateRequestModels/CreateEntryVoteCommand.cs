using MediatR;

namespace Common.Models.RequestModels.EntryRequestModels.CreateRequestModels
{
    public class CreateEntryVoteCommand : IRequest<bool>
    {
        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }
        public VoteType VoteType { get; set; }

        public CreateEntryVoteCommand()
        {

        }

        public CreateEntryVoteCommand(Guid entryId, Guid userId, VoteType voteType)
        {
            EntryId = entryId;
            UserId = userId;
            VoteType = voteType;
        }
    }
}
