using Microformats.Definitions.Vocabularies;
using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.DateTime
{
    /// <summary>
    /// dt-duration - the duration of the event
    /// </summary>
    [HEvent, HRecipe]
    public class Duration : IProperty
    {
        public MType Type => MType.DateTime;

        public string Name => "dt-duration";

        public string Key => "duration";
    }
}
