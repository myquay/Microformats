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

        private string REGEX_TIMEZONE = @"(([+-]\d{1,2}:\d{2})|([+-]\d)|([+-]\d{3,4}))$";
        private string REGEX_DATE = @"^[0-9]{4}";

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

            //Support implicit h-entry for h-feed elements
            foreach (var node in doc.DocumentNode.Descendants().Where(n => n.GetClasses().Contains(Specs.FEED)))
            {
                foreach (var entry in node.Descendants().Where(n => n.GetClasses().Contains(Specs.ENTRY) && !n.GetClasses().Contains(Props.ENTRY)))
                    entry.AddClass(Props.ENTRY);
            }



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

            //TODO: IMPROVE IMPLICT TIMEZONE/ DATE SEARCHING
            var context = new ParseContext();
            foreach (var property in properties)
            {
                var propertyValue = ParseChildrenForProperty(node, property, context);
                if (propertyValue != null && property.Type != MfType.DateTime) //Search for implicit timezones
                    resultSet.Properties.Add(property.Key, propertyValue);
            }
            foreach (var property in properties.Where(t => t.Type == MfType.DateTime))
            {
                var propertyValue = ParseChildrenForProperty(node, property, context); //Set with timezones
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

            foreach (var child in node.ChildNodes.Where(c => !c.IsMicoformatEntity()))
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

            var nodesForSearch = node.ChildNodes.Where(c => !c.IsMicoformatEntity()).ToList();

            foreach (var nodeToAdd in nodesForSearch)
            {
                nodesWithProperty.AddRange(GetChildPropertyNodes(nodeToAdd, property));
            }

            return nodesWithProperty.ToArray();
        }

        private MfValue[] ParseChildrenForProperty(HtmlNode node, MfProperty property, ParseContext context = null)
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
                        string parsedDate = null;

                        if (child.ChildNodes.Any(c => c.HasClass("value")))
                        {
                            var dateTimeParts = child.ChildNodes.Where(c => c.HasClass("value")).Select(s =>
                            {
                                if (s.Is("time", "ins", "del") && s.HasAttr("datetime"))
                                    return s.GetAttributeValue("datetime", null);
                                if (s.Is("img", "area") && s.HasAttr("alt"))
                                    return s.GetAttributeValue("alt", null);
                                if (s.Is("data", "input"))
                                    return s.GetAttributeValue("value", null) ?? s.InnerText.Trim();
                                if (s.Is("abbr"))
                                    return s.GetAttributeValue("title", null) ?? s.InnerText.Trim();
                                return s.InnerText.Trim();
                            }).Select(a => Regex.Replace(Regex.Replace(a, @"([+-])(\d{1,2}):(\d{1,2})", "$1$2$3"), @"^\d{4}-\d{3}", match =>
                            {
                                return new DateTime(int.Parse(match.Value.Substring(0, 4)), 1, 1).AddDays(int.Parse(match.Value.Substring(5)) - 1).ToString("yyyy-MM-dd");
                            })).Select(a => new
                            {
                                //Remove ':' from the timezone offset, normalise ordinal
                                Part = a,
                                IsDate = Regex.IsMatch(a,REGEX_DATE),
                                IsTimezone = Regex.IsMatch(a, REGEX_TIMEZONE)
                            });

                            parsedDate = $"{dateTimeParts.FirstOrDefault(a => a.IsDate)?.Part} {dateTimeParts.FirstOrDefault(a => !a.IsDate && !a.IsTimezone)?.Part}{dateTimeParts.FirstOrDefault(a => a.IsTimezone)?.Part}".Trim();

                            if(context != null)
                            {
                                context.ImpliedTimezone = context.ImpliedTimezone ?? dateTimeParts.FirstOrDefault(a => a.IsTimezone)?.Part;
                                context.ImpliedDate = context.ImpliedDate ?? dateTimeParts.FirstOrDefault(a => a.IsDate)?.Part;
                            }

                        }
                        else if (child.Is("time", "ins", "del") && child.HasAttr("datetime"))
                        {
                            parsedDate = child.GetAttributeValue("datetime", null);
                        }
                        else if (child.Is("img", "area") && child.HasAttr("alt"))
                        {
                            parsedDate = child.GetAttributeValue("alt", null);
                        }
                        else if (child.Is("abbr") && child.HasAttr("title"))
                        {
                            parsedDate = child.GetAttributeValue("title", null);
                        }
                        else if (child.Is("data", "input") && child.HasAttr("value"))
                        {
                            parsedDate = child.GetAttributeValue("value", null);
                        }
                        else
                        {
                            //TODO: dropping any nested <script> & <style> elements, replacing any nested <img> elements with their alt attribute, if present
                            parsedDate = Regex.Replace(child.InnerText.Trim(), @"\s+", " ");
                        }

                        if (parsedDate != null)
                        {
                            //Ensure the date part is present
                            if (!Regex.IsMatch(parsedDate, REGEX_DATE))
                                parsedDate = $"{context.ImpliedDate} {parsedDate}".Trim();

                            //Ensure the timezone part is present
                            if (!Regex.IsMatch(parsedDate, REGEX_TIMEZONE))
                                parsedDate = $"{parsedDate}{context.ImpliedTimezone}".Trim();

                            //Ensure standard timezone
                            parsedDate = Regex.Replace(parsedDate, @"(?<prefix>[+-])(?<offset>\d{3})$", delegate (Match m)
                            {
                                return $"{m.Groups["prefix"].Value}0{m.Groups["offset"].Value}";
                            });
                            parsedDate = Regex.Replace(parsedDate, @"(?<prefix>[+-])(?<offset>\d)$", delegate (Match m)
                            {
                                return $"{m.Groups["prefix"].Value}0{m.Groups["offset"].Value}00";
                            });

                            //Extract the am and pm parts
                            parsedDate = Regex.Replace(parsedDate, @"(?<hour>\d{1,2})(?<minute>:\d{2})?(?<second>:\d{2})?[ap]\.?m\.?", delegate (Match m) {
                                
                                var isAm = Regex.IsMatch(m.Value, @"[a]\.?m\.?", RegexOptions.IgnoreCase);
                                var hour = isAm ? $"{int.Parse(m.Groups["hour"].Value):D2}" :
                                 $"{(int.Parse(m.Groups["hour"].Value)+12):D2}";

                                return $"{hour}{m.Groups["minute"].Value??":00"}{m.Groups["second"].Value ?? ":00"}";
                            }, RegexOptions.IgnoreCase);

                            //Ensure standard time format
                            parsedDate = Regex.Replace(parsedDate, @"(?<hour>\s\d{1,2})(?<minute>:\d{2})?(?<second>:\d{2})?", delegate (Match m) {
                                return $"{m.Groups["hour"].Value}{(m.Groups["minute"].Success ? m.Groups["minute"].Value : ":00")}{(m.Groups["second"].Value)}";
                            });

                            //Set implied timezone if required
                            if (context.ImpliedTimezone == null && Regex.IsMatch(parsedDate, REGEX_TIMEZONE))
                                context.ImpliedTimezone = Regex.Match(parsedDate, REGEX_TIMEZONE).Value;

                            //Set implied timezone if required
                            if (context.ImpliedDate == null &&  Regex.IsMatch(parsedDate, @"^[0-9]{4}-[0-9]{2}-[0-9]{2}"))
                                context.ImpliedDate = Regex.Match(parsedDate, @"^[0-9]{4}-[0-9]{2}-[0-9]{2}").Value;

                            propertyValue.Add(new MfValue(parsedDate));
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

    /// <summary>
    /// Context of parse action
    /// </summary>
    internal class ParseContext
    {
        public string ImpliedDate { get; set; }

        public string ImpliedTimezone { get; set; }
    }

}