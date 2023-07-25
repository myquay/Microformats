using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Result
{
    /// <summary>
    /// Result of parsing a document for microformats
    /// </summary>
    public class MResult
    {
        /// <summary>
        /// Items present
        /// </summary>
        public MValue[] Items { get; set; } = Array.Empty<MValue>();

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
