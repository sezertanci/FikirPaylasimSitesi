using Application.Interfaces.Repositories;
using Common.Infrastructure.Extensions;
using Common.Models.Page;
using Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.GetEntries
{
    public class GetEntriesQuery : BasePagedQuery, IRequest<PagedViewModel<GetEntriesViewModel>>
    {
        public GetEntriesQuery(Guid tagId, int pageNumber = 1, int pageSize = 10) : base(pageNumber, pageSize)
        {
            TagId = tagId;
        }

        public Guid TagId { get; set; }

        public class GetEntriesQueryHandler : IRequestHandler<GetEntriesQuery, PagedViewModel<GetEntriesViewModel>>
        {
            private readonly IEntryRepository entryRepository;
            private readonly IEntryTagRepository entryTagRepository;

            public GetEntriesQueryHandler(IEntryRepository entryRepository, IEntryTagRepository entryTagRepository)
            {
                this.entryRepository = entryRepository;
                this.entryTagRepository = entryTagRepository;
            }

            public async Task<PagedViewModel<GetEntriesViewModel>> Handle(GetEntriesQuery request, CancellationToken cancellationToken)
            {
                var query = entryRepository.Query();
                var queryTag = entryTagRepository.Query();

                if(request.TagId != Guid.Empty)
                {
                    var entryIds = queryTag.Where(x => x.TagId == request.TagId).Select(x => x.EntryId);

                    query = query.Where(x => entryIds.Contains(x.Id));
                }

                query = query.Include(x => x.EntryComments).OrderByDescending(x => x.EntryComments.Count);

                var list = query.Select(x => new GetEntriesViewModel
                {
                    CommentCount = x.EntryComments.Count,
                    Id = x.Id,
                    Subject = x.Subject
                });

                return await list.GetPaged(request.PageNumber, request.PageSize);
            }
        }
    }
}
