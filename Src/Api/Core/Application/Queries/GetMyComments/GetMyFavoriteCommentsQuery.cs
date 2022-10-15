using Application.Interfaces.Repositories;
using Common.Infrastructure.Extensions;
using Common.Models;
using Common.Models.Page;
using Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.GetEntryComments
{
    public class GetMyFavoriteCommentsQuery : BasePagedQuery, IRequest<PagedViewModel<GetMyCommentsViewModel>>
    {
        public Guid UserId { get; set; }

        public GetMyFavoriteCommentsQuery(Guid userId, int pageNumber, int pageSize) : base(pageNumber, pageSize)
        {
            UserId = userId;
        }

        public class GetMyEntryCommentsQueryHandler : IRequestHandler<GetMyFavoriteCommentsQuery, PagedViewModel<GetMyCommentsViewModel>>
        {
            private readonly IEntryCommentFavoriteRepository entryCommentFavoriteRepository;
            private readonly IEntryRepository entryRepository;

            public GetMyEntryCommentsQueryHandler(IEntryCommentFavoriteRepository entryCommentFavoriteRepository, IEntryRepository entryRepository)
            {
                this.entryCommentFavoriteRepository = entryCommentFavoriteRepository;
                this.entryRepository = entryRepository;
            }

            public async Task<PagedViewModel<GetMyCommentsViewModel>> Handle(GetMyFavoriteCommentsQuery request, CancellationToken cancellationToken)
            {
                var entryCommentIds = entryCommentFavoriteRepository.Get(x => x.UserId == request.UserId).Select(x => x.EntryCommentId).ToList();

                var query = entryRepository.Query();

                query = query.Where(entry =>
               entry.EntryComments.Where(comment => entryCommentIds.Contains(comment.Id) && !entry.Deleted)
                                  .Select(comment => comment.EntryId)
                                  .Contains(entry.Id))
                            .Include(entry => entry.EntryVotes)
                            .Include(entry => entry.EntryFavorites)
                            .Include(entry => entry.EntryComments)
                               .ThenInclude(comment => comment.EntryCommentFavorites)
                            .Include(entry => entry.EntryComments)
                               .ThenInclude(comment => comment.User)
                            .Include(entry => entry.EntryComments)
                               .ThenInclude(comment => comment.EntryCommentVotes)
                            .Include(entry => entry.User)
                            .OrderByDescending(entry => entry.CreatedDate);

                var list = query.Select(entry => new GetMyCommentsViewModel()
                {
                    EntryDetail = new GetEntryDetailViewModel
                    {
                        Content = entry.Content,
                        Id = entry.Id,
                        Subject = entry.Subject,
                        CreatedDate = entry.CreatedDate,
                        Favorited = request.UserId != Guid.Empty && entry.EntryFavorites.Any(entryFavorite => entryFavorite.UserId == request.UserId),
                        FavoritedCount = entry.EntryFavorites.Count,
                        UserId = entry.UserId,
                        UserName = entry.User.UserName,
                        VoteType = request.UserId != Guid.Empty && entry.EntryVotes.Any(entryVote => entryVote.UserId == request.UserId) ? entry.EntryVotes.FirstOrDefault(entryVote => entryVote.UserId == request.UserId).VoteType : VoteType.None
                    },
                    MyEntry = entry.UserId == request.UserId,
                    Comments = entry.EntryComments.Where(comment => entryCommentIds.Contains(comment.Id) && !comment.Deleted).OrderByDescending(comment => comment.CreatedDate).Select(comment => new GetEntryCommentsViewModel
                    {
                        Id = comment.Id,
                        Content = comment.Content,
                        CreatedDate = comment.CreatedDate,
                        Favorited = request.UserId != Guid.Empty && comment.EntryCommentFavorites.Any(commentFavorite => commentFavorite.UserId == request.UserId),
                        FavoritedCount = comment.EntryCommentFavorites.Count,
                        MyComment = comment.UserId == request.UserId,
                        UserId = comment.User.Id,
                        UserName = comment.User.UserName,
                        VoteType = request.UserId != Guid.Empty && comment.EntryCommentVotes.Any(commentVote => commentVote.UserId == request.UserId) ? comment.EntryCommentVotes.FirstOrDefault(commentVote => commentVote.UserId == request.UserId).VoteType : VoteType.None
                    }).ToList()
                });

                var entryComments = await list.GetPaged(request.PageNumber, request.PageSize);

                return entryComments;
            }
        }
    }
}
