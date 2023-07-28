using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Link
{
    /// <summary>
    /// u-email - email address
    /// </summary>
    [HCard]
    public class Email : IProperty
    {
        public MType Type => MType.Url;

        public string Name => "u-email";

        public string Key => "email";
    }
}
