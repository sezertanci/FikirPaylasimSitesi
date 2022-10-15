namespace Domain.Models
{
    public class EmailConfirmation : BaseEntity
    {
        public string OldEmailAddress { get; set; }
        public string NewEmailAddress { get; set; }

        public EmailConfirmation()
        {

        }

        public EmailConfirmation(Guid id, string oldEmailAddress, string newEmailAddress)
        {
            Id = id;
            OldEmailAddress = oldEmailAddress;
            NewEmailAddress = newEmailAddress;
        }
    }
}
