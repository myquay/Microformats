using Microformats.Definitions.Vocabularies;
using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-count - the total number of reviews
    /// </summary>
    [HReviewAggregate]
    public class Count : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-count";

        public string Key => "count";
    }
}
