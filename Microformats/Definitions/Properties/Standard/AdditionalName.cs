using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-additional-name - other (e.g. middle) name
    /// </summary>
    [HCard]
    public class AdditionalName : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-additional-name";

        public string Key => "additional-name";
    }
}
