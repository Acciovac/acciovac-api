using System;

namespace acciovac.Domain.Entities;

public class LocalExpirences : AuditableEntity<Guid>
    {
        public string LocationName { get; private set; }
        public string Description { get; private set; }

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
