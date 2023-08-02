using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-yield - Specifies the quantity produced by the recipe, like how many persons it satisfies
    /// </summary>
    [HRecipe]
    public class Yield : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-yield";

        public string Key => "yield";
    }
}
