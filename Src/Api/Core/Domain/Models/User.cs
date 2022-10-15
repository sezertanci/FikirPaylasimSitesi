namespace Domain.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public bool EmailConfirmed { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordSalt { get; set; }

        public User()
        {

        }

        public User(Guid id, string firstName, string lastName, string userName, string emailAddress, byte[] password, byte[] passwordSalt) : this()
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            EmailAddress = emailAddress;
            Password = password;
            PasswordSalt = passwordSalt;
        }

        public virtual ICollection<Entry> Entries { get; set; }
        public virtual ICollection<EntryComment> EntryComments { get; set; }
        public virtual ICollection<EntryFavorite> EntryFavorites { get; set; }
        public virtual ICollection<EntryCommentFavorite> EntryCommentFavorites { get; set; }
    }
}
