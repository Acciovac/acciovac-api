using acciovac.Domain.Common;

namespace acciovac.Domain.Entities
{
    public class Message : AuditableEntity<Guid>
    {
        public Guid UserId { get; private set; }
        public string Subject { get; private set; } = null!;
        public string Body { get; private set; } = null!;
        public bool IsRead { get; private set; }
        public DateTime? ReadAt { get; private set; }
        public bool IsResolved { get; private set; }
        public DateTime? ResolvedAt { get; private set; }

        private Message() { }

        public Message(Guid userId, string subject, string body, string createdBy = "system")
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Subject = subject;
            Body = body;
            IsRead = false;
            IsResolved = false;
            SetCreated(createdBy);
        }

        public void MarkAsRead(string modifiedBy = "system")
        {
            if (IsRead)
            {
                return;
            }

            IsRead = true;
            ReadAt = DateTime.UtcNow;
            SetModified(modifiedBy);
        }

        public void MarkAsResolved(string modifiedBy = "system")
        {
            if (!IsRead)
            {
                MarkAsRead(modifiedBy);
            }

            if (IsResolved)
            {
                return;
            }

            IsResolved = true;
            ResolvedAt = DateTime.UtcNow;
            SetModified(modifiedBy);
        }
    }
}
