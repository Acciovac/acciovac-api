using System;
using acciovac.Domain.Common;

namespace acciovac.Domain.Entities;

public class LocalExpirences : AuditableEntity<Guid>
    {
        public string expireancename { get; private set; } = null!;
        public string Description { get; private set; } = null!;

        private LocalExpirences() { }

        public LocalExpirences(
            string locationName,
            string description,
            string createdBy = "system")
        {
            Id = Guid.NewGuid();
            expireancename = locationName;
            Description = description;
            SetCreated(createdBy);
        }

        public void Update(
            string locationName,
            string description,
            string modifiedBy = "system")
        {
            expireancename = locationName;
            Description = description;
            SetModified(modifiedBy);
        }
}
