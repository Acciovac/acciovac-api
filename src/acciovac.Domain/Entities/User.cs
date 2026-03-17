using acciovac.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace acciovac.Domain.Entities
{
    public class User : AuditableEntity<Guid>
    {
        public string FirebaseUid { get; private set; }
        public string Email { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public ICollection<UserRole> Roles { get; private set; } = new List<UserRole>();

        private User() { }

        public User(string firebaseUid, string email, string createdBy)
        {
            Id = Guid.NewGuid();
            FirebaseUid = firebaseUid;
            Email = email;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;

            SetCreated(createdBy);
        }

        public void Update(string email, string modifiedBy)
        {
            Email = email;
            SetModified(modifiedBy);
        }

        public void Deactivate(string modifiedBy)
        {
            IsActive = false;
            SetModified(modifiedBy);
        }
    }
}
