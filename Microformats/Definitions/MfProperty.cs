using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions
{
    /// <summary>
    /// Property of a microformat
    /// </summary>
    public class MfProperty
    {
        private readonly string name;

        public MfProperty(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Property Name e.g. p-name
        /// </summary>
        public string Name => name;

        /// <summary>
        /// Property Key e.g. name
        /// </summary>
        public string Key
        {
            get
            {
                if(Type == MfType.Unknown)
                {
                    return name;
                }
                else if (Type == MfType.DateTime)
                {
                    return name.Substring(3);
                }
                else
                {
                    return name.Substring(2);
                }
            }
        }

        /// <summary>
        /// Type of the property
        /// </summary>
        public MfType Type {
            get
            {
                if(name.StartsWith("h-"))
                {
                    return MfType.Specification;
                }
                else if(name.StartsWith("p-"))
                {
                    return MfType.Property;
                }
                else if (name.StartsWith("dt-"))
                {
                    return MfType.DateTime;
                }
                else if (name.StartsWith("u-"))
                {
                    return MfType.Url;
                }
                else if (name.StartsWith("e-"))
                {
                    return MfType.Embedded;
                }

                return MfType.Unknown;
            }
        }

        /// <summary>
        /// Try create from property name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryFromName(string name, out MfProperty result)
        {
            result = new MfProperty(name);
            return result.Type != MfType.Unknown;
        }

        /// <summary>
        /// Check if a property is valid
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsValid(string name)
        {
            return TryFromName(name, out _);
        }

        /// <summary>
        /// Check if property is of type
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsOfType(string name, MfType type)
        {
            return TryFromName(name, out MfProperty result) && result.Type == type;
        }
    }
}

