using Common.Models;
using Common.Models.RequestModels.EntryCommentRequestModels.CreateRequestModels;
using Common.Models.RequestModels.EntryCommentRequestModels.DeleteRequestModel;
using Common.Models.RequestModels.EntryRequestModels.CreateRequestModels;
using Common.Models.RequestModels.EntryRequestModels.DeleteRequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VoteController : ExtendBaseController
    {
        [HttpPost]
        [Route("EntryVote/{entryId}")]
        public async Task<IActionResult> CreateEntryVote(Guid entryId, VoteType voteType = VoteType.UpVote)
        {
            var result = await Mediator.Send(new CreateEntryVoteCommand(entryId, UserId, voteType));

            return Ok(result);
        }

        [HttpPost]
        [Route("EntryCommentVote/{entryCommentId}")]
        public async Task<IActionResult> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType = VoteType.UpVote)
        {
            var result = await Mediator.Send(new CreateEntryCommentVoteCommand(entryCommentId, UserId, voteType));

            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteEntryVote/{entryId}")]
        public async Task<IActionResult> DeleteEntryVote(Guid entryId)
        {
            var result = await Mediator.Send(new DeleteEntryVoteCommand(entryId, UserId));

            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteEntryCommentVote/{entryCommentId}")]
        public async Task<IActionResult> DeleteEntryCommentVote(Guid entryCommentId)
        {
            var result = await Mediator.Send(new DeleteEntryCommentVoteCommand(entryCommentId, UserId));

            return Ok(result);
        }
    }
}
