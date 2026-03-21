using System;
using System.Collections.Generic;
using System.Text;

namespace acciovac.Infrastructure.Configuration
{
    public class AiOptions
    {
        public const string SectionName = "Gemini";
        public string ApiKey { get; set; } = "AIzaSyC_VEdddcceQTp26dGPy5AlA_pn7jPKcZE";
        public string ModelId { get; set; } = "gemini-3-flash-preview";
    }
}
