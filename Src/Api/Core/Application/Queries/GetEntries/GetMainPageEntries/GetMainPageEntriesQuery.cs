using Application.Interfaces.Repositories;
using Common.Infrastructure.Extensions;
using Common.Models;
using Common.Models.Page;
using Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.GetEntries.GetMainPageEntries
{
    public class GetMainPageEntriesQuery : BasePagedQuery, IRequest<PagedViewModel<GetEntryDetailViewModel>>
    {
        public Guid UserId { get; set; }
        public Guid TagId { get; set; }

        public GetMainPageEntriesQuery(Guid userId, int pageNumber, int pageSize) : base(pageNumber, pageSize)
        {
            UserId = userId;
        }

        public class GetMainPageEntriesQueryHandler : IRequestHandler<GetMainPageEntriesQuery, PagedViewModel<GetEntryDetailViewModel>>
        {
            private readonly IEntryRepository entryRepository;

            public GetMainPageEntriesQueryHandler(IEntryRepository entryRepository)
            {
                this.entryRepository = entryRepository;
            }

            public Task<PagedViewModel<GetEntryDetailViewModel>> Handle(GetMainPageEntriesQuery request, CancellationToken cancellationToken)
            {
                var query = entryRepository.Query();

                if(request.TagId != Guid.Empty)
                    query = query.Include(x => x.EntryTags.Where(y => y.TagId == request.TagId));

                query = query.Include(x => x.EntryFavorites)
                             .Include(x => x.EntryVotes)
                             .Include(x => x.User)
                             .Include(x => x.EntryTags)
                                .ThenInclude(x => x.Tag)
                             .OrderByDescending(x => x.CreatedDate);

                var list = query.Select(x => new GetEntryDetailViewModel
                {
                    Id = x.Id,
                    Content = x.Content,
                    Subject = x.Subject,
                    CreatedDate = x.CreatedDate,
                    Favorited = request.UserId != Guid.Empty && x.EntryFavorites.Any(y => y.UserId == request.UserId),
                    FavoritedCount = x.EntryFavorites.Count,
                    MyEntry = request.UserId != Guid.Empty && x.UserId == request.UserId,
                    UserId = x.User.Id,
                    UserName = x.User.UserName,
                    Tags = x.EntryTags.Where(entryTag => entryTag.EntryId == x.Id).Select(entryTag => new GetTagsViewModel
                    {
                        Id = entryTag.Tag.Id,
                        Name = entryTag.Tag.Name
                    }).ToList(),
                    VoteType = request.UserId != Guid.Empty && x.EntryVotes.Any(y => y.UserId == request.UserId) ? x.EntryVotes.FirstOrDefault(y => y.UserId == request.UserId).VoteType : VoteType.None
                });

                var entries = list.GetPaged(request.PageNumber, request.PageSize);

                return entries;
            }
        }
    }
}
