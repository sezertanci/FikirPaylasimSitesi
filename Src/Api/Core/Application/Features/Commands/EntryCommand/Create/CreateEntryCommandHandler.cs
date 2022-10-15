using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Models.RequestModels.EntryRequestModels.CreateRequestModels;
using Domain.Models;
using MediatR;

namespace Application.Features.Commands.EntryCommand.Create
{
    public class CreateEntryCommandHandler : IRequestHandler<CreateEntryCommand, Guid>
    {
        private readonly IMapper mapper;
        private readonly IEntryRepository entryRepository;

        public CreateEntryCommandHandler(IMapper mapper, IEntryRepository entryRepository)
        {
            this.mapper = mapper;
            this.entryRepository = entryRepository;
        }

        public async Task<Guid> Handle(CreateEntryCommand request, CancellationToken cancellationToken)
        {
            var entry = mapper.Map<Entry>(request);

            await entryRepository.AddAsync(entry);

            return entry.Id;
        }
    }
}
