using Microformats.Definitions.Vocabularies;
using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
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
