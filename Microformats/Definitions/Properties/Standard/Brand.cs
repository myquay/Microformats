using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-brand - manufacturer, can also be embedded h-card
    /// </summary>
    [HProduct]
    public class Brand : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-brand";

        public string Key => "brand";
    }
}
