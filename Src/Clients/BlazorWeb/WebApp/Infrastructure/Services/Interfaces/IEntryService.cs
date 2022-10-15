using Common.Models.Page;
using Common.Models.Queries;
using Common.Models.RequestModels.EntryCommentRequestModels.CreateRequestModels;
using Common.Models.RequestModels.EntryCommentRequestModels.DeleteRequestModel;
using Common.Models.RequestModels.EntryCommentRequestModels.UpdateRequestModels;
using Common.Models.RequestModels.EntryRequestModels.CreateRequestModels;
using Common.Models.RequestModels.EntryRequestModels.DeleteRequestModels;
using Common.Models.RequestModels.EntryRequestModels.UpdateRequestModels;

namespace WebApp.Infrastructure.Services.Interfaces
{
    public interface IEntryService
    {
        Task<Guid> CreateEntry(CreateEntryCommand createEntryCommand);
        Task<GetEntryDetailViewModel> GetEntryDetail(Guid entryId);
        Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int pageNumber, int pageSize);
        Task<PagedViewModel<GetEntryDetailViewModel>> GetUserPageEntries(int pageNumber, int pageSize, string userName = null);
        Task<List<SearchEntryViewModel>> SearcBySubject(string searchText);
        Task<bool> UpdateEntry(UpdateEntryCommand updateEntryCommand);
        Task<bool> DeleteEntry(DeleteEntryCommand deleteEntryCommand);
        Task<Guid> CreateEntryComment(CreateEntryCommentCommand createEntryCommentCommand);
        Task<bool> UpdateEntryComment(UpdateEntryCommentCommand updateEntryCommentCommand);
        Task<bool> DeleteEntryComment(DeleteEntryCommentCommand deleteEntryCommentCommand);
        Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int pageNumber, int pageSize);
        Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntriesWithTag(Guid tagId, int pageNumber, int pageSize);
        Task<PagedViewModel<GetEntriesViewModel>> GetEntries(Guid tagId, int pageNumber, int pageSize);
        Task<PagedViewModel<GetEntriesViewModel>> GetEntries(int pageNumber, int pageSize);
        Task<PagedViewModel<GetMyEntryDetailViewModel>> GetMyEntries(int pageNumber, int pageSize);
        Task<PagedViewModel<GetEntryDetailViewModel>> GetMyFavoriteEntries(int pageNumber, int pageSize);
        Task<bool> CreateEntryTag(CreateEntryTagCommand createEntryTagCommand);
        Task<GetMyEntryDetailViewModel> GetMyEntryDetail(Guid entryId);
    }
}