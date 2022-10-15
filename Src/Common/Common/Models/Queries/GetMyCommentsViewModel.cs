namespace Common.Models.Queries
{
    public class GetMyCommentsViewModel
    {
        public GetEntryDetailViewModel EntryDetail { get; set; }
        public bool MyEntry { get; set; }
        public List<GetEntryCommentsViewModel> Comments { get; set; }
    }
}
