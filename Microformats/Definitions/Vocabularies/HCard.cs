using Microformats.Definitions.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Vocabularies
{
    public class HCard : IVocabulary
    {
        public MicroformatsVersion Version => MicroformatsVersion.Two;
        public string Name => "h-card";
        public IProperty[] Properties => new[] { new PName() };
    }
}
