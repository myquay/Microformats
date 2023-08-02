using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-contact - current contact info in an h-card
    /// </summary>
    [HResume]
    public class Contact : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-contact";

        public string Key => "contact";
    }
}
