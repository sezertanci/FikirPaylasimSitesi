using Common.Models;

namespace Domain.Models
{
    public class EntryCommentVote : BaseEntity
    {
        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }
        public VoteType VoteType { get; set; }

        public EntryCommentVote()
        {

        }

        public EntryCommentVote(Guid id, Guid entryCommentId, Guid userId, VoteType voteType) : this()
        {
            Id = id;
            EntryCommentId = entryCommentId;
            UserId = userId;
            VoteType = voteType;
        }

        public virtual EntryComment EntryComment { get; set; }
    }
}
