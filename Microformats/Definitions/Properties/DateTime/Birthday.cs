using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Link
{
    /// <summary>
    /// dt-bday - birth date
    /// </summary>
    [HCard]
    public class Birthday : IProperty
    {
        public MType Type => MType.DateTime;

        public string Name => "dt-bday";

        public string Key => "bday";
    }
}
