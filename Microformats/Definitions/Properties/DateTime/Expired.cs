using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.DateTime
{
    /// <summary>
    /// dt-expired: datetime, optional
    /// </summary>
    [HListing]
    public class Expired : IProperty
    {
        public MType Type => MType.DateTime;

        public string Name => "dt-expired";

        public string Key => "expired";
    }
}
