namespace Domain.Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool Deleted { get; set; }

        public BaseEntity()
        {

        }

        public BaseEntity(Guid id, DateTime createdDate, bool deleted) : this()
        {
            Id = id;
            CreatedDate = createdDate;
            Deleted = deleted;
        }
    }
}
