namespace Domain.Models
{
    public class EntryComment : BaseEntity
    {
        public string Content { get; set; }
        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }

        public EntryComment()
        {

        }

        public EntryComment(Guid id, Guid entryId, Guid userId, string content) : this()
        {
            Id = id;
            EntryId = entryId;
            UserId = userId;
            Content = content;
        }

        public virtual Entry Entry { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<EntryCommentVote> EntryCommentVotes { get; set; }
        public virtual ICollection<EntryCommentFavorite> EntryCommentFavorites { get; set; }
    }
}
