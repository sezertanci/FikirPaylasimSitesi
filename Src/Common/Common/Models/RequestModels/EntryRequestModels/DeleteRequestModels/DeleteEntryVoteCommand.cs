using MediatR;

namespace Common.Models.RequestModels.EntryRequestModels.DeleteRequestModels
{
    public class DeleteEntryVoteCommand : IRequest<bool>
    {
        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }

        public DeleteEntryVoteCommand()
        {

        }

        public DeleteEntryVoteCommand(Guid entryId, Guid userId)
        {
            EntryId = entryId;
            UserId = userId;
        }
    }
}
