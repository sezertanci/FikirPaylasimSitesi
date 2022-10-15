using Common.Models.Page;
using Common.Models.Queries;
using Common.Models.RequestModels.EntryCommentRequestModels.CreateRequestModels;

namespace WebApp.Infrastructure.Services.Interfaces
{
    public interface IEntryCommentService
    {
        Task<Guid> CreateEntryComment(CreateEntryCommentCommand createEntryCommentCommand);
        Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int pageNumber, int pageSize);
        Task<PagedViewModel<GetMyCommentsViewModel>> GetMyComments(int pageNumber, int pageSize);
        Task<PagedViewModel<GetMyCommentsViewModel>> GetMyFavoriteComments(int pageNumber, int pageSize);
    }
}