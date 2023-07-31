using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-latitude - decimal latitude
    /// </summary>
    [HAdr, HCard]
    public class Latitude : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-latitude";

        public string Key => "latitude";
    }
}
