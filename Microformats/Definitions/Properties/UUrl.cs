﻿using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties
{
    public class UUrl : IProperty
    {
        public MType Type => MType.Url;

        public string Name => "u-url";
    }
}