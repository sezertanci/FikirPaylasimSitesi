using Application.Interfaces.Repositories;
using Common.Models.RequestModels.EntryRequestModels.DeleteRequestModels;
using MediatR;

namespace Application.Features.Commands.EntryCommand.Delete
{
    public class DeleteEntryCommandHandler : IRequestHandler<DeleteEntryCommand, bool>
    {
        private readonly IEntryRepository entryRepository;

        public DeleteEntryCommandHandler(IEntryRepository entryRepository)
        {
            this.entryRepository = entryRepository;
        }

        public async Task<bool> Handle(DeleteEntryCommand request, CancellationToken cancellationToken)
        {
            var entry = await entryRepository.GetFirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if(entry is null)
                return false;

            entry.Deleted = true;

            await entryRepository.UpdateAsync(entry);

            return true;
        }
    }
}
