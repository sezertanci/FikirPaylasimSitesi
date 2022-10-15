using MediatR;

namespace Common.Models.RequestModels.EntryCommentRequestModels.DeleteRequestModel
{
    public class DeleteEntryCommentVoteCommand : IRequest<bool>
    {
        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }

        public DeleteEntryCommentVoteCommand()
        {

        }

        public DeleteEntryCommentVoteCommand(Guid entryCommentId, Guid userId)
        {
            EntryCommentId = entryCommentId;
            UserId = userId;
        }
    }
}
