using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties
{
    /// <summary>
    /// p-name - The full/formatted name of the person or organization
    /// </summary>
    public class PName : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-name";

        public string Key => "name";
    }
}
