using Common.Models.Page;
using Common.Models.Queries;
using Common.Models.RequestModels.EntryCommentRequestModels.CreateRequestModels;
using System.Net.Http.Json;
using WebApp.Infrastructure.Services.Interfaces;

namespace WebApp.Infrastructure.Services
{
    public class EntryCommentService : IEntryCommentService
    {
        private readonly HttpClient client;

        public EntryCommentService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int pageNumber, int pageSize)
        {
            var response = await client.GetFromJsonAsync<PagedViewModel<GetEntryCommentsViewModel>>($"EntryComment/EntryComments/{entryId}?pageNumber={pageNumber}&pageSize={pageSize}");

            return response;
        }

        public async Task<Guid> CreateEntryComment(CreateEntryCommentCommand createEntryCommentCommand)
        {
            var response = await client.PostAsJsonAsync("EntryComment/Create", createEntryCommentCommand);

            if(!response.IsSuccessStatusCode)
                return Guid.Empty;

            var guidStr = await response.Content.ReadAsStringAsync();

            return new Guid(guidStr.Trim('"'));
        }

        public async Task<PagedViewModel<GetMyCommentsViewModel>> GetMyComments(int pageNumber, int pageSize)
        {
            var response = await client.GetFromJsonAsync<PagedViewModel<GetMyCommentsViewModel>>($"EntryComment/MyComments?pageNumber={pageNumber}&pageSize={pageSize}");

            return response;
        }

        public async Task<PagedViewModel<GetMyCommentsViewModel>> GetMyFavoriteComments(int pageNumber, int pageSize)
        {
            var response = await client.GetFromJsonAsync<PagedViewModel<GetMyCommentsViewModel>>($"EntryComment/MyFavoriteComments?pageNumber={pageNumber}&pageSize={pageSize}");

            return response;
        }
    }
}
