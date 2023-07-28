using Microformats.Definitions.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions
{
    /// <summary>
    /// Properties
    /// </summary>
    public class Props
    {

        public static PCategory PCategory { get; } = new PCategory();
        public static PName PName { get; } = new PName();
        public static PNote PNote { get; } = new PNote();
        public static POrg POrg { get; } = new POrg();

        public static UPhoto UPhoto { get; } = new UPhoto();

        public static UUrl UUrl { get; } = new UUrl();
    }
}
