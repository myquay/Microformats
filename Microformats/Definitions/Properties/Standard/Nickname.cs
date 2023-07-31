using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-nickname - nickname/alias/handle
    /// </summary>
    [HCard]
    public class Nickname : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-nickname";

        public string Key => "nickname";
    }
}
