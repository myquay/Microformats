using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties
{
    public class PName : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-name";
    }
}
