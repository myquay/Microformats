using Microformats.Definitions.Vocabularies;
using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-price: text, optional (should include a floating-point number with optional ISO currency codes)
    /// </summary>
    [HListing, HProduct]
    public class Price : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-price";

        public string Key => "price";
    }
}
