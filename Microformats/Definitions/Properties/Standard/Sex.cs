using Microformats.Definitions.Vocabularies;
using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-sex - biological sex, new in vCard4 (RFC 6350)
    /// </summary>
    [HCard]
    public class Sex : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-sex";

        public string Key => "sex";
    }
}
