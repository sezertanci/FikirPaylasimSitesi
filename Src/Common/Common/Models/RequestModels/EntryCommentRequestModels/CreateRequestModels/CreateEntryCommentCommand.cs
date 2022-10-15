using MediatR;

namespace Common.Models.RequestModels.EntryCommentRequestModels.CreateRequestModels
{
    public class CreateEntryCommentCommand : IRequest<Guid>
    {
        public string Content { get; set; }
        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }

        public CreateEntryCommentCommand()
        {

        }

        public CreateEntryCommentCommand(string content, Guid entryId, Guid userId)
        {
            Content = content;
            EntryId = entryId;
            UserId = userId;
        }
    }
}
