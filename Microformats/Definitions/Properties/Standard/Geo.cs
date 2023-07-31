using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-geo (or u-geo with a RFC 5870 geo: URL), optionally embedded h-geo
    /// </summary>
    [HAdr, HCard]
    public class Geo : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-geo";

        public string Key => "geo";
    }
}
