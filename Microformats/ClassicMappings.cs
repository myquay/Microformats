using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats
{
    /// <summary>
    /// Mapping for classic microformats
    /// </summary>
    public class ClassicMapping
    {
        public string ClassicType { get; set; }

        public string Type { get; set; }

        public (string classic, string modern)[] Properties { get; set; }
    }

    public static class ClassicMappings
    {
        public static string DISABLE_IMPLIED_PROPS = "disable-implied-properties";

        public static List<ClassicMapping> Mapping = new List<ClassicMapping>
        {
            new ClassicMapping
            {
                 ClassicType = "adr",
                 Type = "h-adr",
                 Properties = new (string classic, string modern)[]{
                    ("locality", "p-locality"),
                    ("region", "p-region"),
                    ("extended-address", "p-extended-address"),
                    ("post-office-box", "p-post-office-box"),
                    ("street-address", "p-street-address"),
                    ("postal-code", "p-postal-code"),
                    ("country-name", "p-country-name")
                 }
            },
            new ClassicMapping
            {
                 ClassicType = "geo",
                 Type = "h-geo",
                 Properties = new (string classic, string modern)[]{
                    ("latitude", "p-latitude"),
                    ("longitude", "p-longitude")
                 }
            },
            new ClassicMapping
            {
                 ClassicType = "hentry",
                 Type = "h-entry",
                 Properties = new (string classic, string modern)[]{
                    ("category", "p-category"),
                    ("entry-title", "p-name"),
                    ("published", "dt-published"),
                    ("entry-content", "e-content"),
                    ("entry-summary", "p-summary"),
                    ("author", "p-author"),
                    ("author", "h-card"),
                    ("geo", "p-geo"),
                    ("geo", "h-geo"),
                    ("updated", "dt-updated")
                 }
            },
            new ClassicMapping
            {
                 ClassicType = "hfeed",
                 Type = "h-feed",
                 Properties = new (string classic, string modern)[]{
                    ("category", "p-category"),
                    ("site-description", "p-summary"),
                    ("description", "p-summary"),
                    ("site-title", "p-name"),
                    ("title", "p-name")
                 }
            },
            new ClassicMapping
            {
                 ClassicType = "hproduct",
                 Type = "h-product",
                 Properties = new (string classic, string modern)[]{
                    ("category", "p-category"),
                    ("price", "p-price"),
                    ("description", "p-description"),
                    ("url", "u-url"),
                    ("photo", "u-photo"),
                    ("brand", "p-brand"),
                    ("identifier", "u-identifier"),
                    ("review", "p-review"),
                    ("review", "h-review"),
                    ("fn", "p-name")
                 }
            },
            new ClassicMapping
            {
                 ClassicType = "hrecipe",
                 Type = "h-recipe",
                 Properties = new (string classic, string modern)[]{
                    ("nutrition", "p-nutrition"),
                    ("yield", "p-yield"),
                    ("author", "p-author"),
                    ("author", "h-card"),
                    ("duration", "dt-duration"),
                    ("photo", "u-photo"),
                    ("instructions", "e-instructions"),
                    ("summary", "p-summary"),
                    ("fn", "p-name"),
                    ("ingredient", "p-ingredient"),
                    ("category", "p-category")
                 }
            },
            new ClassicMapping
            {
                 ClassicType = "hresume",
                 Type = "h-resume",
                 Properties = new (string classic, string modern)[]{
                    ("experience", "h-event"),
                    ("experience", "p-experience"),
                    ("summary", "p-summary"),
                    ("affiliation", "p-affiliation"),
                    ("affiliation", "h-card"),
                    ("contact", "p-contact"),
                    ("contact", "h-card"),
                    ("skill", "p-skill"),
                    ("education", "h-event"),
                    ("education", "p-education")
                 }
            },
            new ClassicMapping
            {
                 ClassicType = "hreview-aggregate",
                 Type = "h-review-aggregate",
                 Properties = new (string classic, string modern)[]{
                    ("rating", "p-rating"),
                    ("description", "p-description"),
                    ("photo", "u-photo"),
                    ("worst", "p-worst"),
                    ("reviewer", "h-card"),
                    ("reviewer", "p-reviewer"),
                    ("reviewer", "p-author"),
                    ("best", "p-best"),
                    ("count", "p-count"),
                    ("votes", "p-votes"),
                    ("dtreviewed", "dt-reviewed"),
                    ("url", "u-url"),
                    ("summary", "p-name"),
                    ("fn", "p-name"),
                    ("fn", "p-item"),
                    ("fn", "h-item")
                 }
            },
            new ClassicMapping
            {
                 ClassicType = "hreview",
                 Type = "h-review",
                 Properties = new (string classic, string modern)[]{
                    ("rating", "p-rating"),
                    ("worst", "p-worst"),
                    ("dtreviewed", "dt-reviewed"),
                    ("reviewer", "p-author"),
                    ("reviewer", "h-card"),
                    ("url", "u-url"),
                    ("url", "p-item"),
                    ("url", "h-item"),
                    ("photo", "u-photo"),
                    ("photo", "p-item"),
                    ("photo", "h-item"),
                    ("best", "p-best"),
                    ("description", "p-description"),
                    ("fn", "p-name"),
                    ("fn", "p-item"),
                    ("fn", "h-item"),
                    ("summary", "p-name"),
                    ("item vcard", "p-item"),
                    ("item vcard", "vcard"),
                    ("item vevent", "p-item"),
                    ("item vevent", "vevent"),
                    ("item hproduct", "p-item"),
                    ("item hproduct", "hproduct")
                 }
            },
            new ClassicMapping
            {
                 ClassicType = "vcard",
                 Type = "h-card",
                 Properties = new (string classic, string modern)[]{
                    ("tel", "p-tel"),
                    ("honorific-suffix", "p-honorific-suffix"),
                    ("family-name", "p-family-name"),
                    ("photo", "u-photo"),
                    ("logo", "u-logo"),
                    ("postal-code", "p-postal-code"),
                    ("country-name", "p-country-name"),
                    ("uid", "u-uid"),
                    ("category", "p-category"),
                    ("adr", "p-adr"),
                    ("adr", "h-adr"),
                    ("locality", "p-locality"),
                    ("nickname", "p-nickname"),
                    ("label", "p-label"),
                    ("note", "p-note"),
                    ("street-address", "p-street-address"),
                    ("latitude", "p-latitude"),
                    ("email", "u-email"),
                    ("bday", "dt-bday"),
                    ("extended-address", "p-extended-address"),
                    ("additional-name", "p-additional-name"),
                    ("organization-unit", "p-organization-unit"),
                    ("given-name", "p-given-name"),
                    ("key", "u-key"),
                    ("org", "p-org"),
                    ("honorific-prefix", "p-honorific-prefix"),
                    ("geo", "p-geo"),
                    ("geo", "h-geo"),
                    ("fn", "p-name"),
                    ("url", "u-url"),
                    ("region", "p-region"),
                    ("longitude", "p-longitude"),
                    ("organization-name", "p-organization-name"),
                    ("title", "p-job-title"),
                    ("role", "p-role")
                 }
            },
            new ClassicMapping
            {
                 ClassicType = "vevent",
                 Type = "h-event",
                 Properties = new (string classic, string modern)[]{
                    ("attendee", "p-attendee"),
                    ("description", "p-description"),
                    ("duration", "dt-duration"),
                    ("dtend", "dt-end"),
                    ("dtstart", "dt-start"),
                    ("geo", "p-location h-geo"),
                    ("organizer", "p-organizer"),
                    ("category", "p-category"),
                    ("url", "u-url"),
                    ("summary", "p-name"),
                    ("contact", "p-contact"),
                    ("location", "p-location")
                 }
            }
        };
    }
}
