using Microformats.Definitions.Vocabularies;
using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Link
{
    /// <summary>
    /// u-url - home page or other URL representing the person or organization
    /// </summary>
    [HCard, HEntry, HEvent, HFeed, HItem, HListing, HProduct, HReview]
    public class Url : IProperty
    {
        public MType Type => MType.Url;

        public string Name => "u-url";

        public string Key => "url";
    }
}
