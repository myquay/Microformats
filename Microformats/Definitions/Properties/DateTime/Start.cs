using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.DateTime
{
    /// <summary>
    /// dt-start - when the event starts
    /// </summary>
    [HEvent]
    public class Start : IProperty
    {
        public MType Type => MType.DateTime;

        public string Name => "dt-start";

        public string Key => "start";
    }
}
