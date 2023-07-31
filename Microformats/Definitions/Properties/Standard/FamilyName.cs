using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-family-name - family (often last) name
    /// </summary>
    [HCard]
    public class FamilyName : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-family-name";

        public string Key => "family-name";
    }
}
