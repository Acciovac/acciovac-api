using System;
using System.Collections.Generic;
using System.Text;

namespace acciovac.Infrastructure.Configuration
{
    public class AiOptions
    {
        public const string SectionName = "Gemini";
        public string ApiKey { get; set; } = string.Empty;
        public string ModelId { get; set; } = string.Empty;
    }
}
