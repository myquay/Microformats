﻿using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Standard
{
    /// <summary>
    /// p-region - state/county/province
    /// </summary>
    [HAdr, HCard]
    public class Region : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "p-region";

        public string Key => "region";
    }

    /// <summary>
    /// region - state/county/province
    /// </summary>
    [HAdr, HCard]
    public class RegionLegacy : IProperty
    {
        public MType Type => MType.Property;

        public string Name => "region";

        public string Key => "region";
    }
}