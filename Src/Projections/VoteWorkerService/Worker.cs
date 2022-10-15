using Common.Constants;
using Common.Events.EntryCommentVoteEvent;
using Common.Events.EntryVoteEvent;
using Common.Infrastructure;
using VoteWorkerService.Services;

namespace VoteWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration configuration;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            this.configuration = configuration;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var voteService = new VoteService(configuration["SqlServerConnectionString"]);

            var entryVoteExchangeName = CommonConstants.EntryVoteExchangeName;
            var createEntryVoteQueueName = CommonConstants.CreateEntryVoteQueueName;
            var deleteEntryVoteQueueName = CommonConstants.DeleteEntryVoteQueueName;

            QueryFactory.CreateBasicConsumer()
                .EnsureExchange(entryVoteExchangeName)
                .EnsureQueue(createEntryVoteQueueName, entryVoteExchangeName)
                .Receive<CreateEntryVoteEvent>(async vote =>
                {
                    await voteService.CreateEntryVote(vote);

                    _logger.LogInformation($"Received EntryId : {vote.EntryId} , VoteType : {vote.VoteType}");
                })
                .StartConsuming(createEntryVoteQueueName);

            QueryFactory.CreateBasicConsumer()
                .EnsureExchange(entryVoteExchangeName)
                .EnsureQueue(deleteEntryVoteQueueName, entryVoteExchangeName)
                .Receive<DeleteEntryVoteEvent>(async vote =>
                {
                    await voteService.DeleteEntryVote(vote);

                    _logger.LogInformation($"Received EntryId : {vote.EntryId}");
                })
                .StartConsuming(deleteEntryVoteQueueName);

            var entryCommentVoteExchangeName = CommonConstants.EntryCommentVoteExchangeName;
            var createEntryCommentVoteQueueName = CommonConstants.CreateEntryCommentVoteQueueName;
            var deleteEntryCommentVoteQueueName = CommonConstants.DeleteEntryCommentVoteQueueName;

            QueryFactory.CreateBasicConsumer()
                .EnsureExchange(entryCommentVoteExchangeName)
                .EnsureQueue(createEntryCommentVoteQueueName, entryCommentVoteExchangeName)
                .Receive<CreateEntryCommentVoteEvent>(async vote =>
                {
                    await voteService.CreateEntryCommentVote(vote);

                    _logger.LogInformation($"Received EntryId : {vote.EntryCommentId} , VoteType : {vote.VoteType}");
                })
                .StartConsuming(createEntryCommentVoteQueueName);

            QueryFactory.CreateBasicConsumer()
                .EnsureExchange(entryCommentVoteExchangeName)
                .EnsureQueue(deleteEntryCommentVoteQueueName, entryCommentVoteExchangeName)
                .Receive<DeleteEntryCommentVoteEvent>(async vote =>
                {
                    await voteService.DeleteEntryCommentVote(vote);

                    _logger.LogInformation($"Received EntryId : {vote.EntryCommentId}");
                })
                .StartConsuming(deleteEntryCommentVoteQueueName);

            return Task.CompletedTask;
        }
    }
}