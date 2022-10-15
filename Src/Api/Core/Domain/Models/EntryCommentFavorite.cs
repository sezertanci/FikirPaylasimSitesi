namespace Domain.Models
{
    public class EntryCommentFavorite : BaseEntity
    {
        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }

        public EntryCommentFavorite()
        {

        }

        public EntryCommentFavorite(Guid id, Guid entryCommentId, Guid userId) : this()
        {
            Id = id;
            EntryCommentId = entryCommentId;
            UserId = userId;
        }

        public virtual EntryComment EntryComment { get; set; }
        public virtual User User { get; set; }
    }
}
