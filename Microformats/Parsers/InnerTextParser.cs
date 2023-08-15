using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Microformats.Parsers
{
    /// <summary>
    /// Inner text parser
    /// </summary>
    internal class InnerTextParser
    {

        private readonly static string[] TAGS_TO_DROP = new string[] { "script", "style" };

        /// <summary>
        /// Get the inner text of a node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="replaceImage"></param>
        /// <param name="convertImgToSrc"></param>
        /// <returns></returns>
        public static string GetInnerText(HtmlNode node, Uri baseUri, bool replaceImage = true, bool convertImgToSrc = false)
        {
            var elements = GetInnerTextElements(node, baseUri, replaceImage, convertImgToSrc)
                .Select(s => Regex.Replace(s, @"[ ]+", " "));

            if(!elements.Any())
                return String.Empty;

            if (elements.Count() == 1)
                return elements.First().Trim();

            return elements.Aggregate((a, b) =>
                {
                    if(Regex.IsMatch(a, @"\s$") && Regex.IsMatch(b, @"^\s+"))
                        return $"{a}{b.Substring(1)}";
                    else
                        return $"{a}{b}";  
                }).Trim();
        }

        /// <summary>
        /// Get text elements from node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="replaceImage"></param>
        /// <param name="convertImgToSrc"></param>
        /// <returns></returns>
        private static string[] GetInnerTextElements(HtmlNode node, Uri baseUri, bool replaceImage = true, bool convertImgToSrc = false)
        {
            if (TAGS_TO_DROP.Contains(node.Name.ToLowerInvariant()))
            {
                return Array.Empty<string>();
            }
            else if (node.NodeType == HtmlNodeType.Text)
            {
                return new[] { Regex.Replace(node.InnerText, @"\s+", " ") };
            }
            else if(node.Name.ToLowerInvariant() == "p")
            {
                //Paragraphs should be separated by a newline
                return node.ChildNodes.SelectMany(n => GetInnerTextElements(n, baseUri, replaceImage, convertImgToSrc)).Concat(new[] { "\n" }).ToArray();
            }
            else if (node.Is("img") && replaceImage)
            {
                if(node.HasAttr("alt", ignoreEmpty: false))
                    return new[] { $" {node.GetAttributeValue("alt", null)} " };
                if(node.HasAttr("src", ignoreEmpty: false) && convertImgToSrc)
                    return new[] { $" {node.GetAttributeValue("src", null).ToAbsoluteUri(baseUri)} " };

            }
            else if (node.Is("br"))
            {
                return new[] { "\n" };
            }
            else
            {
                return node.ChildNodes.SelectMany(n => GetInnerTextElements(n, baseUri, replaceImage, convertImgToSrc)).ToArray();
            }

            return Array.Empty<string>();
        }
    }
}
