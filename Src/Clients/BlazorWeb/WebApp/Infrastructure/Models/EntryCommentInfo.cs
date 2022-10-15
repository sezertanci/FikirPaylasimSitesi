namespace WebApp.Infrastructure.Models
{
    public class EntryCommentInfo
    {
        public Guid EntryCommentId { get; set; }
        public string EntryCommentContent { get; set; }
        public string EntrySubject { get; set; }
        public bool MyComment { get; set; }
    }
}
