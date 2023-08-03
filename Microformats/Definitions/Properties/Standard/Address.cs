using Microformats.Definitions.Vocabularies;
using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-adr - postal address, optionally embed an h-adr
    /// </summary>
    [HCard]
    public class Address : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-adr";

        public string Key => "adr";
    }
}
