namespace Domain.Models
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }

        public Tag()
        {

        }

        public Tag(Guid id, string name) : this()
        {
            Id = id;
            Name = name;
        }

        public virtual ICollection<EntryTag> EntryTags { get; set; }
    }
}
