using acciovac.API.Common;
using acciovac.Application.Abstractions;
using acciovac.Application.Behaviors.Users.Commands.CreateUser;
using acciovac.Application.Behaviors.Users.Commands.DeleteUser;
using acciovac.Application.Behaviors.Users.Commands.UpdateUser;
using acciovac.Application.Behaviors.Users.Queries.GetAllUsers;
using acciovac.Application.Behaviors.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace acciovac.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;

        public UsersController(IMediator mediator, IUserRepository userRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error });

            return CreatedAtAction(
                nameof(GetUser),
                new { id = result.Value },
                ApiResponse.Success(new { id = result.Value }));
            return CreatedAtAction(nameof(GetUser), new { id = result.Value }, new { id = result.Value });
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return Ok(result.Value);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));

            if (user is null)
            {
                return NotFound(ApiResponse.Failure("User not found"));
            }

            return Ok(ApiResponse.Success(new
            {
                id = user.Id,
                firebaseUid = user.FirebaseUid,
                email = user.Email,
                isActive = user.IsActive,
                createdAt = user.CreatedAt
            }));
            var result = await _mediator.Send(new GetUserByIdQuery(id));

            if (!result.IsSuccess)
                return NotFound(new { error = result.Error });

            return Ok(result.Value);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand command)
        {
            if (id != command.Id)
                return BadRequest(new { error = "Route id and body id do not match." });

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return NotFound(new { error = result.Error });

            return Ok(new { id = result.Value });
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(Guid id, [FromQuery] string? modifiedBy)
        {
            var result = await _mediator.Send(new DeleteUserCommand(id, modifiedBy));

            if (!result.IsSuccess)
                return NotFound(new { error = result.Error });

            return Ok(new { id = result.Value });
        }
    }
}