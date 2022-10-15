using Application.Interfaces.Repositories;
using Common.Models;
using Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.GetMyEntries
{
    public class GetMyEntryDetailQuery : IRequest<GetMyEntryDetailViewModel>
    {
        public Guid EntryId { get; set; }

        public GetMyEntryDetailQuery(Guid entryId)
        {
            EntryId = entryId;
        }

        public class GetMyEntryDetailQueryHandler : IRequestHandler<GetMyEntryDetailQuery, GetMyEntryDetailViewModel>
        {
            private readonly IEntryRepository entryRepository;

            public GetMyEntryDetailQueryHandler(IEntryRepository entryRepository)
            {
                this.entryRepository = entryRepository;
            }

            public async Task<GetMyEntryDetailViewModel> Handle(GetMyEntryDetailQuery request, CancellationToken cancellationToken)
            {
                var query = entryRepository.Query();

                if(request.EntryId != Guid.Empty)
                    query = query.Where(x => x.Id == request.EntryId);
                else return null;

                query = query.Include(x => x.EntryFavorites)
                             .Include(x => x.EntryVotes)
                             .Include(x => x.EntryTags)
                                .ThenInclude(x => x.Tag)
                             .OrderByDescending(x => x.CreatedDate);

                var response = query.Select(x => new GetMyEntryDetailViewModel()
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

                return await response.FirstOrDefaultAsync(cancellationToken: cancellationToken);
            }
        }
    }
}
