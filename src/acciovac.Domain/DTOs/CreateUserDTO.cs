using System;
using System.Collections.Generic;
using System.Text;

namespace acciovac.Domain.DTOs
{
    public class CreateUserDTO
    {
        public required string FirebaseUid { get; set; }
        public string Email { get; set; }
        public string? DisplayName { get; set; }
        public string? PhotoUrl { get; set; }
        public string? PhoneNumber { get; set; }
        public string? CreatedBy { get; set; }
    }
}
