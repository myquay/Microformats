using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    ///p-action: text, optional, one of the following values
    ///sell, rent, trade, meet, announce, offer, wanted, event, service
    /// </summary>
    [HListing]
    public class Action : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-action";

        public string Key => "action";
    }
}
