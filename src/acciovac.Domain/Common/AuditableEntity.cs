using System;
using System.Collections.Generic;
using System.Text;

namespace acciovac.Domain.Common
{
    public abstract class AuditableEntity<TId> : Entity<TId>
    {
        public DateTime CreatedAt { get; protected set; }
        public string CreatedBy { get; protected set; }

        public DateTime? LastModifiedAt { get; protected set; }
        public string? LastModifiedBy { get; protected set; }

        protected void SetCreated(string userId)
        {
            CreatedAt = DateTime.UtcNow;
            CreatedBy = userId;
        }

        protected void SetModified(string userId)
        {
            LastModifiedAt = DateTime.UtcNow;
            LastModifiedBy = userId;
        }
    }
}
