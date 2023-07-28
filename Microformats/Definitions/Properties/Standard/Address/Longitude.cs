using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard.Address
{
    /// <summary>
    /// p-longitude - decimal longitude
    /// </summary>
    [HAdr]
    public class Longitude : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-longitude";

        public string Key => "longitude";
    }
}
