using Common.Events.EntryCommentVoteEvent;
using Common.Events.EntryVoteEvent;
using Dapper;
using Microsoft.Data.SqlClient;

namespace VoteWorkerService.Services
{
    public class VoteService
    {
        private readonly string connectionString;

        public VoteService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task CreateEntryVote(CreateEntryVoteEvent createEntryVoteEvent)
        {
            var deleteEntryVoteEvent = new DeleteEntryVoteEvent
            {
                EntryId = createEntryVoteEvent.EntryId,
                UserId = createEntryVoteEvent.UserId,
            };

            await DeleteEntryVote(deleteEntryVoteEvent);

            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("INSERT INTO EntryVote (Id, EntryId, UserId, VoteType, CreatedDate, Deleted) VALUES (@Id, @EntryId, @UserId, @VoteType, GETDATE(), 0)",
               new
               {
                   Id = Guid.NewGuid(),
                   createEntryVoteEvent.EntryId,
                   createEntryVoteEvent.UserId,
                   createEntryVoteEvent.VoteType
               });
        }

        public async Task DeleteEntryVote(DeleteEntryVoteEvent deleteEntryVoteEvent)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("DELETE FROM EntryVote WHERE EntryId = @EntryId AND UserId = @UserId",
               new
               {
                   deleteEntryVoteEvent.EntryId,
                   deleteEntryVoteEvent.UserId
               });
        }

        public async Task CreateEntryCommentVote(CreateEntryCommentVoteEvent createEntryCommentVoteEvent)
        {
            var deleteEntryCommentVoteEvent = new DeleteEntryCommentVoteEvent
            {
                EntryCommentId = createEntryCommentVoteEvent.EntryCommentId,
                UserId = createEntryCommentVoteEvent.UserId,
            };

            await DeleteEntryCommentVote(deleteEntryCommentVoteEvent);

            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("INSERT INTO EntryCommentVote (Id, EntryCommentId, UserId, VoteType, CreatedDate, Deleted) VALUES (@Id, @EntryCommentId, @UserId, @VoteType, GETDATE(), 0)",
                new
                {
                    Id = Guid.NewGuid(),
                    createEntryCommentVoteEvent.EntryCommentId,
                    createEntryCommentVoteEvent.UserId,
                    createEntryCommentVoteEvent.VoteType
                });
        }

        public async Task DeleteEntryCommentVote(DeleteEntryCommentVoteEvent deleteEntryCommentVoteEvent)
        {
            using var connections = new SqlConnection(connectionString);

            await connections.ExecuteAsync("DELETE FROM EntryCommentVote WHERE EntryCommentId = @EntryCommentId AND UserId = @UserId",
                new
                {
                    deleteEntryCommentVoteEvent.EntryCommentId,
                    deleteEntryCommentVoteEvent.UserId
                });
        }
    }
}
