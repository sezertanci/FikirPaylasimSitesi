using Application.Interfaces.Repositories;
using Common.Models;
using Common.Models.Queries;
using MediatR;

namespace Application.Queries.GetMyStatistics
{
    public class GetMyStatisticsQuery : IRequest<GetMyStatisticsViewModel>
    {
        public Guid UserId { get; set; }

        public GetMyStatisticsQuery(Guid userId)
        {
            UserId = userId;
        }

        public class GetMyStatisticsQueryHandler : IRequestHandler<GetMyStatisticsQuery, GetMyStatisticsViewModel>
        {
            private readonly IEntryRepository entryRepository;
            private readonly IEntryCommentRepository entryCommentRepository;
            private readonly IEntryFavoriteRepository entryFavoriteRepository;
            private readonly IEntryVoteRepository entryVoteRepository;
            private readonly IEntryCommentFavoriteRepository entryCommentFavoriteRepository;
            private readonly IEntryCommentVoteRepository entryCommentVoteRepository;

            public GetMyStatisticsQueryHandler(IEntryRepository entryRepository, IEntryCommentRepository entryCommentRepository, IEntryFavoriteRepository entryFavoriteRepository, IEntryVoteRepository entryVoteRepository, IEntryCommentFavoriteRepository entryCommentFavoriteRepository, IEntryCommentVoteRepository entryCommentVoteRepository)
            {
                this.entryRepository = entryRepository;
                this.entryCommentRepository = entryCommentRepository;
                this.entryFavoriteRepository = entryFavoriteRepository;
                this.entryVoteRepository = entryVoteRepository;
                this.entryCommentFavoriteRepository = entryCommentFavoriteRepository;
                this.entryCommentVoteRepository = entryCommentVoteRepository;
            }

            public async Task<GetMyStatisticsViewModel> Handle(GetMyStatisticsQuery request, CancellationToken cancellationToken)
            {
                var entryQuery = entryRepository.Query();
                var entryFavoriteQuery = entryFavoriteRepository.Query();
                var entryVoteQuery = entryVoteRepository.Query();
                var entryCommentQuery = entryCommentRepository.Query();
                var entryCommentFavoriteQuery = entryCommentFavoriteRepository.Query();
                var entryCommentVoteQuery = entryCommentVoteRepository.Query();

                var myEntryIds = entryQuery.Where(x => x.UserId == request.UserId).Select(x => x.Id);
                var myEntriesVotes = entryVoteQuery.Where(x => myEntryIds.Contains(x.EntryId)).ToList();

                var myEntryCommentIds = entryCommentQuery.Where(x => x.UserId == request.UserId).Select(x => x.Id);
                var myEntryCommentsVotes = entryCommentVoteQuery.Where(x => myEntryCommentIds.Contains(x.EntryCommentId)).ToList();

                var response = new GetMyStatisticsViewModel
                {
                    MyEntriesCount = myEntryIds.Count(),
                    CommentsOfMyEntriesCount = entryCommentQuery.Where(x => myEntryIds.Contains(x.EntryId)).Count(),
                    FavoritesOfMyEntriesCount = entryFavoriteQuery.Where(x => myEntryIds.Contains(x.EntryId)).Count(),
                    DownvotesOfMyEntriesCount = myEntriesVotes.Where(x => x.VoteType == VoteType.DownVote).Count(),
                    UpvotesOfMyEntriesCount = myEntriesVotes.Where(x => x.VoteType == VoteType.UpVote).Count(),
                    MyFavoriteEntriesCount = entryFavoriteQuery.Where(x => x.UserId == request.UserId).Count(),
                    MyCommentsCount = myEntryCommentIds.Count(),
                    DownvotesOfMyCommentsCount = myEntryCommentsVotes.Where(x => x.VoteType == VoteType.DownVote).Count(),
                    FavoritesOfMyCommentsCount = entryCommentFavoriteQuery.Where(x => myEntryCommentIds.Contains(x.EntryCommentId)).Count(),
                    UpvotesOfMyCommentsCount = myEntryCommentsVotes.Where(x => x.VoteType == VoteType.UpVote).Count(),
                    MyFavoriteCommentsCount = entryCommentFavoriteQuery.Where(x => x.UserId == request.UserId).Count()
                };

                return await Task.FromResult(response);
            }
        }
    }
}
