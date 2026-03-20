using System;

namespace acciovac.Domain.DTOs
{
    public class CreateMessageDto
    {
        public Guid UserId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string CreatedBy { get; set; }
    }
}
