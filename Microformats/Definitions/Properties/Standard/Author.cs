using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-author - who wrote the entry, optionally embedded h-card(s)
    /// </summary>
    [HEntry]
    public class Author : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-author";

        public string Key => "author";
    }
}
