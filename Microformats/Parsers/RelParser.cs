using HtmlAgilityPack;
using Microformats.Grammar;
using Microformats.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Linq;

namespace Microformats.Parsers
{
    /// <summary>
    /// Inner text parser
    /// </summary>
    internal class RelParser
    {

        /// <summary>
        /// Rels for document
        /// </summary>
        /// <param name="node"></param>
        /// <param name="replaceImage"></param>
        /// <param name="convertImgToSrc"></param>
        /// <returns></returns>
        public static Dictionary<string, string[]> ParseRels(HtmlNode node, Uri baseUri)
        {
            //Get all rels
            return node.DescendantsAndSelf().Where(a => a.Is("a", "link", "area") && a.HasAttr("rel") && a.HasAttr("href"))
                .Select(a => new
                {

                    Rels = a.GetAttributeValue("rel", null).Split(new char[] { ' ', '\t', '\n', '\f', '\r' }, StringSplitOptions.RemoveEmptyEntries).Distinct().OrderBy(b => b),
                    Href = a.GetAttributeValue("href", null)
                })
                .SelectMany(r => r.Rels.Select(a => new { Rel = a, Href = r.Href }))
                .GroupBy(r => r.Rel)
                .ToDictionary(r => r.Key, r => r.Select(a => a.Href).Distinct().ToArray());
        }

        /// <summary>
        /// Get Rel URLs for document
        /// </summary>
        /// <param name="node"></param>
        /// <param name="replaceImage"></param>
        /// <param name="convertImgToSrc"></param>
        /// <returns></returns>
        public static Dictionary<string, MfRelUrlResult> ParseRelUrls(HtmlNode node, Uri baseUri)
        {
            //Get all rels
            return node.DescendantsAndSelf().Where(a => a.Is("a", "link", "area") && a.HasAttr("rel") && a.HasAttr("href"))
                .Select(a => new
                {
                    Result = new MfRelUrlResult
                    {
                        Rels = a.GetAttributeValue("rel", null).Split(new char[] { ' ', '\t', '\n', '\f', '\r' }, StringSplitOptions.RemoveEmptyEntries).Distinct().OrderBy(b => b).ToArray(),
                        HrefLang = a.GetAttributeValue("hreflang", null),
                        Media = a.GetAttributeValue("media", null),
                        Title = a.GetAttributeValue("title", null),
                        Type = a.GetAttributeValue("type", null),
                        Text = string.IsNullOrEmpty(InnerTextParser.GetInnerText(a, baseUri, false, false)) ? null : InnerTextParser.GetInnerText(a, baseUri, false, false),
                    },
                    Href = a.GetAttributeValue("href", null)
                })
                .GroupBy(r => r.Href)
                .ToDictionary(r => r.Key, r => r.Select(a => a.Result).Aggregate((a, b) =>
                {
                    return new MfRelUrlResult
                    {
                        Rels = a.Rels.Union(b.Rels).Distinct().OrderBy(c => c).ToArray(),
                        HrefLang = a.HrefLang ?? b.HrefLang,
                        Media = a.Media ?? b.Media,
                        Title = a.Title ?? b.Title,
                        Type = a.Type ?? b.Type,
                        Text = a.Text ?? b.Text
                    };
                }));
        }

    }
}