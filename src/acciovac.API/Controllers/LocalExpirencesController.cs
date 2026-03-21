using acciovac.API.Common;
using acciovac.Application.Abstractions;
using acciovac.Application.Behaviors.LocalExpirences.Commands.AddLocalExpirences;
using acciovac.Application.Behaviors.LocalExpirences.Commands.DeleteLocalExpirences;
using acciovac.Application.Behaviors.LocalExpirences.Commands.UpdateLocalExpirences;
using acciovac.Application.Behaviors.LocalExpirences.Queries.GetAllLocalExpirences;
using acciovac.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace acciovac.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocalExpirencesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAzureBlobStorageService _blobStorageService;
        private const string PhotoContainerName = "local-experiences-photos";
        private const long MaxFileSizeBytes = 5 * 1024 * 1024; // 5 MB

        public LocalExpirencesController(IMediator mediator, IAzureBlobStorageService blobStorageService)
        {
            _mediator = mediator;
            _blobStorageService = blobStorageService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllLocalExpirencesQuery());

            var experiences = result.Value?.Select(x => new
            {
                id = x.Id,
                expireancename = x.expireancename,
                description = x.Description,
                photos = x.Photos.Select(p => new { id = p.Id, photoUrl = p.PhotoUrl, displayOrder = p.DisplayOrder }),
                createdAt = x.CreatedAt
            }) ?? Enumerable.Empty<object>();

            return Ok(ApiResponse.Success(experiences));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] CreateLocalExpirenceDto request)
        {
            var command = new AddLocalExpirencesCommand(
                request.LocationName,
                request.Description,
                request.PhotoUrls ?? new List<string>()
            );
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.Failure(result.Error));
            }

            return CreatedAtAction(nameof(GetAll), new { id = result.Value }, 
                ApiResponse.Success(new { id = result.Value, message = "Local experience added successfully" }));
        }

        [HttpPost("{id:guid}/photos")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UploadPhoto(Guid id, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(ApiResponse.Failure("No file provided"));
            }

            if (file.Length > MaxFileSizeBytes)
            {
                return BadRequest(ApiResponse.Failure($"File size exceeds maximum limit of 5 MB"));
            }

            try
            {
                // Generate unique file name
                var fileExtension = Path.GetExtension(file.FileName);
                var fileName = $"{id}-{Guid.NewGuid()}{fileExtension}";

                // Upload to Azure Blob Storage
                using (var stream = file.OpenReadStream())
                {
                    var photoUrl = await _blobStorageService.UploadFileAsync(
                        PhotoContainerName,
                        fileName,
                        stream
                    );

                    return CreatedAtAction(nameof(GetAll), 
                        ApiResponse.Success(new { photoUrl, message = "Photo uploaded successfully" }));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Failure($"Failed to upload photo: {ex.Message}"));
            }
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLocalExpirenceDto request)
        {
            var command = new UpdateLocalExpirencesCommand(
                id,
                request.LocationName,
                request.Description,
                request.PhotoUrls ?? new List<string>()
            );
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return NotFound(ApiResponse.Failure(result.Error));
            }

            return Ok(ApiResponse.Success(new { id = result.Value, message = "Local experience updated successfully" }));
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteLocalExpirencesCommand(id));

            if (!result.IsSuccess)
            {
                return NotFound(ApiResponse.Failure(result.Error));
            }

            return Ok(ApiResponse.Success(new { message = "Local experience deleted successfully" }));
        }
    }

    public record AddLocalExpirencesRequest(string LocationName, string Description);
    public record UpdateLocalExpirencesRequest(string LocationName, string Description);
}
