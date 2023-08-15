using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Microformats.Grammar
{
    /// <summary>
    /// Result of parsing a document for microformats
    /// </summary>
    public class MfResult
    {
        /// <summary>
        /// Items present
        /// </summary>
        public MfSpec[] Items { get; set; } = Array.Empty<MfSpec>();

        /// <summary>
        /// Rel values present
        /// </summary>
        public Dictionary<string, string[]> Rels { get; set; } = new Dictionary<string, string[]>();

        /// <summary>
        /// Rel-urls values present
        /// </summary>
        public Dictionary<string, MfRelUrlResult> RelUrls { get; set; } = new Dictionary<string, MfRelUrlResult>();
    }

    public class MfRelUrlResult
    {
        public string[] Rels { get; set; } = Array.Empty<string>();
        public string Text { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Media { get; set; }
        public string HrefLang { get; set; }
    }
}
