using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-experience - a job or other professional experience h-event event, years, embedded h-card of the organization, location, job-title.
    /// </summary>
    [HResume]
    public class Experience : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-experience";

        public string Key => "experience";
    }
}
