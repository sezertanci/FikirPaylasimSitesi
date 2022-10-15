using Application.Queries.GetTags;
using Common.Models.RequestModels.TagRequestModels.UpdateRequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ExtendBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            var result = await Mediator.Send(new GetTagsQuery());

            return Ok(result);
        }

        [HttpPut]
        [Route("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateTagCommand updateTagCommand)
        {
            var result = await Mediator.Send(updateTagCommand);

            return Ok(result);
        }
    }
}
