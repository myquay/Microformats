using Microformats.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microformats.Grammar
{
    public class MfSpec
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
        /// Language of the value
        /// </summary>
        public string Lang { get; set; }

        /// <summary>
        /// Shape
        /// </summary>
        public string Shape { get; set; }

        /// <summary>
        /// Coords
        /// </summary>
        public string Coords { get; set; }

        /// <summary>
        /// The Value
        /// </summary>
        public string[] Type { get; set; }

        /// <summary>
        /// Properties of the value
        /// </summary>
        public Dictionary<string, MfValue[]> Properties { get; set; } = new Dictionary<string, MfValue[]>();

        /// <summary>
        /// Try get property value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryGet<T>(string property, out T[] result)
            where T : class
        {

            result = default;
            if (!MfProperty.TryFromName(property, out MfProperty parsedProperty))
                return false;

            var results = Properties[parsedProperty.Key].Select(s =>
            {
                var successful = s.TryGet<T>(out T value);
                return new
                {
                    value,
                    successful
                };
            });

            result = results.Where(s => s.successful).Select(s => s.value).ToArray();

            return results.All(a => !a.successful);
        }

        /// <summary>
        /// Get property value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        public T[] Get<T>(string property) 
            where T : class 
        {
            if (!MfProperty.TryFromName(property, out MfProperty parsedProperty))
                return default;

            return Properties[parsedProperty.Key].Select(s => s.Get<T>()).ToArray();
        }

        /// <summary>
        /// Get property values
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string[] Get(string property)
        {
            if (!MfProperty.TryFromName(property, out MfProperty parsedProperty))
                return default;

            if (parsedProperty.Type == MfType.Property)
            {
                if (Properties.ContainsKey(parsedProperty.Key))
                    return Properties[parsedProperty.Key].Select(s => s.GetName()).ToArray();
            }else
            {
                if (Properties.ContainsKey(parsedProperty.Key))
                    return Properties[parsedProperty.Key].Select(s => s.GetValue()).ToArray();
            }
            return Array.Empty<string>();
        }

        /// <summary>
        /// Types this spec has already parsed
        /// </summary>
        public MfType[] ParsedTypes
        {
            get
            {
                return Properties.SelectMany(s => s.Value).Select(s => s.Property.Type).Distinct().ToArray();
            }
        }

        /// <summary>
        /// Has parsed on of the specified types
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public bool HasParsedType(params MfType[] types)
        {
            return types.Any(a => ParsedTypes.Contains(a));
        }
    }
}
