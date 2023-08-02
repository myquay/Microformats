using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Embedded
{
    /// <summary>
    /// e-instructions - the method of the recipe.
    /// </summary>
    [HRecipe]
    public class Instructions : IProperty
    {
        public MType Type => MType.Embedded;

        public string Name => "e-instructions";

        public string Key => "instructions";
    }
}
