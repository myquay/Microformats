using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Link
{
    /// <summary>
    /// u-like-of - the URL which the h-entry is considered a “like” (favorite, star) of. Optionally an embedded h-cite
    /// </summary>
    [HEntry]
    public class LikeOf : IProperty
    {
        public MType Type => MType.Url;

        public string Name => "u-like-of";

        public string Key => "like-of";
    }
}
