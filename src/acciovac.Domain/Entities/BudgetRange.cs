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
        public byte Type { get; set; }
        public decimal FromBudget { get; set; }
        public decimal ToBudget { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
