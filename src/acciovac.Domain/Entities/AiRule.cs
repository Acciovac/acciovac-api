using acciovac.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace acciovac.Domain.Entities
{
    public class AiRule : Entity<Guid>
    {
        public string Code { get; private set; }
        public string RuleText { get; private set; }
        public int Priority { get; private set; }
        public bool IsActive { get; private set; }

        private AiRule() { }

        public AiRule(string code, string ruleText, int priority)
        {
            Id = Guid.NewGuid();
            Code = code;
            RuleText = ruleText;
            Priority = priority;
            IsActive = true;
        }
    }
}
