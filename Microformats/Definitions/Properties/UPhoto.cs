﻿using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties
{
    /// <summary>
    /// u-photo - a photo of the person or organization
    /// </summary>
    [HCard]
    public class UPhoto : IProperty
    {
        public MType Type => MType.Url;

        public string Name => "u-photo";

        public string Key => "photo";
    }
}
