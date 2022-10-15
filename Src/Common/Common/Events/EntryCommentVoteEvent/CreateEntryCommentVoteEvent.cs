using Common.Models;

namespace Common.Events.EntryCommentVoteEvent
{
    public class CreateEntryCommentVoteEvent
    {
        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }
        public VoteType VoteType { get; set; }
    }
}
