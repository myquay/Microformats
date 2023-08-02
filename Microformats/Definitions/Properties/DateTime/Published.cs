using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Link
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
