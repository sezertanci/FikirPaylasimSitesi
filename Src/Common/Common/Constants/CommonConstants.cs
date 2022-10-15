namespace Common.Constants
{
    public class CommonConstants
    {
#if DEBUG
        public const string RabbitMQHost = "localhost";
#else
        public const string RabbitMQHost = "localhost";
#endif

        public const string DefaultExchangeType = "direct";

        public const string UserExchangeName = "UserExchange";
        public const string UserEmailChangedQueueName = "UserEmailChangedQueue";

        public const string EntryVoteExchangeName = "EntryVoteExchange";
        public const string CreateEntryVoteQueueName = "CreateEntryVoteQueue";
        public const string DeleteEntryVoteQueueName = "DeleteEntryVoteQueue";

        public const string EntryFavoriteExchangeName = "EntryFavoriteExchange";
        public const string CreateEntryFavoriteQueueName = "CreateEntryFavoriteQueue";
        public const string DeleteEntryFavoriteQueueName = "DeleteEntryFavoriteQueue";

        public const string EntryCommentFavoriteExchangeName = "EntryCommentFavoriteExchange";
        public const string CreateEntryCommentFavoriteQueueName = "CreateEntryCommentFavoriteQueue";
        public const string DeleteEntryCommentFavoriteQueueName = "DeleteEntryCommentFavoriteQueue";

        public const string EntryCommentVoteExchangeName = "EntryCommentVoteExchange";
        public const string CreateEntryCommentVoteQueueName = "CreateEntryCommentVoteQueue";
        public const string DeleteEntryCommentVoteQueueName = "DeleteEntryCommentVoteQueue";
    }
}
