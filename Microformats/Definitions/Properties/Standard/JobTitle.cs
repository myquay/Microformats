using Microformats.Definitions.Vocabularies;
using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-job-title - job title, previously 'title' in hCard, disambiguated.
    /// </summary>
    [HCard]
    public class JobTitle : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-job-title";

        public string Key => "job-title";
    }
}
