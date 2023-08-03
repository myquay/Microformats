using Microformats.Definitions.Vocabularies;
using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-gender-identity - gender identity, new in vCard4 (RFC 6350)
    /// </summary>
    [HCard]
    public class GenderIdentity : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-gender-identity";

        public string Key => "gender-identity";
    }
}
