using MediatR;

namespace Common.Models.RequestModels.EntryRequestModels.CreateRequestModels
{
    public class CreateEntryFavoriteCommand : IRequest<bool>
    {
        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }

        public CreateEntryFavoriteCommand()
        {

        }

        public CreateEntryFavoriteCommand(Guid entryId, Guid userId)
        {
            EntryId = entryId;
            UserId = userId;
        }
    }
}
