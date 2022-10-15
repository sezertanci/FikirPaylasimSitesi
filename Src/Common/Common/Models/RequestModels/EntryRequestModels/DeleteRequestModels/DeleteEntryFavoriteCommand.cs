using MediatR;

namespace Common.Models.RequestModels.EntryRequestModels.DeleteRequestModels
{
    public class DeleteEntryFavoriteCommand : IRequest<bool>
    {
        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }

        public DeleteEntryFavoriteCommand()
        {

        }

        public DeleteEntryFavoriteCommand(Guid entryId, Guid userId)
        {
            EntryId = entryId;
            UserId = userId;
        }
    }
}
