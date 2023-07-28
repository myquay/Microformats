using Microformats.Definitions.Properties.Standard;
using Microformats.Definitions.Properties.Link;
using System;
using System.Collections.Generic;
using System.Text;
using Microformats.Definitions.Properties.Standard.Card;

namespace Microformats.Definitions
{
    /// <summary>
    /// Properties
    /// </summary>
    public class Props
    {

        public static Category Category { get; } = new Category();
        public static PName Name { get; } = new PName();
        public static Note Note { get; } = new Note();
        public static Org Org { get; } = new Org();
        public static Photo Photo { get; } = new Photo();
        public static Url Url { get; } = new Url();
    }
}
