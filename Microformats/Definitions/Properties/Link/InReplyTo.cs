using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Link
{
    /// <summary>
    /// u-in-reply-to - the URL which the h-entry is considered reply to (i.e. doesn’t make sense without context, could show up in comment thread), optionally an embedded h-cite (reply-context)
    /// </summary>
    [HEntry]
    public class InReplyTo : IProperty
    {
        public MType Type => MType.Url;

        public string Name => "u-in-reply-to";

        public string Key => "in-reply-to";
    }
}
