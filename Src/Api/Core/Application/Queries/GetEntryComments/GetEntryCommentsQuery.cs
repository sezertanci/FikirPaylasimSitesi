using Application.Interfaces.Repositories;
using Common.Infrastructure.Extensions;
using Common.Models;
using Common.Models.Page;
using Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.GetEntryComments
{
    public class GetEntryCommentsQuery : BasePagedQuery, IRequest<PagedViewModel<GetEntryCommentsViewModel>>
    {
        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }

        public GetEntryCommentsQuery(Guid entryId, Guid userId, int pageNumber, int pageSize) : base(pageNumber, pageSize)
        {
            EntryId = entryId;
            UserId = userId;
        }

        public class GetEntryCommentsQueryHandler : IRequestHandler<GetEntryCommentsQuery, PagedViewModel<GetEntryCommentsViewModel>>
        {
            private readonly IEntryCommentRepository entryCommentRepository;

            public GetEntryCommentsQueryHandler(IEntryCommentRepository entryCommentRepository)
            {
                this.entryCommentRepository = entryCommentRepository;
            }

            public async Task<PagedViewModel<GetEntryCommentsViewModel>> Handle(GetEntryCommentsQuery request, CancellationToken cancellationToken)
            {
                var query = entryCommentRepository.Query();

                query = query.Include(x => x.EntryCommentFavorites)
                             .Include(x => x.EntryCommentVotes)
                             .Include(x => x.User)
                             .Where(x => x.EntryId == request.EntryId)
                             .OrderByDescending(x => x.CreatedDate);

                var list = query.Select(x => new GetEntryCommentsViewModel
                {
                    Id = x.Id,
                    Content = x.Content,
                    CreatedDate = x.CreatedDate,
                    Favorited = request.UserId != Guid.Empty && x.EntryCommentFavorites.Any(y => y.UserId == request.UserId),
                    FavoritedCount = x.EntryCommentFavorites.Count,
                    MyComment = x.User.Id == request.UserId,
                    UserId = x.User.Id,
                    UserName = x.User.UserName,
                    VoteType = request.UserId != Guid.Empty && x.EntryCommentVotes.Any(y => y.UserId == request.UserId) ? x.EntryCommentVotes.FirstOrDefault(y => y.UserId == request.UserId).VoteType : VoteType.None
                });

                var entryComments = await list.GetPaged(request.PageNumber, request.PageSize);

                return entryComments;
            }
        }
    }
}
