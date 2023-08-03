using Microformats.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Microformats
{
    internal static class AssemblyExtensions
    {
        public static IProperty[] GetProperties<T>(this Assembly assembly) where T : Attribute, IVocabulary
        {
            return assembly.GetTypes().Where(t => t.GetCustomAttributes<T>().Any() && typeof(IProperty).IsAssignableFrom(t)).Select(s => (IProperty)Activator.CreateInstance(s)).ToArray();
        }
    }
}
