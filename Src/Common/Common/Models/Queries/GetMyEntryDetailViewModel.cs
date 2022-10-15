namespace Common.Models.Queries
{
    public class GetMyEntryDetailViewModel
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int FavoritedCount { get; set; }
        public int UpVoteCount { get; set; }
        public int DownVoteCount { get; set; }
        public List<GetTagsViewModel> Tags { get; set; }
    }
}
