using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-location - location the entry was posted from, optionally embed h-card, h-adr, or h-geo
    /// </summary>
    [HEntry, HEvent, HReview]
    public class Location : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-location";

        public string Key => "location";
    }
}
