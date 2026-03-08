using acciovac.Domain.Common;
using System;

namespace acciovac.Domain.Entities
{
    public class Review : AuditableEntity<Guid>
    {
        public Guid UserId { get; private set; }
        public int Rating { get; private set; }
        public string Comment { get; private set; }
        public bool IsVerified { get; private set; }

        private Review() { }

        public Review(
            Guid userId,
            int rating,
            string comment,
            string createdBy)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Rating = rating;
            Comment = comment;
            IsVerified = false;
            SetCreated(createdBy);
        }

        public void Update(int rating, string comment, string modifiedBy)
        {
            Rating = rating;
            Comment = comment;
            SetModified(modifiedBy);
        }

        public void Verify(string modifiedBy)
        {
            IsVerified = true;
            SetModified(modifiedBy);
        }
    }
}
