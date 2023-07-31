﻿using Microformats.Definitions.Vocabularies;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions.Properties.Link
{
    /// <summary>
    /// dt-updated - when the entry was updated
    /// </summary>
    [HEntry]
    public class Updated : IProperty
    {
        public MType Type => MType.DateTime;

        public string Name => "dt-updated";

        public string Key => "updated";
    }
}