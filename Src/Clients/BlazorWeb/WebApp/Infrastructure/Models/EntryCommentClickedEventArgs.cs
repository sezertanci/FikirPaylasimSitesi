namespace WebApp.Infrastructure.Models
{
    public class EntryCommentClickedEventArgs
    {
        public Guid EntryCommentId { get; set; }
        public string EntryCommentContent { get; set; }

        public EntryCommentClickedEventArgs()
        {

        }

        public EntryCommentClickedEventArgs(Guid entryCommentId, string entryCommentContent)
        {
            EntryCommentId = entryCommentId;
            EntryCommentContent = entryCommentContent;
        }
    }
}
