using Application.Queries.GetMyStatistics;
using Application.Queries.GetUserDetail;
using Common.Events.UserEvent;
using Common.Models.RequestModels.UserRequestModels.ConfirmEmailRequestModel;
using Common.Models.RequestModels.UserRequestModels.CreateRequestModels;
using Common.Models.RequestModels.UserRequestModels.LoginRequestModels;
using Common.Models.RequestModels.UserRequestModels.UpdateRequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ExtendBaseController
    {
        [HttpGet("CheckToken")]
        [Authorize]
        public async Task<IActionResult> CheckToken()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetUserDetailQuery(id));

            return Ok(result);
        }

        [HttpGet]
        [Route("UserName/{userName}")]
        public async Task<IActionResult> GetByUserName(string userName)
        {
            var result = await Mediator.Send(new GetUserDetailQuery(Guid.Empty, userName));

            return Ok(result);
        }

        [HttpGet]
        [Route("Statistics")]
        [Authorize]
        public async Task<IActionResult> GetMyStatistics()
        {
            var result = await Mediator.Send(new GetMyStatisticsQuery(UserId));

            return Ok(result);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
        {
            var result = await Mediator.Send(loginUserCommand);

            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        //[Authorize]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand createUserCommand)
        {
            var result = await Mediator.Send(createUserCommand);

            return Ok(result);
        }

        [HttpPost]
        [Route("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand updateUserCommand)
        {
            var result = await Mediator.Send(updateUserCommand);

            return Ok(result);
        }

        [HttpGet]
        [Route("Confirm/{id}")]
        public async Task<IActionResult> ConfirmEmail(Guid id)
        {
            var result = await Mediator.Send(new ConfirmEmailCommand() { Id = id });

            return Ok(result);
        }

        [HttpPost]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailCommandWithEmail confirmEmailCommandWithEmail)
        {
            var result = await Mediator.Send(confirmEmailCommandWithEmail);

            return Ok(result);
        }

        [HttpPost]
        [Route("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand changeUserPasswordCommand)
        {
            var result = await Mediator.Send(changeUserPasswordCommand);

            return Ok(result);
        }
    }
}
