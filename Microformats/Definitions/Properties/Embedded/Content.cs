using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Embedded
{
    /// <summary>
    /// e-content - full content of the entry
    /// </summary>
    [HEntry]
    public class Content : IProperty
    {
        public MType Type => MType.Embedded;

        public string Name => "e-content";

        public string Key => "content";
    }
}
