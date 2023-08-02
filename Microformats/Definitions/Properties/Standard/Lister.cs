using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-lister: h-card
    /// </summary>
    [HListing]
    public class Lister : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-lister";

        public string Key => "lister";
    }
}
