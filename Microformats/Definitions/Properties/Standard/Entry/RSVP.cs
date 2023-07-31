using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard.Card
{
    /// <summary>
    /// p-rsvp (enum, use <data> element or value-class-pattern)
    /// </summary>
    [HEntry]
    public class Rsvp : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-rsvp";

        public string Key => "rsvp";
    }
}
