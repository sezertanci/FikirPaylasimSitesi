using MediatR;

namespace Common.Models.RequestModels.EntryRequestModels.CreateRequestModels
{
    public class CreateEntryCommand : IRequest<Guid>
    {
        public string Subject { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }

        public CreateEntryCommand()
        {

        }

        public CreateEntryCommand(string subject, string content, Guid userId)
        {
            Subject = subject;
            Content = content;
            UserId = userId;
        }
    }
}
