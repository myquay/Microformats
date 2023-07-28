using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard.Address
{
    /// <summary>
    /// p-street-address - house/apartment number, floor, street name
    /// </summary>
    [HAdr, HCard]
    public class StreetAddress : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-street-address";

        public string Key => "street-address";
    }

    /// <summary>
    /// street-address - post office mailbox
    /// </summary>
    [HAdr, HCard]
    public class StreetAddressLegacy : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "street-address";

        public string Key => "street-address";
    }
}
