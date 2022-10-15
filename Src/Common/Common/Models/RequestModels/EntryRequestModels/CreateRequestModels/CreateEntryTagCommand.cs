using MediatR;

namespace Common.Models.RequestModels.EntryRequestModels.CreateRequestModels
{
    public class CreateEntryTagCommand : IRequest<bool>
    {
        public Guid EntryId { get; set; }
        public Guid TagId { get; set; }

        public CreateEntryTagCommand()
        {

        }

        public CreateEntryTagCommand(Guid entryId, Guid tagId)
        {
            EntryId = entryId;
            TagId = tagId;
        }
    }
}
