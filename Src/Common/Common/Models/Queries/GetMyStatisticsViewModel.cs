namespace Common.Models.Queries
{
    public class GetMyStatisticsViewModel
    {
        public int MyEntriesCount { get; set; }
        public int CommentsOfMyEntriesCount { get; set; }
        public int FavoritesOfMyEntriesCount { get; set; }
        public int UpvotesOfMyEntriesCount { get; set; }
        public int DownvotesOfMyEntriesCount { get; set; }
        public int MyFavoriteEntriesCount { get; set; }

        public int MyCommentsCount { get; set; }
        public int FavoritesOfMyCommentsCount { get; set; }
        public int UpvotesOfMyCommentsCount { get; set; }
        public int DownvotesOfMyCommentsCount { get; set; }
        public int MyFavoriteCommentsCount { get; set; }
    }
}
