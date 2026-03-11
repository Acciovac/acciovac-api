using System;
using acciovac.Domain.Common;

namespace acciovac.Domain.Entities;

public class LocalExpirences : AuditableEntity<Guid>
    {
        public string LocationName { get; private set; } = null!;
        public string Description { get; private set; } = null!;

        private LocalExpirences() { }

        public LocalExpirences(
            string locationName,
            string description,
            string createdBy = "system")
        {
            Id = Guid.NewGuid();
            LocationName = locationName;
            Description = description;
            SetCreated(createdBy);
        }

        public void Update(
            string locationName,
            string description,
            string modifiedBy = "system")
        {
            LocationName = locationName;
            Description = description;
            SetModified(modifiedBy);
        }
}
