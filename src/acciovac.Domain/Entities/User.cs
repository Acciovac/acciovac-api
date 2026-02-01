using acciovac.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace acciovac.Domain.Entities
{
    public class User : Entity<Guid>
    {
        public string FirebaseUid { get; private set; }
        public string Email { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private User() { }

        public User(string firebaseUid, string email)
        {
            Id = Guid.NewGuid();
            FirebaseUid = firebaseUid;
            Email = email;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
