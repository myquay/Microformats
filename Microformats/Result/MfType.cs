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

        public bool TryGet<P, T>(out T[] result)
            where T : class
            where P : IProperty, new()
        {
            var property = new P();
            result = default;

            var results = Properties[property.Key].Select(s =>
            {
                var successful = s.TryGet<T>(out T value);
                return new
                {
                    value,
                    successful
                };
            });

            result = results.Where(s => s.successful).Select(s => s.value).ToArray();

            return results.Any(a => !a.successful);
        }

        public T[] Get<P, T>() 
            where T : class 
            where P : IProperty, new()
        {
            return Properties[new P().Key].Select(s => s.Get<T>()).ToArray();
        }

        /// <summary>
        /// Get property values
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string[] Get<P>()
            where P : IProperty, new()
        {
            var property = new P();

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
