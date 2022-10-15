using Application.Interfaces.Repositories;
using Common.Models;
using Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.GetEntryDetail
{
    public class GetEntryDetailQuery : IRequest<GetEntryDetailViewModel>
    {
        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }

        public GetEntryDetailQuery(Guid entryId, Guid userId)
        {
            EntryId = entryId;
            UserId = userId;
        }

        public class GetEntryDetailQueryHandler : IRequestHandler<GetEntryDetailQuery, GetEntryDetailViewModel>
        {
            private readonly IEntryRepository entryRepository;

            public GetEntryDetailQueryHandler(IEntryRepository entryRepository)
            {
                this.entryRepository = entryRepository;
            }

            public async Task<GetEntryDetailViewModel> Handle(GetEntryDetailQuery request, CancellationToken cancellationToken)
            {
                var query = entryRepository.Query();

                query = query.Include(x => x.EntryFavorites)
                             .Include(x => x.EntryVotes)
                             .Include(x => x.User)
                             .Include(x => x.EntryTags)
                                .ThenInclude(x => x.Tag)
                             .Where(x => x.Id == request.EntryId);

                var result = query.Select(x => new GetEntryDetailViewModel
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

                return await result.FirstOrDefaultAsync(cancellationToken: cancellationToken);
            }
        }
    }
}
