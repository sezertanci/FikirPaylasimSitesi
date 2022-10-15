using MediatR;

namespace Common.Models.RequestModels.EntryCommentRequestModels.CreateRequestModels
{
    public class CreateEntryCommentFavoriteCommand : IRequest<bool>
    {
        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }

        public CreateEntryCommentFavoriteCommand()
        {

        }

        public CreateEntryCommentFavoriteCommand(Guid entryCommentId, Guid userId)
        {
            EntryCommentId = entryCommentId;
            UserId = userId;
        }
    }
}
