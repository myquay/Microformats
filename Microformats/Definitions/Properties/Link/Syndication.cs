using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Link
{
    /// <summary>
    /// u-url - home page or other URL representing the person or organization
    /// </summary>
    [HEntry]
    public class Syndication : IProperty
    {
        public MType Type => MType.Url;

        public string Name => "u-syndication";

        public string Key => "syndication";
    }
}
