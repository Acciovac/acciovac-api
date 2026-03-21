using acciovac.API.Common;
using acciovac.Application.Behaviors.Users.Commands.CreateUser;
using acciovac.Application.Behaviors.Users.Queries.GetUserById;
using acciovac.Application.Behaviors.Users.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace acciovac.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.Failure(result.Error));
            }

            return CreatedAtAction(
                nameof(GetUser),
                new { id = result.Value },
                ApiResponse.Success(new { id = result.Value }));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _mediator.Send(new GetUsersQuery());

            var response = result.Value?.Select(user => new
            {
                id = user.Id,
                firebaseUid = user.FirebaseUid,
                email = user.Email,
                isActive = user.IsActive,
                createdAt = user.CreatedAt
            }) ?? Enumerable.Empty<object>();

            return Ok(ApiResponse.Success(response));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));

            if (!result.IsSuccess)
            {
                return NotFound(ApiResponse.Failure(result.Error));
            }

            var user = result.Value;

            return Ok(ApiResponse.Success(new
            {
                id = user!.Id,
                firebaseUid = user.FirebaseUid,
                email = user.Email,
                isActive = user.IsActive,
                createdAt = user.CreatedAt
            }));
        }
    }
}