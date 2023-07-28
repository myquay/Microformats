using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Link
{
    /// <summary>
    /// u-key - cryptographic public key e.g. SSH or GPG
    /// </summary>
    [HCard]
    public class CKey : IProperty
    {
        public MType Type => MType.Url;

        public string Name => "u-key";

        public string Key => "key";
    }
}
