using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard.Address
{
    /// <summary>
    /// p-locality - city/town/village
    /// </summary>
    [HAdr]
    public class Locality : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-locality";

        public string Key => "locality";
    }

    /// <summary>
    /// locality - city/town/village
    /// </summary>
    [HAdr]
    public class LocalityLegacy : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "locality";

        public string Key => "locality";
    }
}
