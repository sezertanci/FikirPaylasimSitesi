using MediatR;

namespace Common.Models.RequestModels.EntryCommentRequestModels.DeleteRequestModel
{
    public class DeleteEntryCommentFavoriteCommand : IRequest<bool>
    {
        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }

        public DeleteEntryCommentFavoriteCommand()
        {

        }

        public DeleteEntryCommentFavoriteCommand(Guid entryCommentId, Guid userId)
        {
            EntryCommentId = entryCommentId;
            UserId = userId;
        }
    }
}
