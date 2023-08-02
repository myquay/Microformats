using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-affiliation - an affiliation with an h-card organization
    /// </summary>
    [HResume]
    public class Affiliation : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-affiliation";

        public string Key => "affiliation";
    }
}
