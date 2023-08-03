using Microformats.Definitions.Vocabularies;
using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-item - thing being reviewed, including embedded microformat for e.g. business or person (h-card), event (h-event), place (h-adr or h-geo), product (h-product), recipe (h-recipe), website, url, or other item (h-item).
    /// </summary>
    [HReview, HReviewAggregate]
    public class Item : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-item";

        public string Key => "item";
    }
}
