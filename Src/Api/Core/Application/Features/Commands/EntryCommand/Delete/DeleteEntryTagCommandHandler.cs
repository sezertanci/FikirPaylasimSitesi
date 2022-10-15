using Application.Interfaces.Repositories;
using Common.Models.RequestModels.EntryRequestModels.DeleteRequestModels;
using MediatR;

namespace Application.Features.Commands.EntryCommand.Delete
{
    public class DeleteEntryTagCommandHandler : IRequestHandler<DeleteEntryTagCommand, bool>
    {
        private readonly IEntryTagRepository entryTagRepository;

        public DeleteEntryTagCommandHandler(IEntryTagRepository entryTagRepository)
        {
            this.entryTagRepository = entryTagRepository;
        }

        public async Task<bool> Handle(DeleteEntryTagCommand request, CancellationToken cancellationToken)
        {
            var entryTag = await entryTagRepository.GetFirstOrDefaultAsync(x => x.TagId == request.TagId && x.EntryId == request.EntryId);

            if(entryTag == null)
                return false;
            else
            {
                entryTag.Deleted = true;

                var result = await entryTagRepository.UpdateAsync(entryTag);

                return result != 0;
            }
        }
    }
}
