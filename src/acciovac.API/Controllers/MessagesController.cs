using acciovac.API.Common;
using acciovac.Application.Abstractions;
using acciovac.Domain.DTOs;
using acciovac.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace acciovac.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;

        public MessagesController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllMessages()
        {
            var messages = await _messageRepository.GetAllAsync();
            return Ok(ApiResponse.Success(messages));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMessage([FromBody] CreateMessageDto createMessageDto)
        {
            if (createMessageDto == null || string.IsNullOrEmpty(createMessageDto.Subject) || string.IsNullOrEmpty(createMessageDto.Body))
            {
                return BadRequest(ApiResponse.Failure("Subject and Body are required"));
            }

            var message = new Message(createMessageDto.UserId, createMessageDto.Subject, createMessageDto.Body, createMessageDto.CreatedBy);
            await _messageRepository.AddAsync(message);

            return CreatedAtAction(
                nameof(GetMessageById),
                new { id = message.Id },
                ApiResponse.Success(new { id = message.Id }));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMessageById(Guid id)
        {
            var message = await _messageRepository.GetByIdAsync(id);

            if (message == null)
            {
                return NotFound(ApiResponse.Failure("Message not found"));
            }

            return Ok(ApiResponse.Success(message));
        }

        [HttpGet("user/{userId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserMessages(Guid userId)
        {
            var messages = await _messageRepository.GetByUserIdAsync(userId);
            return Ok(ApiResponse.Success(messages));
        }

        [HttpPut("{id:guid}/read")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MarkAsRead(Guid id)
        {
            var message = await _messageRepository.GetByIdAsync(id);

            if (message == null)
            {
                return NotFound(ApiResponse.Failure("Message not found"));
            }

            message.MarkAsRead();
            await _messageRepository.UpdateAsync(message);

            return Ok(ApiResponse.Success(new { id = message.Id }));
        }

        [HttpPut("{id:guid}/resolve")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MarkAsResolved(Guid id, [FromQuery] string resolvedBy = "system")
        {
            var message = await _messageRepository.GetByIdAsync(id);

            if (message == null)
            {
                return NotFound(ApiResponse.Failure("Message not found"));
            }

            message.MarkAsResolved(resolvedBy);
            await _messageRepository.UpdateAsync(message);

            return Ok(ApiResponse.Success(new { id = message.Id }));
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMessage(Guid id)
        {
            var message = await _messageRepository.GetByIdAsync(id);

            if (message == null)
            {
                return NotFound(ApiResponse.Failure("Message not found"));
            }

            await _messageRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
