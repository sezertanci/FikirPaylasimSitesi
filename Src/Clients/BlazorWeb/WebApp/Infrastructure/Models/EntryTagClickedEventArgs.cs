namespace WebApp.Infrastructure.Models
{
    public class EntryTagClickedEventArgs
    {
        public Guid EntryId { get; set; }
        public Guid TagId { get; set; }

        public EntryTagClickedEventArgs()
        {

        }

        public EntryTagClickedEventArgs(Guid entryId, Guid tagId)
        {
            EntryId = entryId;
            TagId = tagId;
        }
    }
}
