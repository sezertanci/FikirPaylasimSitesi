using MediatR;

namespace Common.Models.RequestModels.EntryRequestModels.DeleteRequestModels
{
    public class DeleteEntryCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public DeleteEntryCommand()
        {

        }

        public DeleteEntryCommand(Guid id, Guid userId)
        {
            Id = id;
            UserId = userId;
        }
    }
}
