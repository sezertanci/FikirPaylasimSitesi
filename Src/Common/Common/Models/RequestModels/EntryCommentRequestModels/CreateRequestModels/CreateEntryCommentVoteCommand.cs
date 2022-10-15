using MediatR;

namespace Common.Models.RequestModels.EntryCommentRequestModels.CreateRequestModels
{
    public class CreateEntryCommentVoteCommand : IRequest<bool>
    {
        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }
        public VoteType VoteType { get; set; }

        public CreateEntryCommentVoteCommand()
        {

        }

        public CreateEntryCommentVoteCommand(Guid entryCommentId, Guid userId, VoteType voteType)
        {
            EntryCommentId = entryCommentId;
            UserId = userId;
            VoteType = voteType;
        }
    }
}
