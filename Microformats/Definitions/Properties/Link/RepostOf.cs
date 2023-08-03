using Microformats.Definitions.Vocabularies;
using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Link
{
    /// <summary>
    /// u-repost-of - the URL which the h-entry is considered a “repost” of. Optionally an embedded h-cite.
    /// </summary>
    [HEntry]
    public class RepostOf : IProperty
    {
        public MType Type => MType.Url;

        public string Name => "u-repost-of";

        public string Key => "repost-of";
    }
}
