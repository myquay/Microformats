﻿using System;
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
        /// Whether to discover language from HTML
        /// </summary>
        public bool DiscoverLang { get; set; } = true;  

    }
}
