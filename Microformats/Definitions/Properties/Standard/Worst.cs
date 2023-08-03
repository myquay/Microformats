using Microformats.Definitions.Vocabularies;
using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-worst - define worst rating value. can be numerically higher than best.
    /// </summary>
    [HReview, HReviewAggregate]
    public class Worst : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-worst";

        public string Key => "worst";
    }
}
