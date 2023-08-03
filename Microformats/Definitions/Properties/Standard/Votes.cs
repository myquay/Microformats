using Microformats.Definitions.Vocabularies;
using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-votes - the total number of users who have rated the product or service, contributing to the average rating. For some sites, the number of votes is equal to the number of reviews, so count may be used and this property omitted.
    /// </summary>
    [HReviewAggregate]
    public class Votes : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-votes";

        public string Key => "votes";
    }
}
