using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Models.Queries;
using MediatR;

namespace Application.Queries.GetTags
{
    public class GetTagsQuery : IRequest<List<GetTagsViewModel>>
    {
        public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, List<GetTagsViewModel>>
        {
            private readonly IMapper mapper;
            private readonly ITagRepository tagRepository;

            public GetTagsQueryHandler(IMapper mapper, ITagRepository tagRepository)
            {
                this.mapper = mapper;
                this.tagRepository = tagRepository;
            }

            public async Task<List<GetTagsViewModel>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
            {
                var tags = await tagRepository.GetAllAsync();

                return mapper.Map(tags, new List<GetTagsViewModel>());
            }
        }
    }
}
