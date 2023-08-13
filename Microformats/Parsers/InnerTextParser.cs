using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
        public static string GetInnerText(HtmlNode node, bool replaceImage = true, bool convertImgToSrc = false)
        {

            var elements = GetInnerTextElements(node, replaceImage, convertImgToSrc);
            ////Drop scrit tags
            //doc.DocumentNode.Descendants()
            //    .Where(n => n.Name == "script" || n.Name == "style")
            //    .ToList()
            //    .ForEach(n => n.Remove());

            //ReplaceImgNodesWithText(doc.DocumentNode, isImplied);

            //doc.DocumentNode.Descendants()
            //  .Where(n => n.Name == "img")
            //  .ToList()
            //  .ForEach(n => n.Remove());

            //foreach (var textNode in doc.DocumentNode.Descendants().Where(t => t.NodeType == HtmlNodeType.Text))
            //    textNode.InnerHtml = Regex.Replace(textNode.InnerText, @"\s+", " ");

            //foreach (var brNode in doc.DocumentNode.Descendants().Where(n => n.Name == "br").ToList())
            //{
            //    brNode.ParentNode.ReplaceChild(doc.CreateTextNode("\n"), brNode);
            //}

            return Regex.Replace(String.Join("", elements), @"[^\S\r\n]+", " ").Trim();
        }

        /// <summary>
        /// Get text elements from node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="replaceImage"></param>
        /// <param name="convertImgToSrc"></param>
        /// <returns></returns>
        private static string[] GetInnerTextElements(HtmlNode node, bool replaceImage = true, bool convertImgToSrc = false)
        {
            if (TAGS_TO_DROP.Contains(node.Name.ToLowerInvariant()))
            {
                return Array.Empty<string>();
            }
            else if (node.NodeType == HtmlNodeType.Text)
            {
                return new[] { Regex.Replace(node.InnerText, @"\s+", " ") };
            }
            else if (node.Is("img") && replaceImage)
            {
                if(node.HasAttr("alt", ignoreEmpty: false))
                    return new[] { $" {node.GetAttributeValue("alt", null)} " };
                if(node.HasAttr("src", ignoreEmpty: false) && convertImgToSrc)
                    return new[] { $" {node.GetAttributeValue("src", null)} " };
                //TODO: MAKE URL ABSOLUTE IF NEEDED
            }
            else if (node.Is("br"))
            {
                return new[] { "\n" };
            }
            else
            {
                return node.ChildNodes.SelectMany(n => GetInnerTextElements(n, replaceImage, convertImgToSrc)).ToArray();
            }

            return Array.Empty<string>();
        }
    }
}
