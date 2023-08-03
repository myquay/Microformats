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
        public MfType[] Items { get; set; } = Array.Empty<MfType>();

        /// <summary>
        /// Rel values present
        /// </summary>
        public Dictionary<string, string[]> Rels { get; set; } = new Dictionary<string, string[]>();

        /// <summary>
        /// Rel-urls values present
        /// </summary>
        public Dictionary<string, Dictionary<string, object>> RelUrls { get; set; } = new Dictionary<string, Dictionary<string, object>>();
    }
}
