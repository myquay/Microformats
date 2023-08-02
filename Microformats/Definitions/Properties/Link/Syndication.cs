using Microformats.Definitions.Properties.Standard;
using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Link
{
    /// <summary>
    /// u-syndication - URL(s) of syndicated copies of this item.The property equivalent of rel-syndication (example with syndication to Amazon)
    /// </summary>
    [HEntry, HReview]
    public class Syndication : IProperty
    {
        public MType Type => MType.Url;

        public string Name => "u-syndication";

        public string Key => "syndication";
    }
}
