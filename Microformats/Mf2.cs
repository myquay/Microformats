using HtmlAgilityPack;
using Microformats.Definitions;
using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static Microformats.Definitions.Constants;

namespace Microformats
{
    public class Mf2
    {

        private Mf2Options options = new Mf2Options();

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

            var result = new MfResult()
            {
                Items = doc.DocumentNode.ChildNodes.SelectMany(m => SearchElementTreeForMicroformat(m)).Where(m => m != null).ToArray()
            };

            return result;
        }

        /// <summary>
        /// Parse element class for root class name(s) "h-*" and if none, backcompat root classes
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private MfSpec[] SearchElementTreeForMicroformat(HtmlNode node)
        {
            //if none found, parse child elements for microformats (depth first, doc order)
            if (!node.GetClasses().Any(n => MfProperty.IsOfType(n, MfType.Specification)))
            {
                return node.ChildNodes.SelectMany(n => SearchElementTreeForMicroformat(n)).ToArray();
            }
            else //else if found, start parsing a new microformat
            {
                return new[] { ParseElementForMicroformat(node) };
            }
        }

        /// <summary>
        /// Start parsing a new microformat
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private MfSpec ParseElementForMicroformat(HtmlNode node)
        {
            //Get the specs we are attemting to load
            var specs = node.GetClasses().Select(s => MfProperty.TryFromName(s, out MfProperty property) ? property : null)
                .Where(s => s != null)
                .Where(s => s.Type == MfType.Specification)
                .ToArray();

            var resultSet = new MfSpec
            {
                Type = specs.Select(c => c.Name).OrderBy(s => s).ToArray(),
                Id = !String.IsNullOrEmpty(node.Id) ? node.Id : null,
            };

            //Get all childnodes that are properties
            var properties = GetAllPropertiesForSpecification(node)
                .GroupBy(p => p.Name)
                .Select(g => g.First())
                .ToList();

            
            foreach (var property in properties)
            {
                var propertyValue = ParseChildrenForProperty(node, property);
                if (propertyValue != null)
                    resultSet.Properties.Add(property.Key, propertyValue);
            }

            return resultSet;
        }

        /// <summary>
        /// Get all the possbile properties for implemented specification
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        private MfProperty[] GetAllPropertiesForSpecification(HtmlNode node)
        {
            var possibleProperties = node.ChildNodes.SelectMany(c => c.GetClasses())
                .Where(n => MfProperty.TryFromName(n, out MfProperty result))
                .Select(n => MfProperty.TryFromName(n, out MfProperty result) ? result : null)
                .Where(s => s != null && s.Type != MfType.Specification)
                .ToList();

            foreach(var child in node.ChildNodes.Where(c => !c.GetClasses().Any(n => MfProperty.TryFromName(n, out MfProperty result)) && !c.IsMicoformatEntity()))
            {
                possibleProperties.AddRange(GetAllPropertiesForSpecification(child));
            }

            //Add default possible properties
            if (MfProperty.TryFromName(Props.NAME, out MfProperty nameResult))
                possibleProperties.Add(nameResult);
            if (MfProperty.TryFromName(Props.PHOTO, out MfProperty photoResult))
                possibleProperties.Add(photoResult);
            if (MfProperty.TryFromName(Props.URL, out MfProperty urlResult))
                possibleProperties.Add(urlResult);

            return possibleProperties.ToArray();
        }

        /// <summary>
        /// Get all child elements for property
        /// </summary>
        /// <param name="node"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private HtmlNode[] GetChildPropertyNodes(HtmlNode node, MfProperty property)
        {
            var nodesWithProperty = node.ChildNodes.Where(c => c.GetClasses().Any(a => MfProperty.TryFromName(a, out MfProperty parsedProperty) && parsedProperty.Name == property.Name)).ToList();

            var nodesForSearch = node.ChildNodes.Where(c => !c.GetClasses().Any(a => MfProperty.TryFromName(a, out MfProperty parsedProperty)) && !c.IsMicoformatEntity()).ToList();

            foreach(var nodeToAdd in nodesForSearch){
                nodesWithProperty.AddRange(GetChildPropertyNodes(nodeToAdd, property));
            }

            return nodesWithProperty.ToArray();
        }

        private MfValue[] ParseChildrenForProperty(HtmlNode node, MfProperty property)
        {
            var propertyValue = new List<MfValue>();

            //parse a child element for microformats (recurse)
            foreach (var child in GetChildPropertyNodes(node, property))
            {
                //if that child element itself has a microformat ("h-*" or backcompat roots) and is a property element, add it into the array of values for that property as a { } structure, add to that { } structure:
                if (child.GetClasses().Any(c => MfProperty.TryFromName(c, out MfProperty specProp) && specProp.Type == MfType.Specification))
                {
                    var value = ParseElementForMicroformat(child);
                    value.Value = value.Get(Props.NAME)?.First();
                    propertyValue.Add(new MfValue(value));
                }
                else
                {
                    if (property.Type == MfType.Property)
                    {
                        if (child.ChildNodes.Any(c => c.HasClass("value")))
                        {
                            propertyValue.Add(new MfValue(child.ChildNodes.Where(c => c.HasClass("value")).Select(s =>
                            {
                                if (s.Is("img", "area") && s.HasAttr("alt"))
                                    return s.GetAttributeValue("alt", null);
                                if (s.Is("data"))
                                    return s.GetAttributeValue("value", null) ?? s.InnerText.Trim();
                                if (s.Is("abbr"))
                                    return s.GetAttributeValue("title", null) ?? s.InnerText.Trim();
                                return s.InnerText.Trim();
                            }).Aggregate((current, next) => $"{current} {next}")));
                        }
                        else if (child.Is("abbr", "link") && child.HasAttr("title"))
                        {
                            propertyValue.Add(new MfValue(child.GetAttributeValue("title", null)));
                        }
                        else if (child.Is("data", "input") && child.HasAttr("value"))
                        {
                            propertyValue.Add(new MfValue(child.GetAttributeValue("value", null)));
                        }
                        else if (child.Is("img", "area") && child.HasAttr("alt"))
                        {
                            propertyValue.Add(new MfValue(child.GetAttributeValue("alt", null)));
                        }
                        else
                        {
                            //TODO: dropping any nested <script> & <style> elements, replacing any nested <img> elements with their alt attribute, if present
                            propertyValue.Add(new MfValue(Regex.Replace(child.InnerText.Trim(), @"\s+", " ")));
                        }
                    }
                    else if (property.Type == MfType.Url)
                    {
                        if (child.Is("a", "area", "link") && child.HasAttr("href"))
                        {
                            propertyValue.Add(new MfValue(child.GetAttributeValue("href", null)));
                        }
                        else if (child.Is("img") && child.HasAttr("src"))
                        {
                            if (child.HasAttr("alt"))
                            {
                                propertyValue.Add(new MfValue(new MfImage
                                {
                                    Value = child.GetAttributeValue("src", null),
                                    Alt = child.GetAttributeValue("alt", null)
                                }));
                            }
                            else
                            {
                                propertyValue.Add(new MfValue(child.GetAttributeValue("src", null)));
                            }
                        }
                        else if (child.Is("audio", "video", "source", "iframe") && child.HasAttr("src"))
                        {
                            propertyValue.Add(new MfValue(child.GetAttributeValue("src", null)));
                        }
                        else if (child.Is("video") && child.HasAttr("poster"))
                        {
                            propertyValue.Add(new MfValue(child.GetAttributeValue("poster", null)));
                        }
                        else if (child.Is("object") && child.HasAttr("data"))
                        {
                            propertyValue.Add(new MfValue(child.GetAttributeValue("data", null)));
                        }
                        else if (child.ChildNodes.Any(c => c.HasClass("value")))
                        {
                            propertyValue.Add(new MfValue(child.ChildNodes.Where(c => c.HasClass("value")).Select(s =>
                            {
                                if (s.Is("img", "area") && s.HasAttr("alt"))
                                    return s.GetAttributeValue("alt", null);
                                if (s.Is("data"))
                                    return s.GetAttributeValue("value", null) ?? s.InnerText.Trim();
                                if (s.Is("abbr"))
                                    return s.GetAttributeValue("title", null) ?? s.InnerText.Trim();
                                return s.InnerText.Trim();
                            }).Aggregate((current, next) => $"{current} {next}")));
                        }
                        else if (child.Is("abbr") && child.HasAttr("title"))
                        {
                            propertyValue.Add(new MfValue(child.GetAttributeValue("title", null)));
                        }
                        else if (child.Is("data", "input") && child.HasAttr("value"))
                        {
                            propertyValue.Add(new MfValue(child.GetAttributeValue("value", null)));
                        }
                        else
                        {
                            //TODO: dropping any nested <script> & <style> elements, replacing any nested <img> elements with their alt attribute, if present
                            propertyValue.Add(new MfValue(Regex.Replace(child.InnerText.Trim(), @"\s+", " ")));
                        }
                    }
                    else if (property.Type == MfType.DateTime)
                    {
                        if (child.ChildNodes.Any(c => c.HasClass("value")))
                        {
                            propertyValue.Add(new MfValue(child.ChildNodes.Where(c => c.HasClass("value")).Select(s =>
                            {
                                if (s.Is("img", "area") && s.HasAttr("alt"))
                                    return s.GetAttributeValue("alt", null);
                                if (s.Is("data"))
                                    return s.GetAttributeValue("value", null) ?? s.InnerText.Trim();
                                if (s.Is("abbr"))
                                    return s.GetAttributeValue("title", null) ?? s.InnerText.Trim();
                                return s.InnerText.Trim();
                            }).Aggregate((current, next) => $"{current} {next}")));
                        }
                        else if (child.Is("time", "ins", "del") && child.HasAttr("datetime"))
                        {
                            propertyValue.Add(new MfValue(child.GetAttributeValue("datetime", null)));
                        }
                        else if (child.Is("img", "area") && child.HasAttr("alt"))
                        {
                            propertyValue.Add(new MfValue(child.GetAttributeValue("alt", null)));
                        }
                        else if (child.Is("abbr") && child.HasAttr("title"))
                        {
                            propertyValue.Add(new MfValue(child.GetAttributeValue("title", null)));
                        }
                        else if (child.Is("data", "input") && child.HasAttr("value"))
                        {
                            propertyValue.Add(new MfValue(child.GetAttributeValue("value", null)));
                        }
                        else
                        {
                            //TODO: dropping any nested <script> & <style> elements, replacing any nested <img> elements with their alt attribute, if present
                            propertyValue.Add(new MfValue(Regex.Replace(child.InnerText.Trim(), @"\s+", " ")));
                        }
                    }
                    else if (property.Type == MfType.Embedded)
                    {
                        propertyValue.Add(new MfValue(new MfEmbedded
                        {
                            Value = child.InnerText.Trim(),
                            Html = child.InnerHtml.Trim()
                        }));
                    }
                    else
                    {
                        throw new ArgumentException($"Invlaid property type: {property.Type}");
                    }
                }
            }

            //Implicit parsing for special properties
            if (!propertyValue.Any())
            {
                if (property.Name == Props.NAME)
                {
                    if (node.Is("img", "area") && node.HasAttr("alt"))
                        return new[] { new MfValue(node.GetAttributeValue("alt", null)) };

                    if (node.Is("abbr") && node.HasAttr("title"))
                        return new[] { new MfValue(node.GetAttributeValue("title", null)) };

                    if (node.TrySelectSingleChild("img", out HtmlNode imgChild) && imgChild.HasAttr("alt", ignoreEmpty: true) && !imgChild.IsMicoformatEntity())
                        return new[] { new MfValue(imgChild.GetAttributeValue("alt", null)) };

                    if (node.TrySelectSingleChild("area", out HtmlNode areaChild) && areaChild.HasAttr("alt", ignoreEmpty: true) && !areaChild.IsMicoformatEntity())
                        return new[] { new MfValue(areaChild.GetAttributeValue("alt", null)) };

                    if (node.TrySelectSingleChild("abbr", out HtmlNode abbrChild) && abbrChild.HasAttr("title", ignoreEmpty: true) && !abbrChild.IsMicoformatEntity())
                        return new[] { new MfValue(abbrChild.GetAttributeValue("title", null)) };

                    if (node.TrySelectSingleChild(out HtmlNode anyChild) && !anyChild.IsMicoformatEntity())
                    {
                        if (anyChild.TrySelectSingleChild("img", out HtmlNode imgNestedChild) && imgNestedChild.HasAttr("alt", ignoreEmpty: true) && !imgNestedChild.IsMicoformatEntity())
                            return new[] { new MfValue(imgNestedChild.GetAttributeValue("alt", null)) };

                        if (anyChild.TrySelectSingleChild("area", out HtmlNode areaNestedChild) && areaNestedChild.HasAttr("alt", ignoreEmpty: true) && !areaNestedChild.IsMicoformatEntity())
                            return new[] { new MfValue(areaNestedChild.GetAttributeValue("alt", null)) };

                        if (anyChild.TrySelectSingleChild("abbr", out HtmlNode abbrNestedChild) && abbrNestedChild.HasAttr("title", ignoreEmpty: true) && !abbrNestedChild.IsMicoformatEntity())
                            return new[] { new MfValue(abbrNestedChild.GetAttributeValue("title", null)) };
                    }

                    //TODO: dropping any nested <script> & <style> elements, replacing any nested <img> elements with their alt attribute, if present
                    return new[] { new MfValue(Regex.Replace(node.InnerText.Trim(), @"\s+", " ")) };

                }
                else if (property.Name == Props.PHOTO)
                {

                    //TODO: if there is a gotten photo value, return the normalized absolute URL of it, following the containing document's language's rules for resolving relative URLs (e.g. in HTML, use the current URL context as determined by the page, and first <base> element, if any).

                    if (node.Is("img"))
                    {
                        if (node.HasAttr("alt"))
                        {
                            return new[] { new MfValue(new MfImage{
                                  Value = node.GetAttributeValue("src", null),
                                  Alt = node.GetAttributeValue("alt", null)
                            }) };
                        }
                        else
                        {
                            return new[] { new MfValue(node.GetAttributeValue("src", null)) };
                        }
                    }

                    if (node.Is("object") && node.HasAttr("data"))
                        return new[] { new MfValue(node.GetAttributeValue("data", null)) };

                    if (node.TrySelectFirstChild("img", out HtmlNode imgChild, onlyOfType: true) && !imgChild.IsMicoformatEntity())
                    {
                        if (imgChild.HasAttr("alt"))
                        {
                            return new[] { new MfValue(new MfImage{
                                  Value = imgChild.GetAttributeValue("src", null),
                                  Alt = imgChild.GetAttributeValue("alt", null)
                            }) };
                        }
                        else
                        {
                            return new[] { new MfValue(imgChild.GetAttributeValue("src", null)) };
                        }
                    }

                    if (node.TrySelectFirstChild("object", out HtmlNode objChild, onlyOfType: true) && objChild.HasAttr("data") && !objChild.IsMicoformatEntity())
                        return new[] { new MfValue(objChild.GetAttributeValue("data", null)) };

                    if (node.TrySelectSingleChild(out HtmlNode anyChild) && !anyChild.IsMicoformatEntity())
                    {

                        if (anyChild.TrySelectFirstChild("img", out HtmlNode nestedImgChild, onlyOfType: true) && !nestedImgChild.IsMicoformatEntity())
                        {
                            if (nestedImgChild.HasAttr("alt"))
                            {
                                return new[] { new MfValue(new MfImage{
                                  Value = nestedImgChild.GetAttributeValue("src", null),
                                  Alt = nestedImgChild.GetAttributeValue("alt", null)
                            }) };
                            }
                            else
                            {
                                return new[] { new MfValue(nestedImgChild.GetAttributeValue("src", null)) };
                            }
                        }

                        if (anyChild.TrySelectFirstChild("object", out HtmlNode objNestedChild, onlyOfType: true) && objNestedChild.HasAttr("data") && !objNestedChild.IsMicoformatEntity())
                            return new[] { new MfValue(objNestedChild.GetAttributeValue("data", null)) };

                    }

                    return null;
                }
                else if (property.Name == Props.URL)
                {

                    //TODO: if there is a gotten url value, return the normalized absolute URL of it, following the containing document's language's rules for resolving relative URLs (e.g. in HTML, use the current URL context as determined by the page, and first <base> element, if any).

                    if (node.Is("a", "area") && node.HasAttr("href"))
                        return new[] { new MfValue(node.GetAttributeValue("href", null)) };

                    if (node.TrySelectFirstChild("a", out HtmlNode aChild, onlyOfType: true) && aChild.HasAttr("href") && !aChild.IsMicoformatEntity())
                        return new[] { new MfValue(aChild.GetAttributeValue("href", null)) };

                    if (node.TrySelectFirstChild("area", out HtmlNode areaChild, onlyOfType: true) && areaChild.HasAttr("href") && !areaChild.IsMicoformatEntity())
                        return new[] { new MfValue(aChild.GetAttributeValue("href", null)) };

                    if (node.TrySelectSingleChild(out HtmlNode anyChild) && !anyChild.IsMicoformatEntity())
                    {

                        if (anyChild.TrySelectFirstChild("a", out HtmlNode aNestedChild, onlyOfType: true) && aNestedChild.HasAttr("href") && !aNestedChild.IsMicoformatEntity())
                            return new[] { new MfValue(aNestedChild.GetAttributeValue("href", null)) };

                        if (anyChild.TrySelectFirstChild("area", out HtmlNode areaNestedChild, onlyOfType: true) && areaNestedChild.HasAttr("href") && !areaNestedChild.IsMicoformatEntity())
                            return new[] { new MfValue(areaNestedChild.GetAttributeValue("href", null)) };

                    }

                    return null;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return propertyValue.ToArray();
            }
        }
    }
}
