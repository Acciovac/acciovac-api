using acciovac.Domain.Common;
using System;

namespace acciovac.Domain.Entities
{
    public class Message : AuditableEntity<Guid>
    {
        public Guid UserId { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }
        public bool IsRead { get; private set; }
        public bool IsResolved { get; private set; }
        public DateTime? ResolvedAt { get; private set; }

        private Message() { }

        public Message(Guid userId, string subject, string body, string createdBy)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Subject = subject;
            Body = body;
            IsRead = false;
            IsResolved = false;
            ResolvedAt = null;

            SetCreated(createdBy);
        }

        public void MarkAsRead()
        {
            IsRead = true;
        }

        public void MarkAsResolved(string resolvedBy)
        {
            IsResolved = true;
            ResolvedAt = DateTime.UtcNow;
            SetModified(resolvedBy);
        }
    }
}
