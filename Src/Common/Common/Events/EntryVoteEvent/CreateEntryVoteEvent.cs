using Common.Models;

namespace Common.Events.EntryVoteEvent
{
    public class CreateEntryVoteEvent
    {
        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }
        public VoteType VoteType { get; set; }
    }
}
