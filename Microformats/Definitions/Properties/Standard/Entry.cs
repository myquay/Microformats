using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-entry - entry in feed
    /// </summary>
    [HFeed]
    public class Entry : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-entry";

        public string Key => "entry";
    }

    /// <summary>
    /// Implied entry propery in feed
    /// </summary>
    [HFeed]
    public class ImpliedEntry : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "h-entry";

        public string Key => "entry";
    }
}
