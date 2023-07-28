using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard.Card
{
    /// <summary>
    /// p-sort-string - string to sort by
    /// </summary>
    [HCard]
    public class SortString : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-sort-string";

        public string Key => "sort-string";
    }
}
