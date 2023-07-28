using HtmlAgilityPack;
using Microformats.Definitions;
using Microformats.Definitions.Properties;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Microformats
{
    public class Mf2
    {

        /// <summary>
        /// Create a new parser
        /// </summary>
        public Mf2()
        {
            Load(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Supported vocabularies
        /// </summary>
        private List<IVocabulary> Vocabularies { get; set; } = new List<IVocabulary>();

        /// <summary>
        /// Add a new vocabulary
        /// </summary>
        /// <param name="vocabulary"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddVocabulary(IVocabulary vocabulary)
        {
            if (Vocabularies.Any(v => v.Name == vocabulary.Name))
                throw new ArgumentException($"The vocabulary '{vocabulary.Name}' has already been added");
            Vocabularies.Add(vocabulary);
        }

        /// <summary>
        /// Load all vocabularies from assembly
        /// </summary>
        /// <param name="assembly"></param>
        public void Load(Assembly assembly)
        {
            foreach (var vocabulary in assembly.GetTypes().Where(t => typeof(IVocabulary).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract))
            {
                var newVocab = (IVocabulary)Activator.CreateInstance(vocabulary);
                if (!Vocabularies.Any(v => v.Name == vocabulary.Name))
                    Vocabularies.Add(newVocab);
            }
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
        private MfType[] SearchElementTreeForMicroformat(HtmlNode node)
        {
            //if none found, parse child elements for microformats (depth first, doc order)
            if (!Vocabularies.Any(v => node.GetClasses().Contains(v.Name)))
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
        private MfType ParseElementForMicroformat(HtmlNode node)
        {
            //keep track of whether the root class name(s) was from backcompat
            var vocab = Vocabularies.Where(v => node.GetClasses().Contains(v.Name));
            if (vocab.Any(c => c.Version == MicroformatsVersion.Two))
                vocab = vocab.Where(c => c.Version == MicroformatsVersion.Two);

            if (!vocab.Any())
                throw new ArgumentException("No micoformat found on supplied node");

            var resultSet = new MfType
            {
                Type = vocab.Select(c => c.Name).OrderBy(s => s).ToArray(),
                Id = !String.IsNullOrEmpty(node.Id) ? node.Id : null,
            };

            var properties = vocab.SelectMany(c => c.Properties).GroupBy(p => p.Name)
             .Select(g => g.First())
             .ToList();

            foreach (var property in properties)
            {
                switch (property.Type)
                {
                    case MType.Property:
                        var propertyItem = ParseChildrenForProperty(node, property);
                        if (propertyItem != null)
                            resultSet.Properties.Add(property.Key, propertyItem);
                        break;
                    case MType.Url:
                        //if such class(es) are found, it is a property element
                        var urlItem = ParseChildrenForProperty(node, property);
                        //add properties found to current microformat's properties: { } structure
                        if (urlItem != null)
                            resultSet.Properties.Add(property.Key, urlItem);
                        break;
                    default:
                        break;
                }
            }

            return resultSet;
        }

        private MfValue[] ParseChildrenForProperty(HtmlNode node, IProperty property)
        {
            var propertyValue = new List<MfValue>();

            //parse a child element for microformats (recurse)
            foreach (var child in node.ChildNodes.Where(c => c.GetClasses().Contains(property.Name)))
            {
                //if that child element itself has a microformat ("h-*" or backcompat roots) and is a property element, add it into the array of values for that property as a { } structure, add to that { } structure:
                if (Vocabularies.Any(v => child.GetClasses().Contains(v.Name)))
                {
                    var value = ParseElementForMicroformat(child);
                    value.Value = value.GetProperty(property)?.First();
                    propertyValue.Add(new MfValue(value));
                }
                else
                {
                    //TODO: ADD PROPER PARSING
                    propertyValue.Add(new MfValue(node.GetDirectInnerText()));
                }
            }

            //Implicit parsing for special properties
            if (!propertyValue.Any())
            {
                if (property is PName)
                {
                    if (node.Is("img", "area") && node.HasAttr("alt"))
                        return new[] { new MfValue(node.GetAttributeValue("alt", null)) };

                    if (node.Is("abbr") && node.HasAttr("title"))
                        return new[] { new MfValue(node.GetAttributeValue("title", null)) };

                    if (node.TrySelectSingleChild("img", out HtmlNode imgChild) && imgChild.HasAttr("alt") && !imgChild.IsMicoformatEntity())
                        return new[] { new MfValue(imgChild.GetAttributeValue("alt", null)) };

                    if (node.TrySelectSingleChild("area", out HtmlNode areaChild) && areaChild.HasAttr("alt") && !areaChild.IsMicoformatEntity())
                        return new[] { new MfValue(areaChild.GetAttributeValue("alt", null)) };

                    if (node.TrySelectSingleChild("abbr", out HtmlNode abbrChild) && abbrChild.HasAttr("title") && !abbrChild.IsMicoformatEntity())
                        return new[] { new MfValue(abbrChild.GetAttributeValue("title", null)) };

                    if (node.TrySelectSingleChild(out HtmlNode anyChild) && !anyChild.IsMicoformatEntity())
                    {
                        if (anyChild.TrySelectSingleChild("img", out HtmlNode imgNestedChild) && imgNestedChild.HasAttr("alt") && !imgNestedChild.IsMicoformatEntity())
                            return new[] { new MfValue(imgNestedChild.GetAttributeValue("alt", null)) };

                        if (anyChild.TrySelectSingleChild("area", out HtmlNode areaNestedChild) && areaNestedChild.HasAttr("alt") && !areaNestedChild.IsMicoformatEntity())
                            return new[] { new MfValue(areaNestedChild.GetAttributeValue("alt", null)) };

                        if (anyChild.TrySelectSingleChild("abbr", out HtmlNode abbrNestedChild) && abbrNestedChild.HasAttr("title") && !abbrNestedChild.IsMicoformatEntity())
                            return new[] { new MfValue(abbrNestedChild.GetAttributeValue("title", null)) };
                    }

                    //TODO: dropping any nested <script> & <style> elements, replacing any nested <img> elements with their alt attribute, if present
                    return new[] { new MfValue(node.InnerText) };

                }
                else if (property is UPhoto)
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
                else if (property is UUrl)
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
