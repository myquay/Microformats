using Microformats.Definitions.Vocabularies;
using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Link
{
    /// <summary>
    /// u-impp per RFC4770, new in vCard4 (RFC 6350)
    /// </summary>
    [HCard]
    public class Impp : IProperty
    {
        public MType Type => MType.Url;

        public string Name => "u-impp";

        public string Key => "impp";
    }
}
