using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions
{
    public interface IProperty
    {
        MType Type { get; }
        string Name { get; }
        string Key { get; }
    }

    public enum MType
    {
        Root,
        Property,
        Url,
        DateTime,
        Embedded
    }
}
