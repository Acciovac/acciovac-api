using acciovac.Domain.Common;
using acciovac.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace acciovac.Domain.Entities
{
    public class BudgetRange : AuditableEntity<int>
    {
        public int Id { get; set; }
        public byte Type { get; private set; }
        public decimal FromBudget { get; private set; }
        public decimal ToBudget { get; private set; }
        public bool IsActive { get; private set; } = true;

        private BudgetRange() { }

        public BudgetRange(byte type, decimal fromBudget, decimal toBudget, bool isActive, string createdBy = "system")
        {
            Type = type;
            FromBudget = fromBudget;
            ToBudget = toBudget;
            IsActive = isActive;
            SetCreated(createdBy);
        }

        public void Update(byte type, decimal fromBudget, decimal toBudget, bool isActive, string modifiedBy = "system")
        {
            Type = type;
            FromBudget = fromBudget;
            ToBudget = toBudget;
            IsActive = isActive;
            SetModified(modifiedBy);
        }
    }
}
