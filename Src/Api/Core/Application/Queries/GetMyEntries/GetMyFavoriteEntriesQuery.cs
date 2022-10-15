using Application.Interfaces.Repositories;
using Common.Infrastructure.Extensions;
using Common.Models;
using Common.Models.Page;
using Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.GetMyEntries
{
    public class GetMyFavoriteEntriesQuery : BasePagedQuery, IRequest<PagedViewModel<GetEntryDetailViewModel>>
    {
        public Guid UserId { get; set; }
        public GetMyFavoriteEntriesQuery(Guid userId, int pageNumber, int pageSize) : base(pageNumber, pageSize)
        {
            UserId = userId;
        }

        public class GetMyFavoriteEntriesQueryHandler : IRequestHandler<GetMyFavoriteEntriesQuery, PagedViewModel<GetEntryDetailViewModel>>
        {
            private readonly IEntryFavoriteRepository entryFavoriteRepository;

            public GetMyFavoriteEntriesQueryHandler(IEntryFavoriteRepository entryFavoriteRepository)
            {
                this.entryFavoriteRepository = entryFavoriteRepository;
            }

            public Task<PagedViewModel<GetEntryDetailViewModel>> Handle(GetMyFavoriteEntriesQuery request, CancellationToken cancellationToken)
            {
                var query = entryFavoriteRepository.Query();

                query = query.Where(x => x.UserId == request.UserId && !x.Entry.Deleted)
                             .Include(x => x.Entry)
                                .ThenInclude(x => x.EntryVotes)
                             .Include(x => x.Entry)
                                .ThenInclude(x => x.User)
                             .OrderByDescending(x => x.CreatedDate);

                var list = query.Select(x => new GetEntryDetailViewModel
                {
                    Id = x.Entry.Id,
                    Content = x.Entry.Content,
                    Subject = x.Entry.Subject,
                    CreatedDate = x.Entry.CreatedDate,
                    Favorited = request.UserId != Guid.Empty && x.Entry.EntryFavorites.Any(y => y.UserId == request.UserId),
                    FavoritedCount = x.Entry.EntryFavorites.Count,
                    MyEntry = request.UserId != Guid.Empty && x.Entry.UserId == request.UserId,
                    UserId = x.Entry.User.Id,
                    UserName = x.Entry.User.UserName,
                    VoteType = request.UserId != Guid.Empty && x.Entry.EntryVotes.Any(y => y.UserId == request.UserId) ? x.Entry.EntryVotes.FirstOrDefault(y => y.UserId == request.UserId).VoteType : VoteType.None
                });

                var entries = list.GetPaged(request.PageNumber, request.PageSize);

                return entries;
            }
        }
    }
}
