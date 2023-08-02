using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-label - a mailing label, plain text, perhaps with preformatting
    /// </summary>
    [HAdr, HCard]
    public class Label : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-label";

        public string Key => "label";
    }
}
