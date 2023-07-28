using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Link
{
    /// <summary>
    /// u-sound - sound file containing the proper pronunciation of the name property, per vCard (RFC 6350).
    /// </summary>
    [HCard]
    public class Sound : IProperty
    {
        public MType Type => MType.Url;

        public string Name => "u-sound";

        public string Key => "sound";
    }
}
