using MediatR;

namespace Common.Models.RequestModels.EntryCommentRequestModels.DeleteRequestModel
{
    public class DeleteEntryCommentCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public DeleteEntryCommentCommand()
        {

        }

        public DeleteEntryCommentCommand(Guid id, Guid userId)
        {
            Id = id;
            UserId = userId;
        }
    }
}
