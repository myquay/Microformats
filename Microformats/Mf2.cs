using HtmlAgilityPack;
using Microformats.Definitions;
using Microformats.Grammar;
using Microformats.Parsers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using static Microformats.Definitions.Constants;

namespace Microformats
{
    /// <summary>
    /// Microformats2 parser
    /// </summary>
    public class Mf2
    {

        private Mf2Options options = new Mf2Options();
        private DateTimeParseContext dateParseContext;

        /// <summary>
        /// Set options for the parser
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public Mf2 WithOptions(Func<Mf2Options, Mf2Options> config = null)
        {
            if (config != null)
                options = config(options);
            return this;
        }

        /// <summary>
        /// Parse a document according to the Microformats2 specification.
        /// </summary>
        /// <remarks>
        /// <see href="https://microformats.org/wiki/microformats2-parsing#algorithm">https://microformats.org/wiki/microformats2-parsing#algorithm</see>
        /// </remarks>
        /// <param name="html"></param>
        /// <returns></returns>
        public MfResult Parse(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            //Upgrade classic microformats to v2
            if (options.UpgradeClassicMicroformats)
            {
                foreach (var classicSpec in ClassicMappings.Mapping)
                {
                    foreach (var node in doc.DocumentNode.Descendants()
                        .Where(n => n.GetClasses().Contains(classicSpec.ClassicType))
                        .Where(n => !n.GetClasses().Contains(classicSpec.Type)))
                    {
                        node.AddClass(classicSpec.Type);
                        node.SetBackcompat(true);

                        foreach (var childNode in node.Descendants())
                        {
                            foreach (var property in classicSpec.Properties
                                .Where(p => childNode.GetClasses().Contains(p.classic))
                                .Where(p => !childNode.GetClasses().Contains(p.modern)).Select(p => p.modern))
                            {
                                childNode.AddClass(property);
                            }
                        }
                    }
                }
            }

            //Support implicit h-entry for h-feed elements
            foreach (var node in doc.DocumentNode.Descendants().Where(n => n.GetClasses().Contains(Specs.FEED)))
            {
                foreach (var entry in node.Descendants().Where(n => n.GetClasses().Contains(Specs.ENTRY) && !n.GetClasses().Contains(Props.ENTRY)))
                    entry.AddClass(Props.ENTRY);
            }

            //Configure base url
            var baseUri = options.BaseUri;
            var baseElement = doc.DocumentNode.Descendants().Where(n => n.Name.ToLower() == "base" && n.HasAttr("href", ignoreEmpty: true)).FirstOrDefault();
            if (baseElement != null)
                baseUri = new Uri(baseElement.GetAttributeValue("href", null).ToAbsoluteUri(options.BaseUri));
                

            var context = new MfResult
            {
                Items = ParseElementForMicroformat(doc.DocumentNode, baseUri)
            };

            return context;
        }

        /// <summary>
        /// Parse a node for microformats, include children (depth first, doc order)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="isBackcompat"></param>
        /// <returns></returns>
        private MfSpec[] ParseElementForMicroformat(HtmlNode node, Uri baseUri, bool isBackcompat = false)
        {
            isBackcompat = isBackcompat || node.IsBackcompat();

            //Get the specs we are attemting to parse
            var specifications = node.GetClasses().Select(c => MfProperty.TryFromName(c, out MfProperty property) ? property : null)
                .Where(c => c != null)
                .Where(c => c.Type == MfType.Specification)
                .ToArray();

            if (specifications.Any())
            {
                //If microformat root, parse it for a microformat
                return new[] { ParseElementForMicroformat(node, specifications, baseUri, isBackcompat: isBackcompat) };
            }
            else
            {
                //If no specifications, parse child elements for microformats (depth first, doc order)
                return node.ChildNodes.SelectMany(n => ParseElementForMicroformat(n, baseUri, isBackcompat: isBackcompat)).ToArray();
            }
        }

        /// <summary>
        /// Parse a particular microformat for an element
        /// </summary>
        /// <param name="node"></param>
        /// <param name="specification"></param>
        /// <param name="isBackcompat"></param>
        /// <returns></returns>
        private MfSpec ParseElementForMicroformat(HtmlNode node, MfProperty[] specifications, Uri baseUri, bool isBackcompat = false)
        {
            var spec = new MfSpec
            {
                Type = specifications.Select(s => s.Name).ToArray(),
                Id = !String.IsNullOrEmpty(node.Id) ? node.Id : null,
                Lang = options.DiscoverLang ? DiscoverLanguage(node) : null,
                Properties = node.ChildNodes.SelectMany(n => ParseElementForMicroformatProperties(n, baseUri, isBackcompat))
                    .GroupBy(p => p.Property.Key)
                    .ToDictionary(g => g.Key, a => a.Select(s => s).ToArray())
            };

            //TODO: SET VALUE PROPERTY
            //Set the spec value property if applicable
            //if (valueProp != null)
            //    spec.Value = spec.Get(valueProp.Name).FirstOrDefault();

            //Parse the implied properties if applicable
            if (!isBackcompat)
            {
                if (!spec.Properties.ContainsKey("name") && !spec.HasParsedType(MfType.Specification, MfType.Property, MfType.Embedded) && ImpliedPropertyParser.TryParseName(node, baseUri, out MfValue name))
                    spec.Properties.Add("name", new[] { name });

                if (!spec.HasParsedType(MfType.Specification, MfType.Url))
                {
                    if (!spec.Properties.ContainsKey("photo") && ImpliedPropertyParser.TryParsePhoto(node, baseUri, out MfValue photo))
                        spec.Properties.Add("photo", new[] { photo });

                    if (!spec.Properties.ContainsKey("url") && ImpliedPropertyParser.TryParseUrl(node, baseUri, out MfValue url))
                        spec.Properties.Add("url", new[] { url });
                }
            }

            //Support area attributes
            if (node.Is("area") && node.HasAttr("shape"))
                spec.Shape = node.GetAttributeValue("shape", null);

            if (node.Is("area") && node.HasAttr("coords"))
                spec.Shape = node.GetAttributeValue("coords", null);

            return spec;
        }

        private MfValue[] ParseElementForMicroformatProperties(HtmlNode node, Uri baseUri, bool isBackcompat)
        {
            isBackcompat = isBackcompat || node.IsBackcompat();

            var propertiesOnNode = node.GetClasses()
                .Where(n => MfProperty.TryFromName(n, out MfProperty result))
                .Select(n => MfProperty.TryFromName(n, out MfProperty result) ? result : null)
                .Where(s => s != null && s.Type != MfType.Specification)
                .ToList();

            var specifications = node.GetClasses()
                .Where(n => MfProperty.TryFromName(n, out MfProperty result))
                .Select(n => MfProperty.TryFromName(n, out MfProperty result) ? result : null)
                .Where(s => s != null && s.Type == MfType.Specification)
                .ToArray();

            var results = new List<MfValue>();

            //Parse properties on the node
            if(propertiesOnNode.Any(p => p.Type == MfType.Property))
            {
                var propertyValue = PropertyParser.ParseText(node, baseUri);
                foreach(var property in propertiesOnNode.Where(p => p.Type == MfType.Property))
                {
                    if (specifications.Any())
                    {
                        results.Add(new MfValue(property.Name, ParseElementForMicroformat(node, specifications, baseUri, isBackcompat: isBackcompat)));
                    }
                    else
                    {
                        results.Add(new MfValue(property.Name, propertyValue));
                    }
                }   
            }

            //Parse URLs on the node
            if(propertiesOnNode.Any(p => p.Type == MfType.Url))
            {
                var propertyValue = PropertyParser.ParseUrl(node, baseUri);
                foreach (var property in propertiesOnNode.Where(p => p.Type == MfType.Url))
                {
                    if (specifications.Any())
                    {
                        results.Add(new MfValue(property.Name, ParseElementForMicroformat(node, specifications, baseUri, isBackcompat: isBackcompat)));
                    }
                    else
                    {
                        results.Add(new MfValue(property.Name, propertyValue));
                    }
                }
            }

            //Parse dates on the node
            if(propertiesOnNode.Any(p => p.Type == MfType.DateTime))
            {
                var (propertyValue, newDateParseContext) = PropertyParser.ParseDate(node, baseUri, this.dateParseContext);
                dateParseContext = newDateParseContext ?? dateParseContext;

                foreach (var property in propertiesOnNode.Where(p => p.Type == MfType.DateTime))
                {
                    if (specifications.Any())
                    {
                        results.Add(new MfValue(property.Name, ParseElementForMicroformat(node, specifications, baseUri, isBackcompat: isBackcompat)));
                    }
                    else
                    {
                        results.Add(new MfValue(property.Name, propertyValue));
                    }
                }
            }

            //Parse embedded microformats on the node
            if (propertiesOnNode.Any(p => p.Type == MfType.Embedded))
            {
                var propertyValue = PropertyParser.ParseEmbedded(node);
                foreach (var property in propertiesOnNode.Where(p => p.Type == MfType.Embedded))
                {
                    if (specifications.Any())
                    {
                        results.Add(new MfValue(property.Name, ParseElementForMicroformat(node, specifications, baseUri, isBackcompat: isBackcompat)));
                    }
                    else
                    {
                        results.Add(new MfValue(property.Name, propertyValue));
                    }
                }
            }

            //TODO: PARSE h-* elements not attached to a property (spec.hildren support)

            //Recursive parse child properties
            if (!specifications.Any())
                results.AddRange(node.ChildNodes.SelectMany(n => ParseElementForMicroformatProperties(n, baseUri, isBackcompat)));

            return results.ToArray();
        }

        /// <summary>
        /// Discover language of node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private string DiscoverLanguage(HtmlNode node)
        {
            if (node.HasAttr("lang"))
                return node.GetAttributeValue("lang", null);

            if (node.Name == "html")
            {
                if (node.SelectNodes(".//meta[@http-equiv]") != null)
                {
                    var language = node.SelectNodes(".//meta[@http-equiv]")
                        .Where(n => n.HasAttr("http-equiv") && n.HasAttr("content") && n.GetAttributeValue("http-equiv", null).ToLower() == "content-language")
                        .Select(n => n.GetAttributeValue("content", null)?.Trim())
                        .FirstOrDefault();
                    if (language != null)
                        return language;
                }
            }

            if (node.ParentNode != null)
                return DiscoverLanguage(node.ParentNode);
            return null;
        }
    }

    /// <summary>
    /// Context of parse action
    /// </summary>
    internal class DateTimeParseContext
    {
        public string ImpliedDate { get; set; }

        public string ImpliedTimezone { get; set; }
    }
}