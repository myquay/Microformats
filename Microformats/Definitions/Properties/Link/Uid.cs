using Microformats.Definitions.Vocabularies;
using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Link
{
    /// <summary>
    /// u-uid - universally unique identifier, preferably canonical URL
    /// </summary>
    [HCard, HEntry]
    public class Uid : IProperty
    {
        public MType Type => MType.Url;

        public string Name => "u-uid";

        public string Key => "uid";
    }
}
