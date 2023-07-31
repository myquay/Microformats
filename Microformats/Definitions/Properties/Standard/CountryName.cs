using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-country-name - should be full name of country, country code ok
    /// </summary>
    [HAdr, HCard]
    public class CountryName : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-country-name";

        public string Key => "country-name";
    }

    /// <summary>
    /// country-name - should be full name of country, country code ok
    /// </summary>
    [HAdr, HCard]
    public class CountryNameLegacy : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "country-name";

        public string Key => "country-name";
    }
}
