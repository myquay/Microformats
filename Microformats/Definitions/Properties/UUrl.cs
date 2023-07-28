using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties
{
    /// <summary>
    /// u-url - home page or other URL representing the person or organization
    /// </summary>
    public class UUrl : IProperty
    {
        public MType Type => MType.Url;

        public string Name => "u-url";

        public string Key => "url";
    }
}
