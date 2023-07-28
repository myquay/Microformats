﻿using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard.Card
{
    /// <summary>
    /// p-honorific-prefix - e.g. Mrs., Mr. or Dr.
    /// </summary>
    [HCard]
    public class HonorificPrefix : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-honorific-prefix";

        public string Key => "honorific-prefix";
    }
}
