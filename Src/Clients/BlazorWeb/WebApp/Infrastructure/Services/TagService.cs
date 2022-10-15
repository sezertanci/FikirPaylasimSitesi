using Common.Models.Queries;
using Common.Models.RequestModels.TagRequestModels.UpdateRequestModels;
using System.Net.Http.Json;
using WebApp.Infrastructure.Services.Interfaces;

namespace WebApp.Infrastructure.Services
{
    public class TagService : ITagService
    {
        private readonly HttpClient client;

        public TagService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<GetTagsViewModel>> GetTags()
        {
            var response = await client.GetFromJsonAsync<List<GetTagsViewModel>>("Tag");

            return response;
        }

        public async Task<bool> UpdateTag(UpdateTagCommand updateTagCommand)
        {
            var response = await client.PutAsJsonAsync("Tag/Update", updateTagCommand);

            if(!response.IsSuccessStatusCode)
                return false;

            var guidStr = await response.Content.ReadAsStringAsync();

            return Convert.ToBoolean(guidStr);
        }
    }
}
