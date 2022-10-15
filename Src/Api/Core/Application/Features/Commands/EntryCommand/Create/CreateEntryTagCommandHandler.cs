using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Models.RequestModels.EntryRequestModels.CreateRequestModels;
using Domain.Models;
using MediatR;

namespace Application.Features.Commands.EntryCommand.Create
{
    public class CreateEntryTagCommandHandler : IRequestHandler<CreateEntryTagCommand, bool>
    {
        private readonly IMapper mapper;
        private readonly IEntryTagRepository entryTagRepository;

        public CreateEntryTagCommandHandler(IMapper mapper, IEntryTagRepository entryTagRepository)
        {
            this.mapper = mapper;
            this.entryTagRepository = entryTagRepository;
        }

        public async Task<bool> Handle(CreateEntryTagCommand request, CancellationToken cancellationToken)
        {
            var entryTag = mapper.Map<EntryTag>(request);

            var entryTagData = await entryTagRepository.GetFirstOrDefaultAsync(x => x.TagId == entryTag.TagId && x.EntryId == entryTag.EntryId);

            if(entryTagData != null)
                return false;

            var result = await entryTagRepository.AddAsync(entryTag);

            return result != 0;
        }
    }
}
