using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-education - an education h-event event, years, embedded h-card of the school, location.
    /// </summary>
    [HResume]
    public class Education : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-education";

        public string Key => "education";
    }
}
