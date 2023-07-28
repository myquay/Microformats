using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard.Address
{
    /// <summary>
    /// p-postal-code - postal code, e.g. ZIP in the US
    /// </summary>
    [HAdr]
    public class PostalCode : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-postal-code";

        public string Key => "postal-code";
    }

    /// <summary>
    /// postal-code - postal code, e.g. ZIP in the US
    /// </summary>
    [HAdr]
    public class PostalCodeLegacy : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "postal-code";

        public string Key => "postal-code";
    }
}
