using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-review - a review of the product, optionally embedded h-review
    /// </summary>
    [HProduct]
    public class Review : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-review";

        public string Key => "review";
    }
}
