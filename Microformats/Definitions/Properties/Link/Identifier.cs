using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Link
{
    /// <summary>
    /// u-identifier - includes type (e.g. mpn, upc, isbn, issn, sn, vin, sku etc.) and value.
    /// </summary>
    [HProduct]
    public class Identifier : IProperty
    {
        public MType Type => MType.Url;

        public string Name => "u-identifier";

        public string Key => "identifier";
    }
}
