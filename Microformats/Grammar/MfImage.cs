using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Grammar
{
    /// <summary>
    /// To parse an img element for src and alt attributes:
    /// </summary>
    public class MfImage
    {
        /// <summary>
        ///  The element's src attribute as a normalized absolute URL, following the containing document's language's rules for resolving relative URLs (e.g. in HTML, use the current URL context as determined by the page, and first <base> element, if any).
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The element's alt attribute
        /// </summary>
        public string Alt { get; set; }
    }
}
