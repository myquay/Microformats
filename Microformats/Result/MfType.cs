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
        public string[] GetProperty(string property)
        {
            var name = property.Remove(0, 2);
            //TODO: ADD SUPPORT FOR e-*, u-* and dt-* properties
            if (Properties.ContainsKey(name))
            {
                //Suitable for p-*
                return Properties[name].Select(s => s.GetName()).ToArray();
            }
            return null;
        }       
    }
}
