using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-nutrition - nutritional information like calories, fat, dietary fiber etc.
    /// </summary>
    [HRecipe]
    public class Nutrition : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-nutrition";

        public string Key => "nutrition";
    }
}
