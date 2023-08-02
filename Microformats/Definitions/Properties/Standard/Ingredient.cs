using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-ingredient - describes one or more ingredients used in the recipe.
    /// </summary>
    [HRecipe]
    public class Ingredient : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-ingredient";

        public string Key => "ingredient";
    }
}
