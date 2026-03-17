using acciovac.Domain.Common;
using System;

namespace acciovac.Domain.Entities
{
    public class UserRole : AuditableEntity<Guid>
    {
        public Guid UserId { get; private set; }
        public Guid RoleId { get; private set; }
        public User User { get; private set; }
        public Role Role { get; private set; }

        private UserRole() { }

        public UserRole(Guid userId, Guid roleId, string createdBy)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            RoleId = roleId;
            SetCreated(createdBy);
        }
    }
}
