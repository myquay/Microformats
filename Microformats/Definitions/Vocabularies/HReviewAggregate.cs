using Microformats.Definitions.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Microformats.Definitions.Vocabularies
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class HReviewAggregate : Attribute, IVocabulary
    {
        public MicroformatsVersion Version => MicroformatsVersion.Two;
        public string Name => "h-review-aggregate";
        public IProperty[] Properties => Assembly.GetCallingAssembly().GetProperties<HReviewAggregate>();
    }
}
