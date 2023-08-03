using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Microformats
{
    /// <summary>
    /// Parsing options
    /// </summary>
    public class Mf2Options
    {
        /// <summary>
        /// Base URI for relative URIs
        /// </summary>
        public Uri BaseUri { get; set; } = default;

        /// <summary>
        /// Whether to upgrade classic microformats for parsing
        /// </summary>
        public bool UpgradeClassicMicroformats { get; set; } = true;

        /// <summary>
        /// Additional vocabularies to use for parsing
        /// </summary>
        public Assembly[] AdditionalVocabularies { get; set; } = Array.Empty<Assembly>();
    }
}
