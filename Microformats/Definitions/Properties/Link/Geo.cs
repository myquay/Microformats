using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Link
{
    /// <summary>
    /// p-geo (or u-geo with a RFC 5870 geo: URL), optionally embedded h-geo
    /// </summary>
    [HAdr]
    public class Geo : IProperty
    {
        public MType Type => MType.Url;

        public string Name => "u-geo";

        public string Key => "geo";
    }
}
