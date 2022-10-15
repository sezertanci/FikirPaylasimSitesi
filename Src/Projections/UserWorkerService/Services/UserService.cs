using Common.Events.UserEvent;
using Dapper;
using Microsoft.Data.SqlClient;

namespace UserWorkerService.Services
{
    public class UserService
    {
        private readonly string connectionString;

        public UserService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<Guid> CreateEmailConfirmation(UserEmailChangedEvent userEmailChangedEvent)
        {
            using var connection = new SqlConnection(connectionString);

            var guid = Guid.NewGuid();

            await connection.ExecuteAsync("INSERT INTO EmailConfirmation (Id, OldEmailAddress, NewEmailAddress, CreatedDate, Deleted) VALUES (@Id, @OldEmailAddress, @NewEmailAddress, GETDATE(), 0)",
                new
                {
                    Id = guid,
                    userEmailChangedEvent.OldEmailAddress,
                    userEmailChangedEvent.NewEmailAddress
                });

            return guid;
        }
    }
}
