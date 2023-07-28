using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Link
{
    /// <summary>
    /// u-logo - a logo representing the person or organization (e.g. a face icon)
    /// </summary>
    [HCard]
    public class Logo : IProperty
    {
        public MType Type => MType.Url;

        public string Name => "u-logo";

        public string Key => "logo";
    }
}
