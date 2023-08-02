﻿using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Link
{
    /// <summary>
    /// dt-end - when the event ends
    /// </summary>
    [HEvent]
    public class End : IProperty
    {
        public MType Type => MType.DateTime;

        public string Name => "dt-end";

        public string Key => "end";
    }
}