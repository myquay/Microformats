using Microformats.Definitions.Vocabularies;
using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-average - Required: the fixed point integer [1.0-5.0] of the average rating (5.0 best)
    /// </summary>
    [HReviewAggregate]
    public class Average : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-average";

        public string Key => "average";
    }
}
