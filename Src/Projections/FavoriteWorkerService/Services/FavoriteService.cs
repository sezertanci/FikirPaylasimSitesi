using Common.Events.EntryCommentFavoriteEvent;
using Common.Events.EntryFavoriteEvent;
using Dapper;
using Microsoft.Data.SqlClient;

namespace FavoriteWorkerService.Services
{
    public class FavoriteService
    {
        private readonly string connectionString;

        public FavoriteService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task CreateEntryFavorite(CreateEntryFavoriteEvent createEntryFavoriteEvent)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("INSERT INTO EntryFavorite (Id, EntryId, UserId, CreatedDate, Deleted) VALUES(@Id, @EntryId, @UserId, GETDATE(), 0)",
                new
                {
                    Id = Guid.NewGuid(),
                    createEntryFavoriteEvent.EntryId,
                    createEntryFavoriteEvent.UserId
                });
        }

        public async Task DeleteEntryFavorite(DeleteEntryFavoriteEvent deleteEntryFavoriteEvent)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("DELETE FROM EntryFavorite WHERE EntryId = @EntryId AND UserId = @UserId",
                new
                {
                    deleteEntryFavoriteEvent.EntryId,
                    deleteEntryFavoriteEvent.UserId
                });
        }

        public async Task CreateEntryCommentFavorite(CreateEntryCommentFavoriteEvent createEntryCommentFavoriteEvent)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("INSERT INTO EntryCommentFavorite (Id, EntryCommentId, UserId, CreatedDate, Deleted) VALUES(@Id, @EntryCommentId, @UserId, GETDATE(), 0)",
                new
                {
                    Id = Guid.NewGuid(),
                    createEntryCommentFavoriteEvent.EntryCommentId,
                    createEntryCommentFavoriteEvent.UserId
                });
        }

        public async Task DeleteEntryCommentFavorite(DeleteEntryCommentFavoriteEvent deleteEntryCommentFavoriteEvent)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("DELETE FROM EntryCommentFavorite WHERE EntryCommentId = @EntryCommentId AND UserId = @UserId",
                new
                {
                    deleteEntryCommentFavoriteEvent.EntryCommentId,
                    deleteEntryCommentFavoriteEvent.UserId
                });
        }
    }
}
