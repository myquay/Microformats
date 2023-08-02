using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-altitude - decimal altitude - new in vCard4 (RFC6350)
    /// </summary>
    [HAdr, HCard, HGeo]
    public class Altitude : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-altitude";

        public string Key => "altitude";
    }
}
