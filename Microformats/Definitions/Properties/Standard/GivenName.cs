using Microformats.Definitions.Vocabularies;
using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-given-name - given (often first) name
    /// </summary>
    [HCard]
    public class GivenName : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-given-name";

        public string Key => "given-name";
    }
}
