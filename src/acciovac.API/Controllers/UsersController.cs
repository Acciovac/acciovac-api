using acciovac.API.Common;
using acciovac.Application.Abstractions;
using acciovac.Application.Behaviors.Users.Commands.CreateUser;
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

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

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
        }
    }
}