using Application.Queries.GetEntries;
using Application.Queries.GetEntries.GetMainPageEntries;
using Application.Queries.GetEntryComments;
using Application.Queries.GetEntryDetail;
using Application.Queries.GetMyEntries;
using Application.Queries.GetUserEntries;
using Common.Models.Queries;
using Common.Models.RequestModels.EntryCommentRequestModels.CreateRequestModels;
using Common.Models.RequestModels.EntryCommentRequestModels.DeleteRequestModel;
using Common.Models.RequestModels.EntryCommentRequestModels.UpdateRequestModels;
using Common.Models.RequestModels.EntryRequestModels.CreateRequestModels;
using Common.Models.RequestModels.EntryRequestModels.DeleteRequestModels;
using Common.Models.RequestModels.EntryRequestModels.UpdateRequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryController : ExtendBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetEntries(Guid tagId, int pageNumber, int pageSize)
        {
            var result = await Mediator.Send(new GetEntriesQuery(tagId, pageNumber, pageSize));

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetEntryDetailQuery(id, UserId));

            return Ok(result);
        }

        [HttpGet]
        [Route("UserEntries")]
        public async Task<IActionResult> GetUserEntries(Guid userId, string userName, int pageNumber, int pageSize)
        {
            if(userId == Guid.Empty && string.IsNullOrEmpty(userName))
                userId = UserId;

            var result = await Mediator.Send(new GetUserEntriesQuery(userId, userName, pageNumber, pageSize));

            return Ok(result);
        }

        [HttpGet]
        [Route("MyEntries")]
        public async Task<IActionResult> GetMyEntries(int pageNumber, int pageSize)
        {
            var result = await Mediator.Send(new GetMyEntriesQuery(UserId, pageNumber, pageSize));

            return Ok(result);
        }

        [HttpGet]
        [Route("MyEntryDetail")]
        public async Task<IActionResult> GetMyEntryDetail(Guid entryId)
        {
            var result = await Mediator.Send(new GetMyEntryDetailQuery(entryId));

            return Ok(result);
        }

        [HttpGet]
        [Route("MyFavoriteEntries")]
        public async Task<IActionResult> GetMyFavoriteEntries(int pageNumber, int pageSize)
        {
            var result = await Mediator.Send(new GetMyFavoriteEntriesQuery(UserId, pageNumber, pageSize));

            return Ok(result);
        }

        [HttpGet]
        [Route("MainPageEntries")]
        public async Task<IActionResult> GetMainPageEntries(int pageNumber, int pageSize)
        {
            var result = await Mediator.Send(new GetMainPageEntriesQuery(UserId, pageNumber, pageSize));

            return Ok(result);
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchEntryQuery searchEntryQuery)
        {
            var result = await Mediator.Send(searchEntryQuery);

            return Ok(result);
        }

        [HttpGet]
        [Route("EntryComments/{entryId}")]
        public async Task<IActionResult> GetEntryComments(Guid entryId, int pageNumber, int pageSize)
        {
            var result = await Mediator.Send(new GetEntryCommentsQuery(entryId, UserId, pageNumber, pageSize));
            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateEntryCommand createEntryCommand)
        {
            if(createEntryCommand.UserId == Guid.Empty)
                createEntryCommand.UserId = UserId;

            var result = await Mediator.Send(createEntryCommand);

            return Ok(result);
        }

        [HttpPost]
        [Route("CreateEntryComment")]
        [Authorize]
        public async Task<IActionResult> CreateEntryComment([FromBody] CreateEntryCommentCommand createEntryCommentCommand)
        {
            if(createEntryCommentCommand.UserId == Guid.Empty)
                createEntryCommentCommand.UserId = UserId;
            var result = await Mediator.Send(createEntryCommentCommand);

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateEntryComment")]
        [Authorize]
        public async Task<IActionResult> UpdateEntryComment([FromBody] UpdateEntryCommentCommand updateEntryCommentCommand)
        {
            if(updateEntryCommentCommand.UserId == Guid.Empty)
                updateEntryCommentCommand.UserId = UserId;
            var result = await Mediator.Send(updateEntryCommentCommand);

            return Ok(result);
        }

        [HttpPut]
        [Route("DeleteEntryComment")]
        [Authorize]
        public async Task<IActionResult> DeleteEntryComment([FromBody] DeleteEntryCommentCommand deleteEntryCommentCommand)
        {
            if(deleteEntryCommentCommand.UserId == Guid.Empty)
                deleteEntryCommentCommand.UserId = UserId;
            var result = await Mediator.Send(deleteEntryCommentCommand);

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateEntry")]
        [Authorize]
        public async Task<IActionResult> UpdateEntry([FromBody] UpdateEntryCommand updateEntryCommand)
        {
            if(updateEntryCommand.UserId == Guid.Empty)
                updateEntryCommand.UserId = UserId;
            var result = await Mediator.Send(updateEntryCommand);

            return Ok(result);
        }

        [HttpPut]
        [Route("DeleteEntry")]
        [Authorize]
        public async Task<IActionResult> DeleteEntry([FromBody] DeleteEntryCommand deleteEntryCommand)
        {
            if(deleteEntryCommand.UserId == Guid.Empty)
                deleteEntryCommand.UserId = UserId;
            var result = await Mediator.Send(deleteEntryCommand);

            return Ok(result);
        }

        [HttpPost]
        [Route("CreateEntryTag")]
        [Authorize]
        public async Task<IActionResult> CreateEntryTag([FromBody] CreateEntryTagCommand createEntryTagCommand)
        {
            var result = await Mediator.Send(createEntryTagCommand);

            return Ok(result);
        }
    }
}
