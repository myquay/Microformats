using Microformats.Definitions.Vocabularies;
using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-note - additional notes
    /// </summary>
    [HCard]
    public class Note : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-note";

        public string Key => "note";
    }
}
