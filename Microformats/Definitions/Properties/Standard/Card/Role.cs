using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard.Card
{
    /// <summary>
    /// p-role - description of role
    /// </summary>
    [HCard]
    public class Role : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-role";

        public string Key => "role";
    }
}
