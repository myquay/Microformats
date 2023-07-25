using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions
{
    public interface IProperty
    {
        MType Type { get; }
        string Name { get; }
    }
}
