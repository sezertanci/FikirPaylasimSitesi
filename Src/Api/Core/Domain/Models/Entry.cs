namespace Domain.Models
{
    public class Entry : BaseEntity
    {
        public string Subject { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }

        public Entry()
        {

        }

        public Entry(Guid id, Guid userId, string subject, string content) : this()
        {
            Id = id;
            UserId = userId;
            Subject = subject;
            Content = content;
        }

        public virtual User User { get; set; }
        public virtual ICollection<EntryTag> EntryTags { get; set; }
        public virtual ICollection<EntryVote> EntryVotes { get; set; }
        public virtual ICollection<EntryFavorite> EntryFavorites { get; set; }
        public virtual ICollection<EntryComment> EntryComments { get; set; }

    }
}
