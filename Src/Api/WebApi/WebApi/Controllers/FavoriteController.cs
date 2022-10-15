using Common.Models.RequestModels.EntryCommentRequestModels.CreateRequestModels;
using Common.Models.RequestModels.EntryCommentRequestModels.DeleteRequestModel;
using Common.Models.RequestModels.EntryRequestModels.CreateRequestModels;
using Common.Models.RequestModels.EntryRequestModels.DeleteRequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FavoriteController : ExtendBaseController
    {
        [HttpPost]
        [Route("Entry/{entryId}")]
        public async Task<IActionResult> CreateEntryFavorite(Guid entryId)
        {
            var result = await Mediator.Send(new CreateEntryFavoriteCommand(entryId, UserId));

            return Ok(result);
        }

        [HttpPost]
        [Route("EntryComment/{entryCommentId}")]
        public async Task<IActionResult> CreateEntryCommentFavorite(Guid entryCommentId)
        {
            var result = await Mediator.Send(new CreateEntryCommentFavoriteCommand(entryCommentId, UserId));

            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteEntryFavorite/{entryId}")]
        public async Task<IActionResult> DeleteEntryFavorite(Guid entryId)
        {
            var result = await Mediator.Send(new DeleteEntryFavoriteCommand(entryId, UserId));

            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteEntryCommentFavorite/{entryCommentId}")]
        public async Task<IActionResult> DeleteEntryCommentFavorite(Guid entryCommentId)
        {
            var result = await Mediator.Send(new DeleteEntryCommentFavoriteCommand(entryCommentId, UserId));

            return Ok(result);
        }
    }
}
