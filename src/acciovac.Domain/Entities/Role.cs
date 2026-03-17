using acciovac.Domain.Common;
using System;
using System.Collections.Generic;

namespace acciovac.Domain.Entities
{
    public class Role : Entity<Guid>
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();

        private Role() { }

        public Role(Guid id, string name, string? description = null)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
