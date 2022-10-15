namespace Domain.Models
{
    public class EntryTag : BaseEntity
    {
        public Guid EntryId { get; set; }
        public Guid TagId { get; set; }

        public EntryTag()
        {

        }

        public EntryTag(Guid id, Guid entryId, Guid tagId) : this()
        {
            Id = id;
            EntryId = entryId;
            TagId = tagId;
        }

        public virtual Entry Entry { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
