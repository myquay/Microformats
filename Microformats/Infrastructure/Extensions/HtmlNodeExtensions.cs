using HtmlAgilityPack;
using Microformats.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;

namespace Microformats
{
    /// <summary>
    /// HTML Node helpers
    /// </summary>
    internal static class HtmlNodeExtensions
    {
        /// <summary>
        /// Set if this node is in backwards compatible mode
        /// </summary>
        /// <param name="node">HtmlNode to operate on</param>
        /// <param name="value">If backwards compatible or not</param>
        internal static void SetBackcompat(this HtmlNode node, bool value)
        {
            node.SetAttributeValue("data-backcompat", value.ToString().ToLowerInvariant());
        }

        /// <summary>
        /// Checks to see if node is in backwards compatible mode
        /// </summary>
        /// <param name="node">HtmlNode to check</param>
        /// <returns></returns>
        internal static bool IsBackcompat(this HtmlNode node)
        {
            return node.GetAttributeValue("data-backcompat", null) == "true";
        }

        /// <summary>
        /// Checks if node is one of the listed types
        /// </summary>
        /// <param name="node">HtmlNode to check</param>
        /// <param name="elements">List of elements to check the node against</param>
        /// <returns></returns>
        internal static bool Is(this HtmlNode node, params string[] elements)
        {
            return node != null && elements.Contains(node.Name.ToLowerInvariant());
        }

        /// <summary>
        /// Checks if node has the specified attribute
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attr"></param>
        /// <param name="ignoreEmpty"></param>
        /// <returns></returns>
        internal static bool HasAttr(this HtmlNode node, string attr, bool ignoreEmpty = false)
        {
            var attribute = node.GetAttributeValue(attr, null);

            if(ignoreEmpty && string.IsNullOrWhiteSpace(attribute))
                return false;
            return attribute != null;
        }

        /// <summary>
        /// Tries to get the single child of type
        /// </summary>
        /// <param name="node"></param>
        /// <param name="element"></param>
        /// <param name="child"></param>
        /// <returns></returns>
        internal static bool TrySelectSingleChild(this HtmlNode node, string element, out HtmlNode child)
        {
            child = null;

            if (node == null || node.ChildNodes.Count(c => c.Is(element)) != 1)
                return false;

            child = node.ChildNodes.Single(c => c.Is(element));

            return true;
        }

        /// <summary>
        /// Tries to get single child element
        /// </summary>
        /// <param name="node"></param>
        /// <param name="child"></param>
        /// <returns></returns>
        internal static bool TrySelectSingleChild(this HtmlNode node, out HtmlNode child)
        {
            child = null;

            if (node == null || node.ChildNodes.Count() != 1)
                return false;

            child = node.ChildNodes.Single();
            return true;
        }

        /// <summary>
        /// Tries to get first element of type
        /// </summary>
        /// <param name="node"></param>
        /// <param name="element"></param>
        /// <param name="child"></param>
        /// <param name="onlyOfType"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Checks to see if this is a microformat root
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        internal static bool IsMicroformatRoot(this HtmlNode node)
        {
            return node.IsPropertyElement(MfType.Specification);
        }

        /// <summary>
        /// Checks to see if node is a microformat entity
        /// </summary>
        /// <param name="node"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        internal static bool IsPropertyElement(this HtmlNode node, params MfType[] includeProperties)
        {
            includeProperties = includeProperties ?? new MfType[0];
            var classesToCheck = includeProperties.Select(p =>
            {
                if(p == MfType.Property)                     
                    return "p-";
                if(p == MfType.Embedded)
                    return "e-";
                if(p == MfType.Url)
                    return "u-";
                if (p == MfType.Specification)
                    return "h-";
                if (p == MfType.DateTime)
                    return "dt-";
                return "unknown";
            });

            return node.GetClasses().Any(c => classesToCheck.Any(p => c.StartsWith(p)));
        }
    }
}
