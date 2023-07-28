using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Link
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
