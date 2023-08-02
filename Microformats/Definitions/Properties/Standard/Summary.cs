using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-summary - short entry summary
    /// </summary>
    [HEntry, HEvent, HFeed, HRecipe]
    public class Summary : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-summary";

        public string Key => "summary";
    }
}
