using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-skill - a skill or ability, optionally including level and/or duration of experience
    /// </summary>
    [HResume]
    public class Skill : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-skill";

        public string Key => "skill";
    }
}
