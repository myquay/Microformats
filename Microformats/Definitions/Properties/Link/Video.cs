using Microformats.Definitions.Vocabularies;
using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Link
{
    /// <summary>
    /// u-video - video
    /// </summary>
    [HEntry]
    public class Video : IProperty
    {
        public MType Type => MType.Url;

        public string Name => "u-video";

        public string Key => "video";
    }
}
