using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Embedded
{
    /// <summary>
    /// e-description
    /// </summary>
    [HProduct]
    public class Description : IProperty
    {
        public MType Type => MType.Embedded;

        public string Name => "e-description";

        public string Key => "description";
    }
}
