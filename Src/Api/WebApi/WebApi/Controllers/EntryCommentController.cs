using Application.Queries.GetEntryComments;
using Common.Models.RequestModels.EntryCommentRequestModels.CreateRequestModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryCommentController : ExtendBaseController
    {
        [HttpGet]
        [Route("EntryComments/{entryId}")]
        public async Task<IActionResult> GetEntryComments(Guid entryId, int pageNumber, int pageSize)
        {
            var result = await Mediator.Send(new GetEntryCommentsQuery(entryId, UserId, pageNumber, pageSize));

            return Ok(result);
        }

        [HttpGet]
        [Route("MyComments")]
        public async Task<IActionResult> GetMyComments(int pageNumber, int pageSize)
        {
            var result = await Mediator.Send(new GetMyCommentsQuery(UserId, pageNumber, pageSize));

            return Ok(result);
        }

        [HttpGet]
        [Route("MyFavoriteComments")]
        public async Task<IActionResult> GetMyFavoriteComments(int pageNumber, int pageSize)
        {
            var result = await Mediator.Send(new GetMyFavoriteCommentsQuery(UserId, pageNumber, pageSize));

            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CreateEntryCommentCommand createEntryCommentCommand)
        {
            if(createEntryCommentCommand.UserId != Guid.Empty)
                createEntryCommentCommand.UserId = UserId;
            var result = await Mediator.Send(createEntryCommentCommand);

            return Ok(result);
        }
    }
}
