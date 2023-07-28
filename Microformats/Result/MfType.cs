using Microformats.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microformats.Result
{
    public class MfType
    {
        /// <summary>
        /// Id of the value (if specified)
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Value if not root
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The Value
        /// </summary>
        public string[] Type { get; set; }

        /// <summary>
        /// Properties of the value
        /// </summary>
        public Dictionary<string, MfValue[]> Properties { get; set; } = new Dictionary<string, MfValue[]>();

        /// <summary>
        /// Get property values
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string[] GetProperty(IProperty property)
        {
            //TODO: ADD SUPPORT FOR e-*, and dt-* properties
            if (property.Type == MType.Property)
            {
                if (Properties.ContainsKey(property.Key))
                    return Properties[property.Key].Select(s => s.GetName()).ToArray();
            }else
            {
                if (Properties.ContainsKey(property.Key))
                    return Properties[property.Key].Select(s => s.GetValue()).ToArray();
            }
            return null;
        }

    }
}
