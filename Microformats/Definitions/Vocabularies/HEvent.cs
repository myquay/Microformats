using Microformats.Definitions.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Microformats.Definitions.Vocabularies
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class HEvent : Attribute, IVocabulary
    {
        public MicroformatsVersion Version => MicroformatsVersion.Two;
        public string Name => "h-event";
        public IProperty[] Properties => Assembly.GetCallingAssembly().GetProperties<HEvent>();
    }
}
