using Application.Interfaces.Repositories;
using Common.Infrastructure.Extensions;
using Common.Models;
using Common.Models.Page;
using Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.GetMyEntries
{
    public class GetMyEntriesQuery : BasePagedQuery, IRequest<PagedViewModel<GetMyEntryDetailViewModel>>
    {
        public Guid UserId { get; set; }
        public GetMyEntriesQuery(Guid userId, int pageNumber, int pageSize) : base(pageNumber, pageSize)
        {
            UserId = userId;
        }

        public class GetMyEntriesQueryHandler : IRequestHandler<GetMyEntriesQuery, PagedViewModel<GetMyEntryDetailViewModel>>
        {
            private readonly IEntryRepository entryRepository;

            public GetMyEntriesQueryHandler(IEntryRepository entryRepository)
            {
                this.entryRepository = entryRepository;
            }

            public async Task<PagedViewModel<GetMyEntryDetailViewModel>> Handle(GetMyEntriesQuery request, CancellationToken cancellationToken)
            {
                var query = entryRepository.Query();

                if(request.UserId != Guid.Empty)
                    query = query.Where(x => x.UserId == request.UserId);
                else return null;

                query = query.Include(x => x.EntryFavorites)
                             .Include(x => x.EntryVotes)
                             .Include(x => x.EntryTags)
                                .ThenInclude(x => x.Tag)
                             .OrderByDescending(x => x.CreatedDate);

                var list = query.Select(x => new GetMyEntryDetailViewModel()
                {
                    Id = x.Id,
                    Content = x.Content,
                    CreatedDate = x.CreatedDate,
                    Subject = x.Subject,
                    FavoritedCount = x.EntryFavorites.Count,
                    DownVoteCount = x.EntryVotes.Where(x => x.VoteType == VoteType.DownVote).Count(),
                    UpVoteCount = x.EntryVotes.Where(x => x.VoteType == VoteType.UpVote).Count(),
                    Tags = x.EntryTags.Where(y => y.EntryId == x.Id).Select(entryTag => new GetTagsViewModel
                    {
                        Id = entryTag.Tag.Id,
                        Name = entryTag.Tag.Name
                    }).ToList()
                });

                var entries = await list.GetPaged(request.PageNumber, request.PageSize);

                return entries;
            }
        }
    }
}
