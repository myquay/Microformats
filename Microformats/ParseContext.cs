using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Microformats
{
    /// <summary>
    /// This class is used to pass context information between parsing different parts of a microformat.
    /// </summary>
    internal class ParseContext
    {
        /// <summary>
        /// Most recent date found in the microformat.
        /// </summary>
        internal string MostRecentDate { get; set; }

        /// <summary>
        /// Most recent timezone found in the microformat.
        /// </summary>
        internal string MostRecentTimezone { get; set; }
    }
}
