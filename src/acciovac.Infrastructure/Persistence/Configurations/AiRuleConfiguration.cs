using acciovac.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace acciovac.Infrastructure.Persistence.Configurations
{
    public class AiRuleConfiguration : IEntityTypeConfiguration<AiRule>
    {
        public void Configure(EntityTypeBuilder<AiRule> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Code).IsUnique();

            builder.Property(x => x.RuleText).IsRequired();
        }
    }
}
