using HtmlAgilityPack;
using Microformats.Grammar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Microformats.Parsers
{
    /// <summary>
    /// Property parser
    /// </summary>
    internal class PropertyParser
    {

        private static readonly string REGEX_TIMEZONE = @"(([+-]\d{1,2}:\d{2})|([+-]\d)|([+-]\d{3,4}))$";
        private static readonly string REGEX_DATE = @"^[0-9]{4}";

        internal static (object propertyValue, DateTimeParseContext context) ParseDate(HtmlNode node, DateTimeParseContext context)
        {
            context = context ?? new DateTimeParseContext();

            string parsedDate = null;

            if (node.ChildNodes.Any(c => c.HasClass("value")))
            {
                var dateTimeParts = node.ChildNodes.Where(c => c.HasClass("value")).Select(s =>
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
                    IsDate = Regex.IsMatch(a, REGEX_DATE),
                    IsTimezone = Regex.IsMatch(a, REGEX_TIMEZONE)
                });

                parsedDate = $"{dateTimeParts.FirstOrDefault(a => a.IsDate)?.Part} {dateTimeParts.FirstOrDefault(a => !a.IsDate && !a.IsTimezone)?.Part}{dateTimeParts.FirstOrDefault(a => a.IsTimezone)?.Part}".Trim();

                context.ImpliedTimezone = context.ImpliedTimezone ?? dateTimeParts.FirstOrDefault(a => a.IsTimezone)?.Part;
                context.ImpliedDate = context.ImpliedDate ?? dateTimeParts.FirstOrDefault(a => a.IsDate)?.Part;
                
            }
            else if (node.Is("time", "ins", "del") && node.HasAttr("datetime"))
            {
                parsedDate = node.GetAttributeValue("datetime", null);
            }
            else if (node.Is("img", "area") && node.HasAttr("alt"))
            {
                parsedDate = node.GetAttributeValue("alt", null);
            }
            else if (node.Is("abbr") && node.HasAttr("title"))
            {
                parsedDate = node.GetAttributeValue("title", null);
            }
            else if (node.Is("data", "input") && node.HasAttr("value"))
            {
                parsedDate = node.GetAttributeValue("value", null);
            }
            else
            {
                parsedDate = InnerTextParser.GetInnerText(node);
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
                parsedDate = Regex.Replace(parsedDate, @"(?<hour>\d{1,2})(?<minute>:\d{2})?(?<second>:\d{2})?[ap]\.?m\.?", delegate (Match m)
                {

                    var isAm = Regex.IsMatch(m.Value, @"[a]\.?m\.?", RegexOptions.IgnoreCase);
                    var hour = isAm ? $"{int.Parse(m.Groups["hour"].Value):D2}" :
                     $"{(int.Parse(m.Groups["hour"].Value) + 12):D2}";

                    return $"{hour}{m.Groups["minute"].Value ?? ":00"}{m.Groups["second"].Value ?? ":00"}";
                }, RegexOptions.IgnoreCase);

                //Ensure standard time format
                parsedDate = Regex.Replace(parsedDate, @"(?<hour>\s\d{1,2})(?<minute>:\d{2})?(?<second>:\d{2})?", delegate (Match m)
                {
                    return $"{m.Groups["hour"].Value}{(m.Groups["minute"].Success ? m.Groups["minute"].Value : ":00")}{(m.Groups["second"].Value)}";
                });

                //Set implied timezone if required
                if (context.ImpliedTimezone == null && Regex.IsMatch(parsedDate, REGEX_TIMEZONE))
                    context.ImpliedTimezone = Regex.Match(parsedDate, REGEX_TIMEZONE).Value;

                //Set implied timezone if required
                if (context.ImpliedDate == null && Regex.IsMatch(parsedDate, @"^[0-9]{4}-[0-9]{2}-[0-9]{2}"))
                    context.ImpliedDate = Regex.Match(parsedDate, @"^[0-9]{4}-[0-9]{2}-[0-9]{2}").Value;

            }

            return (parsedDate, context);
        }

        /// <summary>
        /// Parse an embedded property
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        internal static object ParseEmbedded(HtmlNode node)
        {
            return new MfEmbedded
            {
                Value = node.InnerText.Trim(),
                Html = node.InnerHtml.Trim()
            };
        }

        /// <summary>
        /// Parse text property
        /// </summary>
        /// <param name="node">Node to parse</param>
        /// <param name="value">Parsed value</param>
        /// <returns>Whether the parse was successful</returns>
        internal static string ParseText(HtmlNode node)
        {
            if (node.ChildNodes.Any(c => c.HasClass("value")))
            {
                return node.ChildNodes.Where(c => c.HasClass("value")).Select(s =>
                {
                    if (s.Is("img", "area") && s.HasAttr("alt"))
                        return s.GetAttributeValue("alt", null);
                    if (s.Is("data"))
                        return s.GetAttributeValue("value", null) ?? s.InnerText.Trim();
                    if (s.Is("abbr"))
                        return s.GetAttributeValue("title", null) ?? s.InnerText.Trim();
                    return s.InnerText.Trim();
                }).Aggregate((current, next) => $"{current} {next}");
            }
            else if (node.Is("abbr", "link") && node.HasAttr("title"))
            {
                return node.GetAttributeValue("title", null);
            }
            else if (node.Is("data", "input") && node.HasAttr("value"))
            {
                return node.GetAttributeValue("value", null);
            }
            else if (node.Is("img", "area") && node.HasAttr("alt"))
            {
                return node.GetAttributeValue("alt", null);
            }
            else
            {
                return InnerTextParser.GetInnerText(node);
            }
        }

        internal static object ParseUrl(HtmlNode node)
        {
            if (node.Is("a", "area", "link") && node.HasAttr("href"))
            {
                return node.GetAttributeValue("href", null);
            }
            else if (node.Is("img") && node.HasAttr("src"))
            {
                if (node.HasAttr("alt"))
                {
                    return new MfImage
                    {
                         Alt = node.GetAttributeValue("alt", null),
                         Value = node.GetAttributeValue("src", null)
                    };
                }
                else
                {
                    return node.GetAttributeValue("src", null);
                }
            }
            else if (node.Is("audio", "video", "source", "iframe") && node.HasAttr("src"))
            {
                return node.GetAttributeValue("src", null);
            }
            else if (node.Is("video") && node.HasAttr("poster"))
            {
                return node.GetAttributeValue("poster", null);
            }
            else if (node.Is("object") && node.HasAttr("data"))
            {
                return node.GetAttributeValue("data", null);
            }
            else if (node.ChildNodes.Any(c => c.HasClass("value")))
            {
               return node.ChildNodes.Where(c => c.HasClass("value")).Select(s =>
                {
                    if (s.Is("img", "area") && s.HasAttr("alt"))
                        return s.GetAttributeValue("alt", null);
                    if (s.Is("data"))
                        return s.GetAttributeValue("value", null) ?? s.InnerText.Trim();
                    if (s.Is("abbr"))
                        return s.GetAttributeValue("title", null) ?? s.InnerText.Trim();
                    return s.InnerText.Trim();
                }).Aggregate((current, next) => $"{current} {next}");
            }
            else if (node.Is("abbr") && node.HasAttr("title"))
            {
                return node.GetAttributeValue("title", null);
            }
            else if (node.Is("data", "input") && node.HasAttr("value"))
            {
                return node.GetAttributeValue("value", null);
            }
            else
            {
                return InnerTextParser.GetInnerText(node);
            }
        }
    }
}
