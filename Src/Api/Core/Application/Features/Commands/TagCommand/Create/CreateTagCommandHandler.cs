using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Models.RequestModels.TagRequestModels.CreateRequestModels;
using Domain.Models;
using MediatR;

namespace Application.Features.Commands.TagCommand.Create
{
    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Guid>
    {
        private readonly IMapper mapper;
        private readonly ITagRepository tagRepository;

        public CreateTagCommandHandler(IMapper mapper, ITagRepository tagRepository)
        {
            this.mapper = mapper;
            this.tagRepository = tagRepository;
        }

        public async Task<Guid> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var tag = mapper.Map<Tag>(request);

            await tagRepository.AddAsync(tag);

            return tag.Id;
        }
    }
}
