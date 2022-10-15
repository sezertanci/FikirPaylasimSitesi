using Application.Interfaces.Repositories;
using Common.Infrastructure.Extensions;
using Common.Models.Page;
using Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.GetUserEntries
{
    public class GetUserEntriesQuery : BasePagedQuery, IRequest<PagedViewModel<GetUserEntriesDetailViewModel>>
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }

        public GetUserEntriesQuery(Guid userId, string userName, int pageNumber = 1, int pageSize = 10) : base(pageNumber, pageSize)
        {
            UserId = userId;
            UserName = userName;
        }

        public class GetUserEntriesQueryHandler : IRequestHandler<GetUserEntriesQuery, PagedViewModel<GetUserEntriesDetailViewModel>>
        {
            private readonly IEntryRepository entryRepository;

            public GetUserEntriesQueryHandler(IEntryRepository entryRepository)
            {
                this.entryRepository = entryRepository;
            }

            public async Task<PagedViewModel<GetUserEntriesDetailViewModel>> Handle(GetUserEntriesQuery request, CancellationToken cancellationToken)
            {
                var query = entryRepository.Query();

                if(request.UserId != Guid.Empty)
                    query = query.Where(x => x.UserId == request.UserId);
                else if(!string.IsNullOrEmpty(request.UserName))
                    query = query.Where(x => x.User.UserName == request.UserName);
                else return null;

                query = query.Include(x => x.EntryFavorites)
                             .Include(x => x.User)
                             .OrderByDescending(x => x.CreatedDate);

                var list = query.Select(x => new GetUserEntriesDetailViewModel()
                {
                    Id = x.Id,
                    Content = x.Content,
                    CreatedDate = x.CreatedDate,
                    Favorited = false,
                    Subject = x.Subject,
                    FavoritedCount = x.EntryFavorites.Count,
                    UserId = x.UserId,
                    UserName = x.User.UserName
                });

                var entries = await list.GetPaged(request.PageNumber, request.PageSize);

                return entries;
            }
        }
    }
}
