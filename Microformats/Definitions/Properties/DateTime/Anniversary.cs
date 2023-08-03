using Microformats.Definitions.Vocabularies;
using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.DateTime
{
    /// <summary>
    /// dt-anniversary
    /// </summary>
    [HCard]
    public class Anniversary : IProperty
    {
        public MType Type => MType.DateTime;

        public string Name => "dt-anniversary";

        public string Key => "anniversary";
    }
}
