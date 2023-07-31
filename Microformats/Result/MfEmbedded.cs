using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Result
{
    /// <summary>
    /// An e-* or backcompat equivalent
    /// </summary>
    public class MfEmbedded
    {
        /// <summary>
        ///  The innerHTML of the element by using the HTML spec: Serializing HTML Fragments algorithm, with leading/trailing spaces removed. Proposed: and normalized absolute URLs in all URL attributes except those that are fragment-only, e.g. start with '#'.(issue 38)
        /// </summary>
        public string Html { get; set; }

        /// <summary>
        /// The text content of the element
        /// </summary>
        public string Value { get; set; }
    }
}
