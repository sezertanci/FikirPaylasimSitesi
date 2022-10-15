using MediatR;

namespace Common.Models.RequestModels.EntryRequestModels.UpdateRequestModels
{
    public class UpdateEntryCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }

        public UpdateEntryCommand()
        {

        }

        public UpdateEntryCommand(Guid id, string subject, string content, Guid userId)
        {
            Id = id;
            Subject = subject;
            Content = content;
            UserId = userId;
        }
    }
}
