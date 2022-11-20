using Common.Constants;
using Common.Events.EntryCommentFavoriteEvent;
using Common.Events.EntryFavoriteEvent;
using Common.Infrastructure;
using FavoriteWorkerService.Services;

namespace FavoriteWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var favoriteService = new FavoriteService();

            var entryFavoriteExchangeName = CommonConstants.EntryFavoriteExchangeName;
            var createEntryFavoriteQueueName = CommonConstants.CreateEntryFavoriteQueueName;
            var deleteEntryFavoriteQueueName = CommonConstants.DeleteEntryFavoriteQueueName;

            QueryFactory.CreateBasicConsumer()
                 .EnsureExchange(entryFavoriteExchangeName)
                 .EnsureQueue(createEntryFavoriteQueueName, entryFavoriteExchangeName)
                 .Receive<CreateEntryFavoriteEvent>(async favorite =>
                 {
                     await favoriteService.CreateEntryFavorite(favorite);

                     _logger.LogInformation($"Received EntryId : {favorite.EntryId}");
                 })
                 .StartConsuming(createEntryFavoriteQueueName);

            QueryFactory.CreateBasicConsumer()
                .EnsureExchange(entryFavoriteExchangeName)
                .EnsureQueue(deleteEntryFavoriteQueueName, entryFavoriteExchangeName)
                .Receive<DeleteEntryFavoriteEvent>(async favorite =>
                {
                    await favoriteService.DeleteEntryFavorite(favorite);

                    _logger.LogInformation($"Received EntryId : {favorite.EntryId}");
                })
                .StartConsuming(deleteEntryFavoriteQueueName);

            var entryCommentFavoriteExchangeName = CommonConstants.EntryCommentFavoriteExchangeName;
            var createEntryCommentFavoriteQueueName = CommonConstants.CreateEntryCommentFavoriteQueueName;
            var deleteEntryCommentFavoriteQueueName = CommonConstants.DeleteEntryCommentFavoriteQueueName;

            QueryFactory.CreateBasicConsumer()
                .EnsureExchange(entryCommentFavoriteExchangeName)
                .EnsureQueue(createEntryCommentFavoriteQueueName, entryCommentFavoriteExchangeName)
                .Receive<CreateEntryCommentFavoriteEvent>(async favorite =>
                {
                    await favoriteService.CreateEntryCommentFavorite(favorite);

                    _logger.LogInformation($"Received EntryCommentId : {favorite.EntryCommentId}");
                })
                .StartConsuming(createEntryCommentFavoriteQueueName);

            QueryFactory.CreateBasicConsumer()
                .EnsureExchange(entryCommentFavoriteExchangeName)
                .EnsureQueue(deleteEntryCommentFavoriteQueueName, entryCommentFavoriteExchangeName)
                .Receive<DeleteEntryCommentFavoriteEvent>(async favorite =>
                {
                    await favoriteService.DeleteEntryCommentFavorite(favorite);

                    _logger.LogInformation($"Received EntryCommentId : {favorite.EntryCommentId}");
                })
                .StartConsuming(deleteEntryCommentFavoriteQueueName);

            return Task.CompletedTask;
        }
    }
}