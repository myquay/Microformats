using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-tel - telephone number
    /// </summary>
    [HCard]
    public class Tel : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-tel";

        public string Key => "tel";
    }
}
