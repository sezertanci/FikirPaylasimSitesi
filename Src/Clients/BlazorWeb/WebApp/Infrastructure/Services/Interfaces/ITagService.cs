using Common.Models.Queries;
using Common.Models.RequestModels.TagRequestModels.UpdateRequestModels;

namespace WebApp.Infrastructure.Services.Interfaces
{
    public interface ITagService
    {
        Task<List<GetTagsViewModel>> GetTags();
        Task<bool> UpdateTag(UpdateTagCommand updateTagCommand);
    }
}
