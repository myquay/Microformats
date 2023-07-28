using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard.Card
{
    /// <summary>
    /// p-category - category/tag
    /// </summary>
    [HCard]
    public class Category : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-category";

        public string Key => "category";
    }
}
