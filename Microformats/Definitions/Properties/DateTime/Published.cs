using Microformats.Definitions.Vocabularies;
using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.DateTime
{
    /// <summary>
    /// dt-published - when the entry was published
    /// </summary>
    [HEntry, HRecipe, HReview]
    public class Published : IProperty
    {
        public MType Type => MType.DateTime;

        public string Name => "dt-published";

        public string Key => "published";
    }
}
