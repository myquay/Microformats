using HtmlAgilityPack;
using Microformats.Definitions;
using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using static Microformats.Definitions.Constants;

namespace Microformats.Parsers
{
    /// <summary>
    /// Parse implied properties
    /// </summary>
    internal class ImpliedPropertyParser
    {
        /// <summary>
        /// Parse implied name property
        /// </summary>
        /// <param name="node">Node to parse</param>
        /// <param name="value">Parsed value</param>
        /// <returns>Whether the parse was successful</returns>
        internal static bool TryParseName(HtmlNode node, Uri baseUri, out MfValue value)
        {
            if (node.Is("img", "area") && node.HasAttr("alt"))
            {
                value = new MfValue(Props.NAME, node.GetAttributeValue("alt", null));
                return true;
            }

            if (node.Is("abbr") && node.HasAttr("title"))
            {
                value = new MfValue(Props.NAME, node.GetAttributeValue("title", null));
                return true;
            }
            if (node.TrySelectSingleChild("img", out HtmlNode imgChild) && imgChild.HasAttr("alt", ignoreEmpty: true) && !imgChild.IsMicroformatRoot())
            {
                value = new MfValue(Props.NAME, imgChild.GetAttributeValue("alt", null));
                return true;
            }

            if (node.TrySelectSingleChild("area", out HtmlNode areaChild) && areaChild.HasAttr("alt", ignoreEmpty: true) && !areaChild.IsMicroformatRoot())
            {
                value = new MfValue(Props.NAME, areaChild.GetAttributeValue("alt", null));
                return true;
            }

            if (node.TrySelectSingleChild("abbr", out HtmlNode abbrChild) && abbrChild.HasAttr("title", ignoreEmpty: true) && !abbrChild.IsMicroformatRoot())
            {
                value = new MfValue(Props.NAME, abbrChild.GetAttributeValue("title", null));
                return true;
            }

            if (node.TrySelectSingleChild(out HtmlNode anyChild) && !anyChild.IsMicroformatRoot())
            {
                if (anyChild.TrySelectSingleChild("img", out HtmlNode imgNestedChild) && imgNestedChild.HasAttr("alt", ignoreEmpty: true) && !imgNestedChild.IsMicroformatRoot())
                {
                    value = new MfValue(Props.NAME, imgNestedChild.GetAttributeValue("alt", null));
                    return true;
                }

                if (anyChild.TrySelectSingleChild("area", out HtmlNode areaNestedChild) && areaNestedChild.HasAttr("alt", ignoreEmpty: true) && !areaNestedChild.IsMicroformatRoot())
                {
                    value = new MfValue(Props.NAME, areaNestedChild.GetAttributeValue("alt", null));
                    return true;
                }

                if (anyChild.TrySelectSingleChild("abbr", out HtmlNode abbrNestedChild) && abbrNestedChild.HasAttr("title", ignoreEmpty: true) && !abbrNestedChild.IsMicroformatRoot())
                {
                    value = new MfValue(Props.NAME, abbrNestedChild.GetAttributeValue("title", null));
                    return true;
                }
            }

            value = new MfValue(Props.NAME, InnerTextParser.GetInnerText(node, baseUri, replaceImage: true, convertImgToSrc: false));
            return true;
        }

        /// <summary>
        /// Parse implied photo property
        /// </summary>
        /// <param name="node">Node to parse</param>
        /// <param name="value">Parsed value</param>
        /// <returns>Whether the parse was successful</returns>
        internal static bool TryParsePhoto(HtmlNode node, Uri baseUri, out MfValue value)
        {
            if (node.Is("img"))
            {
                if (node.HasAttr("alt"))
                {
                    value = new MfValue(Props.PHOTO, new MfImage
                    {
                        Value = node.GetAttributeValue("src", null).ToAbsoluteUri(baseUri),
                        Alt = node.GetAttributeValue("alt", null)
                    });
                    return true;
                }
                else
                {
                    value = new MfValue(Props.PHOTO, node.GetAttributeValue("src", null).ToAbsoluteUri(baseUri));
                    return true;
                }
            }

            if (node.Is("object") && node.HasAttr("data"))
            {
                value = new MfValue(Props.PHOTO, node.GetAttributeValue("data", null).ToAbsoluteUri(baseUri));
                return true;
            }

            //No implicit if there are any nested microformats
            if (!node.Descendants().Any(d => d.IsMicroformatRoot()))
            {

                if (node.TrySelectFirstChild("img", out HtmlNode imgChild, onlyOfType: true) && !imgChild.IsPropertyElement(MfType.Specification, MfType.Url))
                {
                    if (imgChild.HasAttr("alt"))
                    {
                        value = new MfValue(Props.PHOTO, new MfImage
                        {
                            Value = imgChild.GetAttributeValue("src", null).ToAbsoluteUri(baseUri),
                            Alt = imgChild.GetAttributeValue("alt", null)
                        });
                        return true;
                    }
                    else
                    {
                        value = new MfValue(Props.PHOTO, imgChild.GetAttributeValue("src", null).ToAbsoluteUri(baseUri));
                        return true;
                    }
                }

                if (node.TrySelectFirstChild("object", out HtmlNode objChild, onlyOfType: true) && objChild.HasAttr("data") && !objChild.IsPropertyElement(MfType.Specification, MfType.Url))
                {
                    value = new MfValue(Props.PHOTO, objChild.GetAttributeValue("data", null).ToAbsoluteUri(baseUri));
                    return true;
                }

                if (node.TrySelectSingleChild(out HtmlNode anyChild) && !anyChild.IsMicroformatRoot())
                {

                    if (anyChild.TrySelectFirstChild("img", out HtmlNode nestedImgChild, onlyOfType: true) && !nestedImgChild.IsPropertyElement(MfType.Specification, MfType.Url))
                    {
                        if (nestedImgChild.HasAttr("alt"))
                        {
                            value = new MfValue(Props.PHOTO, new MfImage
                            {
                                Value = nestedImgChild.GetAttributeValue("src", null).ToAbsoluteUri(baseUri),
                                Alt = nestedImgChild.GetAttributeValue("alt", null)
                            });
                            return true;
                        }
                        else
                        {
                            value = new MfValue(Props.PHOTO, nestedImgChild.GetAttributeValue("src", null).ToAbsoluteUri(baseUri));
                            return true;
                        }
                    }

                    if (anyChild.TrySelectFirstChild("object", out HtmlNode objNestedChild, onlyOfType: true) && objNestedChild.HasAttr("data") && !objNestedChild.IsPropertyElement(MfType.Specification, MfType.Url))
                    {
                        value = new MfValue(Props.PHOTO, objNestedChild.GetAttributeValue("data", null).ToAbsoluteUri(baseUri));
                        return true;
                    }
                }
            }

            value = null;
            return false;
        }

        /// <summary>
        /// Parse implied URL property
        /// </summary>
        /// <param name="node">Node to parse</param>
        /// <param name="value">Parsed value</param>
        /// <returns>Whether the parse was successful</returns>
        internal static bool TryParseUrl(HtmlNode node, Uri baseUri, out MfValue value)
        {
            if (node.Is("a", "area") && node.HasAttr("href"))
            {
                value = new MfValue(Props.URL, node.GetAttributeValue("href", null).ToAbsoluteUri(baseUri));
                return true;
            }

            if (node.TrySelectFirstChild("a", out HtmlNode aChild, onlyOfType: true) && aChild.HasAttr("href") && !aChild.IsPropertyElement(MfType.Specification, MfType.Url))
            {
                value = new MfValue(Props.URL, aChild.GetAttributeValue("href", null).ToAbsoluteUri(baseUri));
                return true;
            }

            if (node.TrySelectFirstChild("area", out HtmlNode areaChild, onlyOfType: true) && areaChild.HasAttr("href") && !areaChild.IsPropertyElement(MfType.Specification, MfType.Url))
            {
                value = new MfValue(Props.URL, areaChild.GetAttributeValue("href", null).ToAbsoluteUri(baseUri));
                return true;
            }

            if (node.TrySelectSingleChild(out HtmlNode anyChild) && !anyChild.IsMicroformatRoot())
            {

                if (anyChild.TrySelectFirstChild("a", out HtmlNode aNestedChild, onlyOfType: true) && aNestedChild.HasAttr("href") && !aNestedChild.IsPropertyElement(MfType.Specification, MfType.Url))
                {
                    value = new MfValue(Props.URL, aNestedChild.GetAttributeValue("href", null).ToAbsoluteUri(baseUri));
                    return true;
                }

                if (anyChild.TrySelectFirstChild("area", out HtmlNode areaNestedChild, onlyOfType: true) && areaNestedChild.HasAttr("href") && !areaNestedChild.IsPropertyElement(MfType.Specification, MfType.Url))
                {
                    value = new MfValue(Props.URL, areaNestedChild.GetAttributeValue("href", null).ToAbsoluteUri(baseUri));
                    return true;
                }
            }

            value = null;
            return false;
        }
    }
}
