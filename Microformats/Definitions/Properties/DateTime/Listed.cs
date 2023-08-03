using Microformats.Definitions.Vocabularies;
using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.DateTime
{
    /// <summary>
    /// dt-listed: datetime, optional
    /// </summary>
    [HListing]
    public class Listed : IProperty
    {
        public MType Type => MType.DateTime;

        public string Name => "dt-listed";

        public string Key => "listed";
    }
}
