using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-best - define best rating value. can be numerically lower than worst.
    /// </summary>
    [HReview]
    public class Best : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-best";

        public string Key => "p-best";
    }
}
