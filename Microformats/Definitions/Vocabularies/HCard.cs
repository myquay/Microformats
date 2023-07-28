using Microformats.Definitions.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Microformats.Definitions.Vocabularies
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class HCard : Attribute, IVocabulary
    {
        public MicroformatsVersion Version => MicroformatsVersion.Two;
        public string Name => "h-card";
        public IProperty[] Properties => Assembly.GetCallingAssembly().GetProperties<HCard>();
    }
}
