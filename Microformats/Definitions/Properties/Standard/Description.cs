using Microformats.Definitions.Vocabularies;
using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-description - description
    /// </summary>
    [HEvent]
    public class Description : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-description";

        public string Key => "description";
    }
}
