using System;
using System.Collections.Generic;

namespace Microformats.Result
{
    public class MValue
    {
        /// <summary>
        /// The Type (defines serialisation behaviour)
        /// </summary>
        public MType Type { get; set; }

        /// <summary>
        /// Id of the value (if specified)
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The Value
        /// </summary>
        public string[] Value { get; set; }

        /// <summary>
        /// Properties of the value
        /// </summary>
        public Dictionary<string, MValue> Properties { get; set; }
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
