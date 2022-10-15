namespace Domain.Models
{
    public class EntryFavorite : BaseEntity
    {
        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }

        public EntryFavorite()
        {

        }

        public EntryFavorite(Guid id, Guid entryId, Guid userId) : this()
        {
            Id = id;
            EntryId = entryId;
            UserId = userId;
        }

        public virtual Entry Entry { get; set; }
        public virtual User User { get; set; }
    }
}
