using Common.Models;

namespace Domain.Models
{
    public class EntryVote : BaseEntity
    {
        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }
        public VoteType VoteType { get; set; }

        public EntryVote()
        {

        }

        public EntryVote(Guid id, Guid entryId, Guid userId, VoteType voteType) : this()
        {
            Id = id;
            EntryId = entryId;
            UserId = userId;
            VoteType = voteType;
        }

        public virtual Entry Entry { get; set; }
    }
}
