using Common.Models.Page;
using Common.Models.Queries;
using Common.Models.RequestModels.EntryCommentRequestModels.CreateRequestModels;
using Common.Models.RequestModels.EntryCommentRequestModels.DeleteRequestModel;
using Common.Models.RequestModels.EntryCommentRequestModels.UpdateRequestModels;
using Common.Models.RequestModels.EntryRequestModels.CreateRequestModels;
using Common.Models.RequestModels.EntryRequestModels.DeleteRequestModels;
using Common.Models.RequestModels.EntryRequestModels.UpdateRequestModels;
using System.Net.Http.Json;
using WebApp.Infrastructure.Services.Interfaces;

namespace WebApp.Infrastructure.Services
{
    public class EntryService : IEntryService
    {
        private readonly HttpClient client;

        public EntryService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<GetEntryDetailViewModel> GetEntryDetail(Guid entryId)
        {
            var response = await client.GetFromJsonAsync<GetEntryDetailViewModel>($"Entry/{entryId}");

            return response;
        }

        public async Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int pageNumber, int pageSize)
        {
            var response = await client.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"Entry/MainPageEntries?pageNumber={pageNumber}&pageSize={pageSize}");

            return response;
        }

        public async Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntriesWithTag(Guid tagId, int pageNumber, int pageSize)
        {
            var response = await client.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"Entry/MainPageEntries?tagId={tagId}&pageNumber={pageNumber}&pageSize={pageSize}");

            return response;
        }

        public async Task<PagedViewModel<GetEntryDetailViewModel>> GetUserPageEntries(int pageNumber, int pageSize, string userName = null)
        {
            var response = await client.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"Entry/UserEntries?userName={userName}&pageNumber={pageNumber}&pageSize={pageSize}");

            return response;
        }

        public async Task<Guid> CreateEntry(CreateEntryCommand createEntryCommand)
        {
            var response = await client.PostAsJsonAsync("Entry/Create", createEntryCommand);

            if(!response.IsSuccessStatusCode)
                return Guid.Empty;

            var guidStr = await response.Content.ReadAsStringAsync();

            return new Guid(guidStr.Trim('"'));
        }

        public async Task<List<SearchEntryViewModel>> SearcBySubject(string searchText)
        {
            var response = await client.GetFromJsonAsync<List<SearchEntryViewModel>>($"Entry/Search?searchText={searchText}");

            return response;
        }

        public async Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int pageNumber, int pageSize)
        {
            var response = await client.GetFromJsonAsync<PagedViewModel<GetEntryCommentsViewModel>>($"Entry/EntryComments/{entryId}?pageNumber={pageNumber}&pageSize={pageSize}");

            return response;
        }

        public async Task<Guid> CreateEntryComment(CreateEntryCommentCommand createEntryCommentCommand)
        {
            var response = await client.PostAsJsonAsync("Entry/CreateEntryComment", createEntryCommentCommand);

            if(!response.IsSuccessStatusCode)
                return Guid.Empty;

            var guidStr = await response.Content.ReadAsStringAsync();

            return new Guid(guidStr.Trim('"'));
        }

        public async Task<PagedViewModel<GetEntriesViewModel>> GetEntries(Guid tagId, int pageNumber, int pageSize)
        {
            var response = await client.GetFromJsonAsync<PagedViewModel<GetEntriesViewModel>>($"Entry?tagId={tagId}&pageNumber={pageNumber}&pageSize={pageSize}");

            return response;
        }

        public async Task<PagedViewModel<GetEntriesViewModel>> GetEntries(int pageNumber, int pageSize)
        {
            var response = await client.GetFromJsonAsync<PagedViewModel<GetEntriesViewModel>>($"Entry?pageNumber={pageNumber}&pageSize={pageSize}");

            return response;
        }

        public async Task<bool> UpdateEntryComment(UpdateEntryCommentCommand updateEntryCommentCommand)
        {
            var response = await client.PutAsJsonAsync("Entry/UpdateEntryComment", updateEntryCommentCommand);

            if(!response.IsSuccessStatusCode)
                return false;

            var guidStr = await response.Content.ReadAsStringAsync();

            return Convert.ToBoolean(guidStr);
        }

        public async Task<bool> DeleteEntryComment(DeleteEntryCommentCommand deleteEntryCommentCommand)
        {
            var response = await client.PutAsJsonAsync("Entry/DeleteEntryComment", deleteEntryCommentCommand);

            if(!response.IsSuccessStatusCode)
                return false;

            var guidStr = await response.Content.ReadAsStringAsync();

            return Convert.ToBoolean(guidStr);
        }

        public async Task<PagedViewModel<GetMyEntryDetailViewModel>> GetMyEntries(int pageNumber, int pageSize)
        {
            var response = await client.GetFromJsonAsync<PagedViewModel<GetMyEntryDetailViewModel>>($"Entry/MyEntries?pageNumber={pageNumber}&pageSize={pageSize}");

            return response;
        }

        public async Task<GetMyEntryDetailViewModel> GetMyEntryDetail(Guid entryId)
        {
            var response = await client.GetFromJsonAsync<GetMyEntryDetailViewModel>($"Entry/MyEntryDetail?entryId={entryId}");

            return response;
        }

        public async Task<PagedViewModel<GetEntryDetailViewModel>> GetMyFavoriteEntries(int pageNumber, int pageSize)
        {
            var response = await client.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"Entry/MyFavoriteEntries?pageNumber={pageNumber}&pageSize={pageSize}");

            return response;
        }

        public async Task<bool> UpdateEntry(UpdateEntryCommand updateEntryCommand)
        {
            var response = await client.PutAsJsonAsync("Entry/UpdateEntry", updateEntryCommand);

            if(!response.IsSuccessStatusCode)
                return false;

            var guidStr = await response.Content.ReadAsStringAsync();

            return Convert.ToBoolean(guidStr);
        }

        public async Task<bool> DeleteEntry(DeleteEntryCommand deleteEntryCommand)
        {
            var response = await client.PutAsJsonAsync("Entry/DeleteEntry", deleteEntryCommand);

            if(!response.IsSuccessStatusCode)
                return false;

            var guidStr = await response.Content.ReadAsStringAsync();

            return Convert.ToBoolean(guidStr);
        }

        public async Task<bool> CreateEntryTag(CreateEntryTagCommand createEntryTagCommand)
        {
            var response = await client.PostAsJsonAsync("Entry/CreateEntryTag", createEntryTagCommand);

            if(!response.IsSuccessStatusCode)
                return false;

            var guidStr = await response.Content.ReadAsStringAsync();

            return Convert.ToBoolean(guidStr);
        }
    }
}
