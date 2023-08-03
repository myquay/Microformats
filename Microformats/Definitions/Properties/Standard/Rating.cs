using Microformats.Definitions.Vocabularies;
using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-rating - value from 1-5 indicating a rating for the item (5 best).
    /// </summary>
    [HReview]
    public class Rating : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-rating";

        public string Key => "rating";
    }
}
