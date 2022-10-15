namespace Common.Models.Queries
{
    public class GetEntryDetailViewModel : BaseFooterRateFavoritedViewModel
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public List<GetTagsViewModel> Tags { get; set; }
        public bool MyEntry { get; set; }
    }
}
