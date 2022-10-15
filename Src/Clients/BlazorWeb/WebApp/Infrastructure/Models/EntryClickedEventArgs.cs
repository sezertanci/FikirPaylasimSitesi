namespace WebApp.Infrastructure.Models
{
    public class EntryClickedEventArgs
    {
        public Guid EntryId { get; set; }
        public string EntrySubject { get; set; }
        public string EntryContent { get; set; }

        public EntryClickedEventArgs()
        {

        }

        public EntryClickedEventArgs(Guid entryId, string entrySubject, string entryContent)
        {
            EntryId = entryId;
            EntrySubject = entrySubject;
            EntryContent = entryContent;
        }
    }
}
