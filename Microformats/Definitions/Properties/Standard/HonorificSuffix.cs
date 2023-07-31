using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-honorific-suffix - e.g. Ph.D, Esq.
    /// </summary>
    [HCard]
    public class HonorificSuffix : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-honorific-suffix";

        public string Key => "honorific-suffix";
    }
}
