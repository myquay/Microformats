using Microformats.Definitions.Vocabularies;
using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-longitude - decimal longitude
    /// </summary>
    [HAdr, HCard, HEvent, HGeo]
    public class Longitude : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-longitude";

        public string Key => "longitude";
    }
}
