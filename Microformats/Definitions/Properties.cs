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

        public static PName PName { get; } = new PName();

        public static UPhoto UPhoto { get; } = new UPhoto();

        public static UUrl UUrl { get; } = new UUrl();
    }
}
