using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard.Address
{
    /// <summary>
    /// p-extended-address - additional street details
    /// </summary>
    [HAdr]
    public class ExtendedAddress : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-extended-address";

        public string Key => "extended-address";
    }

    /// <summary>
    /// extended-address - post office mailbox
    /// </summary>
    [HAdr]
    public class ExtendedAddressLegacy : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "extended-address";

        public string Key => "extended-address";
    }
}
