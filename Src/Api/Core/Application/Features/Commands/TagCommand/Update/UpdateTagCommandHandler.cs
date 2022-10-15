using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Models.RequestModels.TagRequestModels.UpdateRequestModels;
using Domain.Models;
using MediatR;

namespace Application.Features.Commands.TagCommand.Update
{
    public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, bool>
    {
        private readonly IMapper mapper;
        private readonly ITagRepository tagRepository;

        public UpdateTagCommandHandler(IMapper mapper, ITagRepository tagRepository)
        {
            this.mapper = mapper;
            this.tagRepository = tagRepository;
        }

        public async Task<bool> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var tag = mapper.Map<Tag>(request);

            var tagData = await tagRepository.GetFirstOrDefaultAsync(x => x.Id != tag.Id && x.Name == tag.Name);

            if(tagData is not null)
                return false;

            var result = await tagRepository.UpdateAsync(tag);

            return result != 0;
        }
    }
}
