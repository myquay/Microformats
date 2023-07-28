using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard.Address
{
    /// <summary>
    /// p-post-office-box - post office mailbox
    /// </summary>
    [HAdr, HCard]
    public class PostOfficeBox : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-post-office-box";

        public string Key => "post-office-box";
    }

    /// <summary>
    /// post-office-box - post office mailbox
    /// </summary>
    [HAdr, HCard]
    public class PostOfficeBoxLegacy : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "post-office-box";

        public string Key => "post-office-box";
    }
}
