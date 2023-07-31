﻿using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard.Card
{
    /// <summary>
    /// p-org - affiliated organization, optionally embed an h-card
    /// </summary>
    [HCard]
    public class Org : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-org";

        public string Key => "org";
    }
}