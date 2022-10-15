using MediatR;

namespace Common.Models.RequestModels.EntryRequestModels.DeleteRequestModels
{
    public class DeleteEntryTagCommand : IRequest<bool>
    {
        public Guid EntryId { get; set; }
        public Guid TagId { get; set; }

        public DeleteEntryTagCommand()
        {

        }

        public DeleteEntryTagCommand(Guid entryId, Guid tagId)
        {
            EntryId = entryId;
            TagId = tagId;
        }
    }
}
