using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Link
{
    /// <summary>
    /// u-photo - a photo of the person or organization
    /// </summary>
    [HCard, HFeed, HItem]
    public class Photo : IProperty
    {
        public MType Type => MType.Url;

        public string Name => "u-photo";

        public string Key => "photo";
    }
}
