using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties
{
    /// <summary>
    /// p-note - additional notes
    /// </summary>
    [HCard]
    public class PNote : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-note";

        public string Key => "note";
    }
}
