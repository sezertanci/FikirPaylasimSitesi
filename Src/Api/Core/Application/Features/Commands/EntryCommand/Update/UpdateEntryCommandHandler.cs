using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Models.RequestModels.EntryRequestModels.UpdateRequestModels;
using MediatR;

namespace Application.Features.Commands.EntryCommand.Update
{
    public class UpdateEntryCommandHandler : IRequestHandler<UpdateEntryCommand, bool>
    {
        private readonly IMapper mapper;
        private readonly IEntryRepository entryRepository;

        public UpdateEntryCommandHandler(IMapper mapper, IEntryRepository entryRepository)
        {
            this.mapper = mapper;
            this.entryRepository = entryRepository;
        }

        public async Task<bool> Handle(UpdateEntryCommand request, CancellationToken cancellationToken)
        {
            var entry = await entryRepository.GetFirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if(entry is null)
                return false;

            mapper.Map(request, entry);

            var result = await entryRepository.UpdateAsync(entry);

            return result != 0;
        }
    }
}
