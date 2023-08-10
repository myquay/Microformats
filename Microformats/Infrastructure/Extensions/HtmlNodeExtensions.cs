using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;

namespace Microformats
{
    internal static class HtmlNodeExtensions
    {
        internal static bool Is(this HtmlNode node, params string[] elements)
        {
            return node != null && elements.Contains(node.Name.ToLowerInvariant());
        }

        internal static bool HasAttr(this HtmlNode node, string attr, bool ignoreEmpty = false)
        {
            var attribute = node.GetAttributeValue(attr, null);

            if(ignoreEmpty && string.IsNullOrWhiteSpace(attribute))
                return false;
            return attribute != null;
        }

        internal static bool TrySelectSingleChild(this HtmlNode node, string element, out HtmlNode child)
        {
            child = null;

            if (node == null || node.ChildNodes.Count(c => c.Is(element)) != 1)
                return false;

            child = node.ChildNodes.Single(c => c.Is(element));

            return true;
        }

        internal static bool TrySelectSingleChild(this HtmlNode node, out HtmlNode child)
        {
            child = null;

            if (node == null || node.ChildNodes.Count() != 1)
                return false;

            child = node.ChildNodes.Single();
            return true;
        }

        internal static bool TrySelectFirstChild(this HtmlNode node, string element, out HtmlNode child, bool onlyOfType = false)
        {
            child = null;

            if (node == null || !node.ChildNodes.Any(c => c.Is(element)))
                return false;

            if(onlyOfType && node.ChildNodes.Count(c => c.Is(element)) > 1)
                return false;

            child = onlyOfType ? node.ChildNodes.Single(c => c.Is(element)) : node.ChildNodes.First(c => c.Is(element));

            return true;
        }

        internal static bool IsMicoformatEntity(this HtmlNode node, bool includeProperties = false)
        {
            if (!includeProperties)
            {
                return node.GetClasses().Any(c => c.StartsWith("h-"));
            }
            else
            {
                return node.GetClasses().Any(c => c.StartsWith("h-") || c.StartsWith("p-") || c.StartsWith("e-") || c.StartsWith("u-") || c.StartsWith("dt-"));
            }
        }
    }
}
