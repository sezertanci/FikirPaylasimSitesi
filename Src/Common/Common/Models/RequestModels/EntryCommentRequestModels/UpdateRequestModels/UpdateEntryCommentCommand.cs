using MediatR;

namespace Common.Models.RequestModels.EntryCommentRequestModels.UpdateRequestModels
{
    public class UpdateEntryCommentCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }

        public UpdateEntryCommentCommand()
        {

        }

        public UpdateEntryCommentCommand(Guid id, string content, Guid userId)
        {
            Id = id;
            Content = content;
            UserId = userId;
        }
    }
}
